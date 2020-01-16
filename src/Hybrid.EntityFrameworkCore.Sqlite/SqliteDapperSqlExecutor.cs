// -----------------------------------------------------------------------
//  <copyright file="SqliteDapperSqlExecutor.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-15 19:37</last-date>
// -----------------------------------------------------------------------

using System.Data;
using Hybrid.Domain.Entities;
using Hybrid.Domain.EntityFramework;
using Hybrid.Domain.Uow;
using Microsoft.Data.Sqlite;


namespace Hybrid.EntityFrameworkCore.Sqlite
{
    /// <summary>
    /// Sqlite的Dapper-Sql功能执行
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">编号类型</typeparam>
    public class SqliteDapperSqlExecutor<TEntity, TKey> : SqlExecutorBase<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 初始化一个<see cref="SqlExecutorBase{TEntity,TKey}"/>类型的新实例
        /// </summary>
        public SqliteDapperSqlExecutor(IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        { }
        
        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public override DatabaseType DatabaseType => DatabaseType.Sqlite;

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected override IDbConnection GetDbConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}