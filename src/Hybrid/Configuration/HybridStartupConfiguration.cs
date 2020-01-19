using Hybrid.Audits.Configuration;
using Hybrid.Localization.Configuration;
using Hybrid.Net.Mail.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Configuration
{
    internal sealed class HybridStartupConfiguration : DictionaryBasedConfig, IHybridStartupConfiguration
    {
        public HybridStartupConfiguration(IServiceProvider serviceProvider)
        {
            // TODO: HybridStartupConfiguration
            Localization = serviceProvider.GetRequiredService<ILocalizationConfiguration>();
            EmailSender = serviceProvider.GetRequiredService<IEmailSenderConfiguration>();
            Auditing = serviceProvider.GetRequiredService<IAuditingConfiguration>();
            // Localization = serviceProvider.GetRequiredService<ILocalizationConfiguration>();
        }

        /// <summary>
        /// 用于设置本地化配置
        /// </summary>
        public ILocalizationConfiguration Localization { get; set; }

        /// <summary>
        /// 用于设置邮件发送配置
        /// </summary>
        public IEmailSenderConfiguration EmailSender { get; set; }

        /// <summary>
        /// 用于设置审计配置
        /// </summary>
        public IAuditingConfiguration Auditing { get; set; }
    }
}