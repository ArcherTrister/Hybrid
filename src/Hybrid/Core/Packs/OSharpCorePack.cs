// -----------------------------------------------------------------------
//  <copyright file="HybridCorePack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:19</last-date>
// -----------------------------------------------------------------------

using Hybrid.Caching;
using Hybrid.Core.Configuration;
using Hybrid.Core.Options;
using Hybrid.Entity;
using Hybrid.Filter;
using Hybrid.Http;
using Hybrid.Localization;
using Hybrid.Localization.Configuration;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Dictionaries.Xml;
using Hybrid.Net;
using Hybrid.Net.Mail.Configuration;
using Hybrid.Reflection;
using Hybrid.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using System;
using System.ComponentModel;

namespace Hybrid.Core.Packs
{
    /// <summary>
    /// Hybrid核心模块
    /// </summary>
    [Description("Hybrid核心模块")]
    public class HybridCorePack : HybridPack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Core;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<IConfigureOptions<HybridOptions>, HybridOptionsSetup>();
            services.TryAddSingleton<IEntityTypeFinder, EntityTypeFinder>();
            services.TryAddSingleton<IInputDtoTypeFinder, InputDtoTypeFinder>();
            services.TryAddSingleton<IOutputDtoTypeFinder, OutputDtoTypeFinder>();
            services.TryAddSingleton<ICancellationTokenProvider, NoneCancellationTokenProvider>();
            services.TryAddSingleton<IEmailSender, DefaultEmailSender>();

            services.TryAddSingleton<ICacheService, CacheService>();
            services.TryAddScoped<IFilterService, FilterService>();

            services.TryAddTransient<IClientHttpCrypto, ClientHttpCrypto>();
            services.AddTransient<ClientHttpCryptoHandler>();

            services.AddDistributedMemoryCache();

            //Add Localization Service
            services.AddTransient<ILanguageManager, LanguageManager>();
            services.AddTransient<ILanguageProvider, DefaultLanguageProvider>();
            services.AddSingleton<ILocalizationContext, LocalizationContext>();
            services.AddSingleton<ILocalizationManager, LocalizationManager>();

            // TODO: Add Configuration Service
            services.AddSingleton<ILocalizationConfiguration, LocalizationConfiguration>();
            services.AddSingleton<IEmailSenderConfiguration, EmailSenderConfiguration>();
            services.AddSingleton<IIdentityServerConfiguration, IdentityServerConfiguration>();
            services.AddSingleton<IQuartzConfiguration, QuartzConfiguration>();
            //services.AddSingleton<IAuditingConfiguration, AuditingConfiguration>();
            //services.AddSingleton<IHttpEncryptConfiguration, HttpEncryptConfiguration>();

            return services;
        }

        public override void UsePack(IServiceProvider provider)
        {
            IHybridStartupConfiguration Configuration = provider.GetRequiredService<IHybridStartupConfiguration>();
            HybridOptions Options = provider.GetRequiredService<IOptions<HybridOptions>>().Value;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LocalizationConsts.HybridSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(IQuartzConfiguration).GetAssembly(), "Hybrid.Localization.Sources.XmlSource"
            )));

            InitConfiguration(Configuration, Options);
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        private void InitConfiguration(IHybridStartupConfiguration configuration, HybridOptions options)
        {
            // TODO: InitConfiguration

            ////Auditing
            //configuration.Auditing.IsEnabled = options.Auditing.IsEnabled;
            //configuration.Auditing.IsEnabledForAnonymousUsers = options.Auditing.IsEnabledForAnonymousUsers;
            //configuration.Auditing.SaveReturnValues = options.Auditing.SaveReturnValues;
            //var commonIgnoredTypes = new[]
            //{
            //    typeof(Stream),
            //    typeof(Expression)
            //};

            //foreach (var ignoredType in commonIgnoredTypes)
            //{
            //    configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
            //    //Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            //}

            //var validationIgnoredTypes = new[] { typeof(Type) };
            //foreach (var ignoredType in validationIgnoredTypes)
            //{
            //    Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            //}

            // Email
            configuration.EmailSender.DisplayName = options.EmailSender.DisplayName;
            configuration.EmailSender.Domain = options.EmailSender.Domain;
            configuration.EmailSender.EnableSsl = options.EmailSender.EnableSsl;
            configuration.EmailSender.Host = options.EmailSender.Host;
            configuration.EmailSender.IsEnabled = options.EmailSender.IsEnabled;
            configuration.EmailSender.Password = options.EmailSender.Password;
            configuration.EmailSender.Port = options.EmailSender.Port;
            configuration.EmailSender.UseDefaultCredentials = options.EmailSender.UseDefaultCredentials;
            configuration.EmailSender.UserName = options.EmailSender.UserName;

            // IdentityServer4
            // Quartz

            ////HttpEncrypt
            //configuration.HttpEncrypt.ClientPublicKey = options.HttpEncrypt.ClientPublicKey;
            //configuration.HttpEncrypt.HostPrivateKey = options.HttpEncrypt.HostPrivateKey;
            //configuration.HttpEncrypt.IsEnabled = options.HttpEncrypt.IsEnabled;
        }
    }
}