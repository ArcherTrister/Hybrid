// -----------------------------------------------------------------------
//  <copyright file="MySqlDefaultDbContextMigrationModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Domain.EntityFramework;
using Hybrid.EntityFrameworkCore;
using Hybrid.EntityFrameworkCore.Defaults;
using Hybrid.EntityFrameworkCore.MySql;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Startups.MySql
{
    /// <summary>
    /// MySql-DefaultDbContext迁移模块
    /// </summary>
    [DependsOnModules(typeof(MySqlEntityFrameworkCoreModule))]
    [Description("MySql-DefaultDbContext迁移模块")]
    public class MySqlDefaultDbContextMigrationModule : MigrationModuleBase<DefaultDbContext>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        protected override DatabaseType DatabaseType => DatabaseType.MySql;

        /// <summary>
        /// 重写实现获取数据上下文实例
        /// </summary>
        /// <param name="scopedProvider">服务提供者</param>
        /// <returns></returns>
        protected override DefaultDbContext CreateDbContext(IServiceProvider scopedProvider)
        {
            return new MySqlDesignTimeDefaultDbContextFactory(scopedProvider).CreateDbContext(new string[0]);
        }
    }
}