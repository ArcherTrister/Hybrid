// -----------------------------------------------------------------------
//  <copyright file="SystemModule.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-08-02 4:33</last-date>
// -----------------------------------------------------------------------

using ESoftor.Audits;
using ESoftor.Core.Modules;
using ESoftor.Web.Systems;

using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;


namespace ESoftor.Web.Startups
{
    /// <summary>
    /// 审计模块
    /// </summary>
    [Description("审计模块")]
    public class AuditModule : AuditModuleBase
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
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddScoped<IAuditStore, AuditDatabaseStore>();
            services.AddScoped<IAuditContract, AuditService>();

            return base.AddModule(services);
        }
    }
}