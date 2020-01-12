// -----------------------------------------------------------------------
//  <copyright file="SqlServerDefaultDbContextMigrationModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;
using ESoftor.Entity;
using ESoftor.EntityFrameworkCore;
using ESoftor.EntityFrameworkCore.Defaults;
using ESoftor.EntityFrameworkCore.SqlServer;

using System;
using System.ComponentModel;

namespace ESoftor.Web.Startups.SqlServer
{
    /// <summary>
    /// SqlServer-DefaultDbContext迁移模块
    /// </summary>
    [DependsOnModules(typeof(SqlServerEntityFrameworkCoreModule))]
    [Description("SqlServer-DefaultDbContext迁移模块")]
    public class SqlServerDefaultDbContextMigrationModule : MigrationModuleBase<DefaultDbContext>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        protected override DatabaseType DatabaseType => DatabaseType.SqlServer;

        protected override DefaultDbContext CreateDbContext(IServiceProvider scopedProvider)
        {
            return new SqlServerDesignTimeDefaultDbContextFactory(scopedProvider).CreateDbContext(new string[0]);
        }
    }
}