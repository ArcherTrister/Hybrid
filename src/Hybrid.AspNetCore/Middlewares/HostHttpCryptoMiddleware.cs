﻿using Hybrid.AspNetCore.Http;

using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace Hybrid.AspNetCore.Middlewares
{
    /// <summary>
    /// 服务端通信加密解密中间件，对请求进行解密，对响应进行加密，如使用，请将此中间件放在第一个
    /// </summary>
    public class HostHttpCryptoMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostHttpCrypto _hostHttpCrypto;

        /// <summary>
        /// 初始化一个<see cref="HostHttpCryptoMiddleware"/>类型的新实例
        /// </summary>
        public HostHttpCryptoMiddleware(RequestDelegate next, IHostHttpCrypto hostHttpCrypto)
        {
            _next = next;
            _hostHttpCrypto = hostHttpCrypto;
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest request = context.Request;
            await _hostHttpCrypto.DecryptRequest(request);
            await _next(context);
            HttpResponse response = context.Response;
            await _hostHttpCrypto.EncryptResponse(response);
        }
    }
}
