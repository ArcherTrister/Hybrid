using Hybrid.Audits.Configuration;
using Hybrid.Localization.Configuration;
using Hybrid.Net.Mail.Configuration;

namespace Hybrid.Configuration
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
        /// 用于设置审计配置
        /// </summary>
        IAuditingConfiguration Auditing { get; }
    }
}