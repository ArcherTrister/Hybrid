// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkManagerExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Options;
using ESoftor.Domain.Entities;
using ESoftor.Domain.EntityFramework;
using ESoftor.Domain.Repositories;
using ESoftor.Exceptions;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Domain.Uow
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
                throw new ESoftorException($"类型“{entityType}”不是实体类型");
            }
            IUnitOfWork unitOfWork = unitOfWorkManager.GetUnitOfWork(entityType);
            return unitOfWork?.GetDbContext(entityType);
        }

        /// <summary>
        /// 获取指定实体类型的数据上下文选项
        /// </summary>
        public static ESoftorDbContextOptions GetDbContextResolveOptions<TEntity, TKey>(this IUnitOfWorkManager unitOfWorkManager) where TEntity : IEntity<TKey>
        {
            Type entityType = typeof(TEntity);
            return unitOfWorkManager.GetDbContextResolveOptions(entityType);
        }

        /// <summary>
        /// 获取指定实体类型的数据上下文选项
        /// </summary>
        public static ESoftorDbContextOptions GetDbContextResolveOptions(this IUnitOfWorkManager unitOfWorkManager, Type entityType)
        {
            Type dbContextType = unitOfWorkManager.GetDbContextType(entityType);
            ESoftorDbContextOptions dbContextOptions = unitOfWorkManager.ServiceProvider.GetESoftorOptions()?.GetDbContextOptions(dbContextType);
            if (dbContextOptions == null)
            {
                throw new ESoftorException($"无法找到数据上下文“{dbContextType}”的配置信息");
            }
            return dbContextOptions;
        }

        /// <summary>
        /// 获取指定实体类型的Sql执行器
        /// </summary>
        public static ISqlExecutor<TEntity, TKey> GetSqlExecutor<TEntity, TKey>(this IUnitOfWorkManager unitOfWorkManager) where TEntity : IEntity<TKey>
        {
            ESoftorDbContextOptions options = unitOfWorkManager.GetDbContextResolveOptions(typeof(TEntity));
            DatabaseType databaseType = options.DatabaseType;
            IList<ISqlExecutor<TEntity, TKey>> executors = unitOfWorkManager.ServiceProvider.GetServices<ISqlExecutor<TEntity, TKey>>().ToList();
            return executors.FirstOrDefault(m => m.DatabaseType == databaseType);
        }
    }
}