﻿// -----------------------------------------------------------------------
//  <copyright file="SqlExecutorBase.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-15 19:10</last-date>
// -----------------------------------------------------------------------

using Dapper;

using ESoftor.Domain.Entities;
using ESoftor.Domain.Repositories;
using ESoftor.Domain.Uow;
using ESoftor.Entity;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Data;

namespace ESoftor.EntityFrameworkCore
{
    /// <summary>
    /// Sql功能执行基类
    /// </summary>
    public abstract class SqlExecutorBase<TEntity, TKey> : ISqlExecutor<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        private readonly string _connectionString;

        /// <summary>
        /// 初始化一个<see cref="SqlExecutorBase{TEntity,TKey}"/>类型的新实例
        /// </summary>
        protected SqlExecutorBase(IUnitOfWorkManager unitOfWorkManager)
        {
            DbContext dbContext = (DbContext)unitOfWorkManager.GetDbContext<TEntity, TKey>();
            _connectionString = dbContext.Database.GetDbConnection().ConnectionString;
        }

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected abstract IDbConnection GetDbConnection(string connectionString);

        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        public virtual IEnumerable<TResult> FromSql<TResult>(string sql, object param = null)
        {
            using (IDbConnection db = GetDbConnection(_connectionString))
            {
                return db.Query<TResult>(sql, param);
            }
        }

        /// <summary>
        /// 执行指定的SQL语句
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>操作影响的行数</returns>
        public virtual int ExecuteSqlCommand(string sql, object param = null)
        {
            using (IDbConnection db = GetDbConnection(_connectionString))
            {
                return db.Execute(sql, param);
            }
        }
    }
}