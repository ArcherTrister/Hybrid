using Hybrid.Domain.Entities;

namespace Hybrid.Http.Configuration
{
    /// <summary>
    /// Http通信加密选项配置
    /// </summary>
    public interface IHttpEncryptConfiguration: IEnabled
    {
        /// <summary>
        /// 获取或设置 服务端私钥
        /// </summary>
        string HostPrivateKey { get; set; }

        /// <summary>
        /// 获取或设置 客户端公钥
        /// </summary>
        string ClientPublicKey { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
