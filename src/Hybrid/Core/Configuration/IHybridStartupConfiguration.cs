using Hybrid.Localization.Configuration;
using Hybrid.Net.Mail.Configuration;

namespace Hybrid.Core.Configuration
{
    internal interface IHybridStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// 用于设置本地化配置
        /// </summary>
        ILocalizationConfiguration Localization { get; }

        /// <summary>
        /// 用于设置邮件发送配置
        /// </summary>
        IEmailSenderConfiguration EmailSender { get; }

        /// <summary>
        /// 用于设置IdentityServer4配置
        /// </summary>
        IIdentityServerConfiguration IdentityServer { get; }

        ///// <summary>
        ///// 用于设置审计配置
        ///// </summary>
        //IAuditingConfiguration Auditing { get; }

        ///// <summary>
        ///// 用于设置Http通信加密选项配置
        ///// </summary>
        //IHttpEncryptConfiguration HttpEncrypt { get; }
    }
}
