// -----------------------------------------------------------------------
//  <copyright file="AuditPackBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;
using Hybrid.EventBuses;

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.Audits
{
    /// <summary>
    /// 审计模块基类
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack))]
    public abstract class AuditPackBase : HybridPack
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
            services.AddEventHandler<AuditEntityEventHandler>();

            return base.AddServices(services);
        }

        ///// <summary>
        ///// 将模块服务添加到依赖注入服务容器中【自动模式】
        ///// </summary>
        ///// <param name="services">依赖注入服务容器</param>
        ///// <returns></returns>
        //public override IServiceCollection AddAutoServices(IServiceCollection services)
        //{
        //    services.AddEventHandler<AuditEntityEventHandler>();

        //    return base.AddAutoServices(services);
        //}
    }
}