using Hybrid.Dependency;
using Hybrid.Exceptions;
using Hybrid.Http.Configuration;
using Hybrid.Security;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hybrid.Http
{
    /// <summary>
    /// Http客户端通信加密解密器
    /// </summary>
    [Dependency(ServiceLifetime.Transient, TryAdd = true)]
    public class ClientHttpCrypto : IClientHttpCrypto
    {
        private readonly ILogger _logger;
        private readonly TransmissionEncryptor _encryptor;
        private readonly string _publicKey;
        private readonly IHttpEncryptConfiguration _httpEncrypt;

        /// <summary>
        /// 初始化一个<see cref="ClientHttpCrypto"/>类型的新实例
        /// </summary>
        public ClientHttpCrypto(IServiceProvider provider)
        {
            _logger = provider.GetLogger(typeof(ClientHttpCrypto));
            _httpEncrypt = provider.GetRequiredService<IHttpEncryptConfiguration>();
            if (_httpEncrypt?.IsEnabled == true)
            {
                string clientPublicKey = _httpEncrypt.ClientPublicKey;
                if (string.IsNullOrEmpty(clientPublicKey))
                {
                    throw new HybridException("配置文件中HttpEncrypt节点的ClientPublicKey不能为空");
                }
                RsaHelper rsa = new RsaHelper();
                _encryptor = new TransmissionEncryptor(rsa.PrivateKey, _httpEncrypt.ClientPublicKey);
                _publicKey = rsa.PublicKey;
            }
        }

        /// <summary>
        /// 将要发往服务器的请求进行加密
        /// </summary>
        /// <param name="request">未加密的请求</param>
        /// <returns>加密后的请求</returns>
        public virtual async Task<HttpRequestMessage> EncryptRequest(HttpRequestMessage request)
        {
            if (_encryptor == null || string.IsNullOrEmpty(_publicKey) || request.Method == HttpMethod.Get || request.Content == null)
            {
                return request;
            }

            string data = await request.Content.ReadAsStringAsync();
            data = _encryptor.EncryptData(data);
            request = request.CreateNew(data);
            request.Headers.Add(HttpHeaderNames.ClientPublicKey, _publicKey);
            return request;
        }

        /// <summary>
        /// 解密从服务器收到的响应
        /// </summary>
        /// <param name="response">加密的响应</param>
        /// <returns>解密后的响应</returns>
        public virtual async Task<HttpResponseMessage> DecryptResponse(HttpResponseMessage response)
        {
            if (_encryptor == null || !response.IsSuccessStatusCode)
            {
                return response;
            }

            string data = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(data))
            {
                return response;
            }

            try
            {
                data = _encryptor.DecryptAndVerifyData(data);
                if (data == null)
                {
                    throw new HybridException("客户端对返回数据签名验证未通过。");
                }
                response = response.CreateNew(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("返回数据处理错误，请重试操作。", ex);
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
