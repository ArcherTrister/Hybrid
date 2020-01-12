﻿// -----------------------------------------------------------------------
//  <copyright file="ESoftorCoreModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:19</last-date>
// -----------------------------------------------------------------------

using ESoftor.Audits;
using ESoftor.Caching;
using ESoftor.Collections;
using ESoftor.Core.Options;
using ESoftor.Domain.Entities;
using ESoftor.Filter;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

namespace ESoftor.Core.Modules
{
    /// <summary>
    /// ESoftor核心模块
    /// </summary>
    [Description("ESoftor核心模块")]
    public class ESoftorCoreModule : ESoftorModule
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
            services.TryAddSingleton<IConfigureOptions<ESoftorOptions>, ESoftorOptionsSetup>();
            services.TryAddSingleton<IEntityTypeFinder, EntityTypeFinder>();
            services.TryAddSingleton<IInputDtoTypeFinder, InputDtoTypeFinder>();
            services.TryAddSingleton<IOutputDtoTypeFinder, OutputDtoTypeFinder>();

            services.TryAddSingleton<ICacheService, CacheService>();
            services.TryAddScoped<IFilterService, FilterService>();

            return services;
        }

        public override void UseModule(IServiceProvider provider)
        {
            AuditingConfiguration configuration = provider.GetESoftorOptions().AuditingConfiguration;
            AddIgnoredTypes(configuration);
        }

        private void AddIgnoredTypes(AuditingConfiguration configuration)
        {
            var commonIgnoredTypes = new[]
            {
                typeof(Stream),
                typeof(Expression)
            };

            foreach (var ignoredType in commonIgnoredTypes)
            {
                configuration.IgnoredTypes.AddIfNotContains(ignoredType);
                //Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }

            //var validationIgnoredTypes = new[] { typeof(Type) };
            //foreach (var ignoredType in validationIgnoredTypes)
            //{
            //    Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            //}
        }
    }
}