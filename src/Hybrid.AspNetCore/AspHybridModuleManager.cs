﻿using Hybrid.Core.Packs;
using Hybrid.Exceptions;
using Hybrid.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.ComponentModel;

namespace Hybrid.AspNetCore
{
    /// <summary>
    /// AspNetCore 模块管理器
    /// </summary>
    public class AspHybridModuleManager : HybridModuleManager, IAspUseModule
    {
        /// <summary>
        /// 应用模块服务，仅在非AspNetCore环境下调用，AspNetCore环境请执行<see cref="UseModule(IApplicationBuilder)"/>功能
        /// </summary>
        /// <param name="provider">服务提供者</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void UseModule(IServiceProvider provider)
        {
            IWebHostEnvironment environment = provider.GetService<IWebHostEnvironment>();
            if (environment != null)
            {
                throw new HybridException("当前处于AspNetCore环境，请使用UseModule(IApplicationBuilder)进行初始化");
            }
            base.UseModule(provider);
        }

        /// <summary>
        /// 应用模块服务，仅在AspNetCore环境下调用，非AspNetCore环境请执行<see cref="UseModule(IServiceProvider)"/>功能
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public void UseModule(IApplicationBuilder app)
        {
            ILogger logger = app.ApplicationServices.GetLogger<AspHybridModuleManager>();
            logger.LogInformation("Hybrid框架初始化开始");
            DateTime dtStart = DateTime.Now;

            foreach (HybridPack module in LoadedModules)
            {
                if (module is AspHybridPack aspModule)
                {
                    aspModule.UsePack(app);
                }
                else
                {
                    module.UsePack(app.ApplicationServices);
                }
            }

            // TODO:初始化国际化
            IServiceProvider provider = app.ApplicationServices;
            ILocalizationManager localizationManager = provider.GetRequiredService<ILocalizationManager>();
            localizationManager.Initialize();

            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            logger.LogInformation($"Hybrid框架初始化完成，耗时：{ts:g}");
        }
    }
}
