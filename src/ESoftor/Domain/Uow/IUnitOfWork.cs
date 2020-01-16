﻿// -----------------------------------------------------------------------
//  <copyright file="IUnitOfWork.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities;
using ESoftor.Domain.EntityFramework;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Domain.Uow
{
    /// <summary>
    /// 业务单元操作接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 获取 工作单元的事务是否已提交
        /// </summary>
        bool HasCommitted { get; }

        /// <summary>
        /// 获取指定数据上下文类型<typeparamref name="TEntity"/>的实例
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns><typeparamref name="TEntity"/>所属上下文类的实例</returns>
        IDbContext GetDbContext<TEntity, TKey>() where TEntity : IEntity<TKey>;

        /// <summary>
        /// 获取指定数据实体的上下文类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>实体所属上下文实例</returns>
        IDbContext GetDbContext(Type entityType);

        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        void BeginOrUseTransaction();

        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        /// <param name="cancellationToken">异步取消标记</param>
        /// <returns></returns>
        Task BeginOrUseTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 提交当前上下文的事务更改
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        void Rollback();
    }
}