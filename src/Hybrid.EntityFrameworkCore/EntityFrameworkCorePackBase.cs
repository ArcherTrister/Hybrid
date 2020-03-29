// -----------------------------------------------------------------------
//  <copyright file="EntityFrameworkCorePack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 15:57</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Hybrid.Authorization.EntityInfos;
using Hybrid.Core.Packs;
using Hybrid.EventBuses;


namespace Hybrid.Entity
{
    /// <summary>
    /// EntityFrameworkCore基模块
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack), typeof(EntityInfoPack))]
    public abstract class EntityFrameworkCorePackBase : HybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IAuditEntityProvider, AuditEntityProvider>();
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.TryAddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.TryAddSingleton<IEntityConfigurationTypeFinder, EntityConfigurationTypeFinder>();
            services.TryAddSingleton<IEntityManager, EntityManager>();
            services.AddSingleton<DbContextModelCache>();
            services.AddHybridDbContext<DefaultDbContext>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            IEntityManager manager = provider.GetService<IEntityManager>();
            manager?.Initialize();
            IsEnabled = true;
        }
    }
}