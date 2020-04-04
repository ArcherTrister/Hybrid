// -----------------------------------------------------------------------
//  <copyright file="KeyValueSeedDataInitializer.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-07 0:24</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Systems;
using Hybrid.Dependency;
using Hybrid.Entity;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq.Expressions;

namespace LeXun.Demo.Systems
{
    [Dependency(ServiceLifetime.Singleton)]
    public class KeyValueSeedDataInitializer : SeedDataInitializerBase<KeyValue, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="SeedDataInitializerBase{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public KeyValueSeedDataInitializer(IServiceProvider rootProvider)
            : base(rootProvider)
        { }

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected override KeyValue[] SeedData()
        {
            return new[]
            {
                new KeyValue(SystemSettingKeys.SiteName, "HYBRID"),
                new KeyValue(SystemSettingKeys.SiteDescription, "Hybrid with AspNetCore & Angular"),
            };
        }

        /// <summary>
        /// 重写以提供判断某个实体是否存在的表达式
        /// </summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected override Expression<Func<KeyValue, bool>> ExistingExpression(KeyValue entity)
        {
            return m => m.Key == entity.Key;
        }
    }
}