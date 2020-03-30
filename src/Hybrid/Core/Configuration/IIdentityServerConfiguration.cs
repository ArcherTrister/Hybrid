using Hybrid.Domain.Entities;

namespace Hybrid.Core.Configuration
{
    public interface IIdentityServerConfiguration : IEnabled
    {
        /// <summary>
        /// 获取或设置 授权服务器地址
        /// </summary>
        string Authority { get; set; }

        /// <summary>
        /// 获取或设置 订阅方(要验证的Api)，多个用逗号隔开
        /// </summary>
        string Audience { get; set; }

        /// <summary>
        /// 获取或设置 是否使用ssl
        /// </summary>
        bool UseHttps { get; set; }

        /// <summary>
        /// 获取或设置 授权服务器与Api集成/授权服务器则设置为true,如果只有Api则设置为false
        /// </summary>
        bool IsLocalApi { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}