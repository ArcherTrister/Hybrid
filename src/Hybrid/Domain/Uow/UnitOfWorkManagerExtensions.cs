// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkManagerExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Options;
using Hybrid.Domain.Entities;
using Hybrid.Domain.EntityFramework;
using Hybrid.Domain.Repositories;
using Hybrid.Exceptions;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Domain.Uow
{
    /// <summary>
    /// <see cref="IUnitOfWorkManager"/>扩展方法
    /// </summary>
    public static class UnitOfWorkManagerExtensions
    {
        /// <summary>
        /// 获取指定实体所在的上下文对象
        /// </summary>
        public static IDbContext GetDbContext<TEntity, TKey>(this IUnitOfWorkManager unitOfWorkManager) where TEntity : IEntity<TKey>
        {
            Type entityType = typeof(TEntity);
            return unitOfWorkManager.GetDbContext(entityType);
        }

        /// <summary>
        /// 获取指定实体类型所在的上下文对象
        /// </summary>
        public static IDbContext GetDbContext(this IUnitOfWorkManager unitOfWorkManager, Type entityType)
        {
            if (!entityType.IsEntityType())
            {
                throw new HybridException($"类型“{entityType}”不是实体类型");
            }
            IUnitOfWork unitOfWork = unitOfWorkManager.GetUnitOfWork(entityType);
            return unitOfWork?.GetDbContext(entityType);
        }

        /// <summary>
        /// 获取指定实体类型的数据上下文选项
        /// </summary>
        public static HybridDbContextOptions GetDbContextResolveOptions<TEntity, TKey>(this IUnitOfWorkManager unitOfWorkManager) where TEntity : IEntity<TKey>
        {
            Type entityType = typeof(TEntity);
            return unitOfWorkManager.GetDbContextResolveOptions(entityType);
        }

        /// <summary>
        /// 获取指定实体类型的数据上下文选项
        /// </summary>
        public static HybridDbContextOptions GetDbContextResolveOptions(this IUnitOfWorkManager unitOfWorkManager, Type entityType)
        {
            Type dbContextType = unitOfWorkManager.GetDbContextType(entityType);
            HybridDbContextOptions dbContextOptions = unitOfWorkManager.ServiceProvider.GetHybridOptions()?.GetDbContextOptions(dbContextType);
            if (dbContextOptions == null)
            {
                throw new HybridException($"无法找到数据上下文“{dbContextType}”的配置信息");
            }
            return dbContextOptions;
        }

        /// <summary>
        /// 获取指定实体类型的Sql执行器
        /// </summary>
        public static ISqlExecutor<TEntity, TKey> GetSqlExecutor<TEntity, TKey>(this IUnitOfWorkManager unitOfWorkManager) where TEntity : IEntity<TKey>
        {
            HybridDbContextOptions options = unitOfWorkManager.GetDbContextResolveOptions(typeof(TEntity));
            DatabaseType databaseType = options.DatabaseType;
            IList<ISqlExecutor<TEntity, TKey>> executors = unitOfWorkManager.ServiceProvider.GetServices<ISqlExecutor<TEntity, TKey>>().ToList();
            return executors.FirstOrDefault(m => m.DatabaseType == databaseType);
        }
    }
}