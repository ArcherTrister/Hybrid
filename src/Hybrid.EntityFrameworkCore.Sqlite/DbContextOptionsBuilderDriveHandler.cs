// -----------------------------------------------------------------------
//  <copyright file="SqliteDbContextOptionsBuilderCreator.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-21 1:07</last-date>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using System.Data.Common;


namespace Hybrid.Entity.Sqlite
{
    /// <summary>
    /// Sqlite的<see cref="DbContextOptionsBuilder"/>数据库驱动差异处理器
    /// </summary>
    public class DbContextOptionsBuilderDriveHandler : IDbContextOptionsBuilderDriveHandler
    {
        /// <summary>
        /// 获取 数据库类型名称，如 SQLSERVER，MYSQL，SQLITE等
        /// </summary>
        public DatabaseType Type { get; } = DatabaseType.Sqlite;

        /// <summary>
        /// 处理<see cref="DbContextOptionsBuilder"/>驱动差异
        /// </summary>
        /// <param name="builder">创建器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="existingConnection">已存在的连接对象</param>
        /// <returns></returns>
        public DbContextOptionsBuilder Handle(DbContextOptionsBuilder builder, string connectionString, DbConnection existingConnection)
        {
            if (existingConnection == null)
            {
                return builder.UseSqlite(connectionString);
            }
            return builder.UseSqlite(existingConnection);
        }
    }
}