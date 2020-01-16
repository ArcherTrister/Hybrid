// -----------------------------------------------------------------------
//  <copyright file="SqlServerEntityFrameworkCoreModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Domain.EntityFramework;
using Hybrid.Domain.Repositories;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;
using System.Linq;

namespace Hybrid.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// SqlServerEntityFrameworkCore模块
    /// </summary>
    [Description("SqlServerEntityFrameworkCore模块")]
    public class SqlServerEntityFrameworkCoreModule : EntityFrameworkCoreModuleBase
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Framework;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services = base.AddServices(services);
            services.AddScoped(typeof(ISqlExecutor<,>), typeof(SqlServerDapperSqlExecutor<,>));

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            bool? hasMsSql = provider.GetHybridOptions()?.DbContexts?.Values.Any(m => m.DatabaseType == DatabaseType.SqlServer);
            if (hasMsSql == null || !hasMsSql.Value)
            {
                return;
            }

            base.UseModule(provider);
        }
    }
}