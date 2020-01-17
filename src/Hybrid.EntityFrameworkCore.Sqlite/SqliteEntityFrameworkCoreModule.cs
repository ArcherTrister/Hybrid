// -----------------------------------------------------------------------
//  <copyright file="SqliteEntityFrameworkCoreModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-11-05 16:29</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Domain.EntityFramework;
using Hybrid.Domain.Repositories;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;
using System.Linq;

namespace Hybrid.EntityFrameworkCore.Sqlite
{
    /// <summary>
    /// SqliteEntityFrameworkCore模块
    /// </summary>
    [Description("SqliteEntityFrameworkCore模块")]
    public class SqliteEntityFrameworkCoreModule : EntityFrameworkCoreModuleBase
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

            services.AddScoped(typeof(ISqlExecutor<,>), typeof(SqliteDapperSqlExecutor<,>));

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            bool? hasSqlite = provider.GetHybridOptions()?.DbContexts?.Values.Any(m => m.DatabaseType == DatabaseType.Sqlite);
            if (hasSqlite == null || !hasSqlite.Value)
            {
                return;
            }

            base.UseModule(provider);
        }
    }
}