// -----------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Core.Packs;
using Hybrid.Localization;
using Hybrid.Reflection;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/>辅助扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        //TODO:自动加载
        ///// <summary>
        ///// Hybrid框架自动初始化，适用于AspNetCore环境
        ///// </summary>
        //public static IApplicationBuilder UseAutoHybrid(this IApplicationBuilder app)
        //{
        //    IServiceProvider provider = app.ApplicationServices;
        //    if (!(provider.GetService<IHybridModuleManager>() is IAspUseModule aspModuleManager))
        //    {
        //        throw new HybridException("接口 IHybridModuleManager 的注入类型不正确，该类型应同时实现接口 IAspUseModule");
        //    }
        //    aspModuleManager.UseModule(app);

        //    return app;
        //}

        /// <summary>
        /// Hybrid框架初始化，适用于AspNetCore环境
        /// </summary>
        public static IApplicationBuilder UseHybrid(this IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            ILogger logger = provider.GetLogger("ApplicationBuilderExtensions");
            logger.LogInformation(0, "Hybrid框架初始化开始");
            Stopwatch watch = Stopwatch.StartNew();
            HybridPack[] packs = provider.GetAllPacks();
            foreach (HybridPack pack in packs)
            {
                string packName = pack.GetType().GetDescription();
                logger.LogInformation($"正在初始化模块 “{packName}”");
                if (pack is AspHybridPack aspPack)
                {
                    aspPack.UsePack(app);
                }
                else
                {
                    pack.UsePack(provider);
                }
                logger.LogInformation($"模块 “{packName}” 初始化完成");
            }

            // TODO:初始化国际化
            ILocalizationManager localizationManager = provider.GetRequiredService<ILocalizationManager>();
            localizationManager.Initialize();

            watch.Stop();
            logger.LogInformation(0, $"Hybrid框架初始化完成，耗时：{watch.Elapsed}");

            return app;
        }

        /// <summary>
        /// 添加MVC并Area路由支持
        /// </summary>
        public static IApplicationBuilder UseMvcWithAreaRoute(this IApplicationBuilder app, bool area = true)
        {
            return app.UseMvc(builder =>
            {
                if (area)
                {
                    builder.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                }
                builder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// 添加Endpoint并Area路由支持
        /// </summary>
        public static IEndpointRouteBuilder MapControllersWithAreaRoute(this IEndpointRouteBuilder endpoints, bool area = true)
        {
            if (area)
            {
                endpoints.MapControllerRoute(
                    name: "areas-router",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return endpoints;
        }
    }
}