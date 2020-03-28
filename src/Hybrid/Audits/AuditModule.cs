// -----------------------------------------------------------------------
//  <copyright file="AuditModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits.Configuration;
using Hybrid.Core.Modules;
using Hybrid.EventBuses;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;

namespace Hybrid.Audits
{
    /// <summary>
    /// 审计模块
    /// </summary>
    [Description("审计模块")]
    [DependsOnModules(typeof(EventBusModule))]
    public class AuditModule : HybridModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuditStore, AuditDatabaseStore>();

            return base.AddServices(services);
        }

        ///// <summary>
        ///// 将模块服务添加到依赖注入服务容器中
        ///// </summary>
        ///// <param name="services">依赖注入服务容器</param>
        ///// <returns></returns>
        //public override IServiceCollection AddServices(IServiceCollection services)
        //{
        //    services.AddEventHandler<AuditEntityEventHandler>();

        //    return base.AddServices(services);
        //}

        public override void UseModule(IServiceProvider provider)
        {
            IAuditingConfiguration auditingConfiguration = provider.GetRequiredService<IAuditingConfiguration>();
            IsEnabled = auditingConfiguration.IsEnabled;
        }
    }
}