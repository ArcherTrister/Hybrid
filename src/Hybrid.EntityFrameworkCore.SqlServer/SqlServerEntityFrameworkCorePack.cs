﻿// -----------------------------------------------------------------------
//  <copyright file="SqlServerEntityFrameworkCorePack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:24</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;
using System.Linq;

namespace Hybrid.Entity.SqlServer
{
    /// <summary>
    /// SqlServerEntityFrameworkCore模块
    /// </summary>
    [Description("SqlServerEntityFrameworkCore模块")]
    public class SqlServerEntityFrameworkCorePack : EntityFrameworkCorePackBase
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

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
            services.AddSingleton<IDbContextOptionsBuilderDriveHandler, DbContextOptionsBuilderDriveHandler>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            bool? hasMsSql = provider.GetHybridOptions()?.DbContexts?.Values.Any(m => m.DatabaseType == DatabaseType.SqlServer);
            if (hasMsSql == null || !hasMsSql.Value)
            {
                return;
            }

            base.UsePack(provider);
        }
    }
}