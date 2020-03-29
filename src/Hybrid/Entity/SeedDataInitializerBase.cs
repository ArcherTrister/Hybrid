﻿// -----------------------------------------------------------------------
//  <copyright file="SeedDataInitializerBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-06 21:53</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;


namespace Hybrid.Entity
{
    /// <summary>
    /// 种子数据初始化基类
    /// </summary>
    public abstract class SeedDataInitializerBase<TEntity, TKey> : ISeedDataInitializer
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IServiceProvider _rootProvider;

        /// <summary>
        /// 初始化一个<see cref="SeedDataInitializerBase{TEntity, TKey}"/>类型的新实例
        /// </summary>
        protected SeedDataInitializerBase(IServiceProvider rootProvider)
        {
            _rootProvider = rootProvider;
        }

        /// <summary>
        /// 获取 种子数据初始化的顺序
        /// </summary>
        public virtual int Order => 0;

        /// <summary>
        /// 初始化种子数据
        /// </summary>
        public void Initialize()
        {
            TEntity[] entities = SeedData();
            SyncToDatabase(entities);
        }

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected abstract TEntity[] SeedData();

        /// <summary>
        /// 重写以提供判断某个实体是否存在的表达式
        /// </summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> ExistingExpression(TEntity entity);

        /// <summary>
        /// 将种子数据初始化到数据库
        /// </summary>
        /// <param name="entities"></param>
        protected virtual void SyncToDatabase(TEntity[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                return;
            }

            _rootProvider.BeginUnitOfWorkTransaction(provider =>
                {
                    IRepository<TEntity, TKey> repository = provider.GetService<IRepository<TEntity, TKey>>();
                    foreach (TEntity entity in entities)
                    {
                        if (repository.CheckExists(ExistingExpression(entity)))
                        {
                            continue;
                        }

                        repository.Insert(entity);
                    }
                },
                true);
        }
    }
}