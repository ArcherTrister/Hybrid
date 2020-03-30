// -----------------------------------------------------------------------
//  <copyright file="MvcFunctionPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Authorization.Modules;
using Hybrid.Core.Packs;
using Hybrid.Systems;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.ComponentModel;
using System.Linq;

namespace Hybrid.AspNetCore.Mvc
{
    /// <summary>
    /// MVC功能点模块
    /// </summary>
    [DependsOnPacks(typeof(MvcPack), typeof(SystemPack))]
    [Description("MVC功能点模块")]
    public class MvcFunctionPack : AspHybridPack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.GetOrAddTypeFinder<IFunctionTypeFinder>(assemblyFinder => new MvcControllerTypeFinder(assemblyFinder));
            services.AddSingleton<IFunctionHandler, MvcFunctionHandler>();
            services.TryAddSingleton<IModuleInfoPicker, MvcModuleInfoPicker>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            IFunctionHandler functionHandler = app.ApplicationServices.GetServices<IFunctionHandler>().FirstOrDefault(m => m.GetType() == typeof(MvcFunctionHandler));
            if (functionHandler == null)
            {
                return;
            }
            functionHandler.Initialize();

            IsEnabled = true;
        }
    }
}