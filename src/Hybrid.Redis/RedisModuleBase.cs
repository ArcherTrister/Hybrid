// -----------------------------------------------------------------------
//  <copyright file="RedisModuleBase.cs" company="Hybrid开源团队">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 16:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Core.Options;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Hybrid.Redis
{
    /// <summary>
    /// Redis模块基类
    /// </summary>
    public abstract class RedisModuleBase : HybridModule
    {
        private bool _enabled = false;

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
            IConfiguration configuration = services.GetConfiguration();
            RedisOptions redisOptions = configuration.GetSection("Hybrid:Redis").Get<RedisOptions>();
            _enabled = redisOptions.IsEnabled;
            if (!_enabled)
            {
                return services;
            }
            services.RemoveAll(typeof(IDistributedCache));
            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = redisOptions.Configuration;
                opts.InstanceName = redisOptions.InstanceName;
            });

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            IsEnabled = _enabled;
        }
    }
}