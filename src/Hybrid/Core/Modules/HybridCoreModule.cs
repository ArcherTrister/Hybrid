﻿// -----------------------------------------------------------------------
//  <copyright file="HybridCoreModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:19</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits.Configuration;
using Hybrid.Caching;
using Hybrid.Configuration;
using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Dependency;
using Hybrid.Domain.Entities;
using Hybrid.Extensions;
using Hybrid.Filter;

using Hybrid.Localization;
using Hybrid.Localization.Configuration;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Dictionaries.Xml;
using Hybrid.Net.Mail.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// Hybrid核心模块
    /// </summary>
    [Description("Hybrid核心模块")]
    public class HybridCoreModule : HybridModule
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Core;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<ISingletonFactory, SingletonFactory>();
            //services.TryAddSingleton<IConfigureOptions<HybridOptions>, HybridOptionsSetup>();
            services.TryAddSingleton<IEntityTypeFinder, EntityTypeFinder>();
            services.TryAddSingleton<IInputDtoTypeFinder, InputDtoTypeFinder>();
            services.TryAddSingleton<IOutputDtoTypeFinder, OutputDtoTypeFinder>();

            services.TryAddSingleton<ICacheService, CacheService>();
            services.TryAddScoped<IFilterService, FilterService>();

            //Add Localization Service
            services.AddTransient<ILanguageManager, LanguageManager>();
            services.AddTransient<ILanguageProvider, DefaultLanguageProvider>();
            services.AddSingleton<ILocalizationContext, LocalizationContext>();
            services.AddSingleton<ILocalizationManager, LocalizationManager>();

            //Add Configuration Service
            services.AddSingleton<ILocalizationConfiguration, LocalizationConfiguration>();
            services.AddSingleton<IEmailSenderConfiguration, EmailSenderConfiguration>();
            services.AddSingleton<IAuditingConfiguration, AuditingConfiguration>();

            return services;
        }

        public override void UseModule(IServiceProvider provider)
        {
            IHybridStartupConfiguration Configuration = provider.GetRequiredService<IHybridStartupConfiguration>();
            HybridOptions Options = provider.GetRequiredService<IOptions<HybridOptions>>().Value;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    HybridConstants.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QuartzOptions).GetAssembly(), "Hybrid.Localization.Sources.XmlSource"
            )));

            ILocalizationManager localizationManager = provider.GetRequiredService<ILocalizationManager>();
            localizationManager.Initialize();

            InitConfiguration(Configuration, Options);

            //TODO:Config
        }

        private void InitConfiguration(IHybridStartupConfiguration configuration, HybridOptions options)
        {
            //Auditing
            configuration.Auditing = options.Auditing;
            var commonIgnoredTypes = new[]
            {
                typeof(Stream),
                typeof(Expression)
            };

            foreach (var ignoredType in commonIgnoredTypes)
            {
                configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                //Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
            //var validationIgnoredTypes = new[] { typeof(Type) };
            //foreach (var ignoredType in validationIgnoredTypes)
            //{
            //    Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            //}

            //Email
            configuration.EmailSender = options.EmailSender;
        }
    }
}