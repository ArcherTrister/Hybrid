// -----------------------------------------------------------------------
//  <copyright file="AspHybridModuleManager.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Exceptions;
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

            foreach (HybridModule module in LoadedModules)
            {
                if (module is AspHybridModule aspModule)
                {
                    aspModule.UseModule(app);
                }
                else
                {
                    module.UseModule(app.ApplicationServices);
                }
            }

            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            logger.LogInformation($"Hybrid框架初始化完成，耗时：{ts:g}");
        }
    }
}