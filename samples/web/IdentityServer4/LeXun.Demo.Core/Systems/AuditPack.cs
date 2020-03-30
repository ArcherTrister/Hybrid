// -----------------------------------------------------------------------
//  <copyright file="SystemPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:33</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits;
using Hybrid.Core.Packs;

using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;

namespace LeXun.Demo.Systems
{
    /// <summary>
    /// 审计模块
    /// </summary>
    [Description("审计模块")]
    public class AuditPack : AuditPackBase
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
            services.AddScoped<IAuditStore, AuditDatabaseStore>();
            services.AddScoped<IAuditContract, AuditService>();

            return base.AddServices(services);
        }
    }
}