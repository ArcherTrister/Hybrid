// -----------------------------------------------------------------------
//  <copyright file="EntityFrameworkCoreModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 15:57</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Domain.Repositories;
using Hybrid.EntityFrameworkCore.Defaults;
using Hybrid.EventBuses;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Hybrid.EntityFrameworkCore
{
    /// <summary>
    /// EntityFrameworkCore基模块
    /// </summary>
    [DependsOnModules(typeof(EventBusModule))]
    public abstract class EntityFrameworkCoreModuleBase : HybridModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddHybridDbContext<DefaultDbContext>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            IEntityManager manager = provider.GetService<IEntityManager>();
            manager?.Initialize();
            IsEnabled = true;
        }
    }
}