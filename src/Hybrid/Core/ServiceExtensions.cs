// -----------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2018-07-26 12:22</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Builders;
using Hybrid.Core.Modules;
using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Dependency;
using Hybrid.Domain.Entities;
using Hybrid.Domain.EntityFramework;
using Hybrid.Domain.Uow;
using Hybrid.Reflection;
using Hybrid.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入服务集合扩展
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// 将Hybrid服务，各个<see cref="HybridModule"/>模块的服务添加到服务容器中
        /// </summary>
        public static IServiceCollection AddHybrid<THybridModuleManager>(this IServiceCollection services, Action<IHybridBuilder> builderAction = null)
            where THybridModuleManager : IHybridModuleManager, new()
        {
            Check.NotNull(services, nameof(services));

            IConfiguration configuration = services.GetConfiguration();
            Singleton<IConfiguration>.Instance = configuration;

            //初始化所有程序集查找器
            services.TryAddSingleton<IAllAssemblyFinder>(new AppDomainAllAssemblyFinder());

            IHybridBuilder builder = services.GetSingletonInstanceOrNull<IHybridBuilder>() ?? new HybridBuilder();
            builderAction?.Invoke(builder);
            services.TryAddSingleton<IHybridBuilder>(builder);

            THybridModuleManager manager = new THybridModuleManager();
            services.AddSingleton<IHybridModuleManager>(manager);
            manager.LoadModules(services);

            services.TryAddSingleton<IHybridStartupConfiguration, HybridStartupConfiguration>();

            return services;
        }

        /// <summary>
        /// 获取<see cref="IConfiguration"/>配置信息
        /// </summary>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetSingletonInstanceOrNull<IConfiguration>();
        }

        /// <summary>
        /// 从服务提供者中获取HybridOptions
        /// </summary>
        public static HybridOptions GetHybridOptions(this IServiceProvider provider)
        {
            return provider.GetService<IOptions<HybridOptions>>()?.Value;
        }

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <typeparam name="T">非静态强类型</typeparam>
        /// <returns>日志对象</returns>
        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type">指定类型</param>
        /// <returns>日志对象</returns>
        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        /// <summary>
        /// 获取指定名称的日志对象
        /// </summary>
        public static ILogger GetLogger(this IServiceProvider provider, string name)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(name);
        }

        /// <summary>
        /// 获取指定实体类的上下文所在工作单元
        /// </summary>
        public static IUnitOfWork GetUnitOfWork<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        {
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            return unitOfWorkManager.GetUnitOfWork<TEntity, TKey>();
        }

        /// <summary>
        /// 获取指定实体类型的上下文对象
        /// </summary>
        public static IDbContext GetDbContext<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        {
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            return unitOfWorkManager.GetDbContext<TEntity, TKey>();
        }

        /// <summary>
        /// Hybrid框架初始化，适用于非AspNetCore环境
        /// </summary>
        public static IServiceProvider UseHybrid(this IServiceProvider provider)
        {
            IHybridModuleManager moduleManager = provider.GetService<IHybridModuleManager>();
            moduleManager.UseModule(provider);
            return provider;
        }
    }
}