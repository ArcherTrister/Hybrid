// -----------------------------------------------------------------------
//  <copyright file="ISqlExecutor.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Dependency;
using ESoftor.Domain.Entities;
using ESoftor.Domain.EntityFramework;
using System.Collections.Generic;

namespace ESoftor.Domain.Repositories
{
    /// <summary>
    /// 定义SQL语句执行功能
    /// </summary>
    [MultipleDependency]
    public interface ISqlExecutor<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// 查询指定SQL的结果集
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>结果集</returns>
        IEnumerable<TResult> FromSql<TResult>(string sql, object param = null);

        /// <summary>
        /// 执行指定的SQL语句
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>操作影响的行数</returns>
        int ExecuteSqlCommand(string sql, object param = null);
    }
}