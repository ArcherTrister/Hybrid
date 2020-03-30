﻿// -----------------------------------------------------------------------
//  <copyright file="MigrationPackBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-03 0:24</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Options;
using Hybrid.Core.Packs;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;

namespace Hybrid.Entity
{
    /// <summary>
    /// 数据迁移模块基类
    /// </summary>
    /// <typeparam name="TDbContext">数据上下文类型</typeparam>
    public abstract class MigrationPackBase<TDbContext> : HybridPack
        where TDbContext : DbContext
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        protected abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            HybridOptions options = provider.GetHybridOptions();
            HybridDbContextOptions contextOptions = options.GetDbContextOptions(typeof(TDbContext));
            if (contextOptions?.DatabaseType != DatabaseType)
            {
                return;
            }

            using (IServiceScope scope = provider.CreateScope())
            {
                TDbContext context = CreateDbContext(scope.ServiceProvider);
                if (context != null && contextOptions.AutoMigrationEnabled)
                {
                    context.CheckAndMigration();
                    DbContextModelCache modelCache = scope.ServiceProvider.GetService<DbContextModelCache>();
                    modelCache?.Set(context.GetType(), context.Model);
                }
            }

            //种子数据
            var seedDataInitializers = provider.GetServices<ISeedDataInitializer>().OrderBy(m => m.Order);
            foreach (ISeedDataInitializer initializer in seedDataInitializers)
            {
                initializer.Initialize();
            }

            IsEnabled = true;
        }

        /// <summary>
        /// 重写实现获取数据上下文实例
        /// </summary>
        /// <param name="scopedProvider">服务提供者</param>
        /// <returns></returns>
        protected abstract TDbContext CreateDbContext(IServiceProvider scopedProvider);
    }
}