// -----------------------------------------------------------------------
//  <copyright file="ExceptionlessPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-02-28 1:42</last-date>
// -----------------------------------------------------------------------

using System;

using Exceptionless;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Hybrid.AspNetCore;
using Hybrid.Core.Packs;
using Hybrid.Exceptions;
using Hybrid.Extensions;


namespace Hybrid.Exceptionless
{
    /// <summary>
    /// Exceptionless分布式异常日志模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class ExceptionlessPackCore : AspHybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            bool enabled = configuration["Hybrid:Exceptionless:Enabled"].CastTo(false);
            if (!enabled)
            {
                return services;
            }

            services.AddSingleton<ILoggerProvider, ExceptionlessLoggerProvider>();
            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            IConfiguration configuration = app.ApplicationServices.GetService<IConfiguration>();
            bool enabled = configuration["Hybrid:Exceptionless:Enabled"].CastTo(false);
            if (!enabled)
            {
                return;
            }

            string apiKey = configuration["Hybrid:Exceptionless:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new HybridException("配置文件中Exceptionless节点的ApiKey不能为空");
            }

            ExceptionlessClient.Default.Configuration.ApiKey = apiKey;
            string serverUrl = configuration["Hybrid:Exceptionless:ServerUrl"];
            if (!string.IsNullOrEmpty(serverUrl))
            {
                ExceptionlessClient.Default.Configuration.ServerUrl = serverUrl;
            }

            app.UseExceptionless();

            UsePack(app.ApplicationServices);
        }

    }
}