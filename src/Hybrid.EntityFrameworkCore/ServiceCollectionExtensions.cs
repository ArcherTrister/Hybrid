// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-27 13:23</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Options;
using Hybrid.Dependency;
using Hybrid.Domain.EntityFramework;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Data.Common;
using System.Linq;

namespace Hybrid.EntityFrameworkCore
{
    /// <summary>
    /// 依赖注入服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 将基于Hybrid数据上下文基类<see cref="DbContextBase"/>上下文类型添加到服务集合中
        /// </summary>
        /// <typeparam name="TDbContext">基于Hybrid数据上下文基类<see cref="DbContextBase"/>上下文类型</typeparam>
        /// <param name="services">依赖注入服务集合</param>
        /// <param name="optionsAction">数据库选项创建配置，将在内置配置后运行</param>
        /// <returns>依赖注入服务集合</returns>
        public static IServiceCollection AddHybridDbContext<TDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction = null) where TDbContext : DbContextBase
        {
            services.AddDbContext<TDbContext>((provider, builder) =>
            {
                Type dbContextType = typeof(TDbContext);
                HybridOptions hybridOptions = provider.GetHybridOptions();
                HybridDbContextOptions hybridDbContextOptions = hybridOptions?.GetDbContextOptions(dbContextType);
                if (hybridDbContextOptions == null)
                {
                    throw new HybridException($"无法找到数据上下文“{dbContextType.DisplayName()}”的配置信息");
                }

                //启用延迟加载
                if (hybridDbContextOptions.LazyLoadingProxiesEnabled)
                {
                    builder = builder.UseLazyLoadingProxies();
                }
                DatabaseType databaseType = hybridDbContextOptions.DatabaseType;

                //处理数据库驱动差异处理
                IDbContextOptionsBuilderDriveHandler driveHandler = provider.GetServices<IDbContextOptionsBuilderDriveHandler>()
                    .FirstOrDefault(m => m.Type == databaseType);
                if (driveHandler == null)
                {
                    throw new HybridException($"无法解析类型为“{databaseType}”的 {typeof(IDbContextOptionsBuilderDriveHandler).DisplayName()} 实例");
                }

                ScopedDictionary scopedDictionary = provider.GetService<ScopedDictionary>();
                string key = $"DnConnection_{hybridDbContextOptions.ConnectionString}";
                DbConnection existingDbConnection = scopedDictionary.GetValue<DbConnection>(key);
                builder = driveHandler.Handle(builder, hybridDbContextOptions.ConnectionString, existingDbConnection);

                //使用模型缓存
                DbContextModelCache modelCache = provider.GetService<DbContextModelCache>();
                IModel model = modelCache?.Get(dbContextType);
                if (model != null)
                {
                    builder = builder.UseModel(model);
                }

                //额外的选项
                optionsAction?.Invoke(provider, builder);
            });
            return services;
        }
    }
}