// -----------------------------------------------------------------------
//  <copyright file="RedisPackCore.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 16:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hybrid.Redis
{
    /// <summary>
    /// Redis模块基类
    /// </summary>
    public abstract class RedisPackBase : HybridPack
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
            IConfiguration configuration = services.GetConfiguration();

            string config = configuration["Hybrid:Redis:Configuration"];
            if (config.IsNullOrEmpty())
            {
                throw new HybridException("配置文件中Redis节点的Configuration不能为空");
            }
            string name = configuration["Hybrid:Redis:InstanceName"].CastTo("RedisName");

            services.RemoveAll(typeof(IDistributedCache));
            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = config;
                opts.InstanceName = name;
            });

            return services;
        }
    }
}