namespace Hybrid.Http.Configuration
{
    /// <summary>
    /// Http通信加密选项配置
    /// </summary>
    public sealed class HttpEncryptConfiguration : IHttpEncryptConfiguration
    {
        /// <summary>
        /// 获取或设置 服务端私钥
        /// </summary>
        public string HostPrivateKey { get; set; }

        /// <summary>
        /// 获取或设置 客户端公钥
        /// </summary>
        public string ClientPublicKey { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}