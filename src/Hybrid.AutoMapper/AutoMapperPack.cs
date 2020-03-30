// -----------------------------------------------------------------------
//  <copyright file="AutoMapperPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:23</last-date>
// -----------------------------------------------------------------------

using AutoMapper;
using AutoMapper.Configuration;

using Hybrid.Core.Packs;
using Hybrid.Mapping;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.ComponentModel;
using System.Linq;

using IMapper = Hybrid.Mapping.IMapper;

namespace Hybrid.AutoMapper
{
    /// <summary>
    /// AutoMapper模块
    /// </summary>
    [Description("AutoMapper模块")]
    public class AutoMapperPack : HybridPack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<MapperConfigurationExpression>(new MapperConfigurationExpression());
            services.TryAddSingleton<IMapFromAttributeTypeFinder, MapFromAttributeTypeFinder>();
            services.TryAddSingleton<IMapToAttributeTypeFinder, MapToAttributeTypeFinder>();
            services.AddSingleton<IMapTuple, MapTupleProfile>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            MapperConfigurationExpression cfg = provider.GetService<MapperConfigurationExpression>();

            //获取已注册到IoC的所有Profile
            IMapTuple[] tuples = provider.GetServices<IMapTuple>().ToArray();
            foreach (IMapTuple mapTuple in tuples)
            {
                mapTuple.CreateMap();
                cfg.AddProfile(mapTuple as Profile);
            }

            //各个模块DTO的 IAutoMapperConfiguration 映射实现类
            IAutoMapperConfiguration[] configs = provider.GetServices<IAutoMapperConfiguration>().ToArray();
            foreach (IAutoMapperConfiguration config in configs)
            {
                config.CreateMaps(cfg);
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg);

            IMapper mapper = new AutoMapperMapper(configuration);
            MapperExtensions.SetMapper(mapper);

            IsEnabled = true;
        }
    }
}