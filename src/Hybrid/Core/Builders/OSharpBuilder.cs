// -----------------------------------------------------------------------
//  <copyright file="HybridBuilder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:40</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Hybrid.Collections;
using Hybrid.Core.Options;
using Hybrid.Core.Packs;
using Hybrid.Data;
using Hybrid.Exceptions;


namespace Hybrid.Core.Builders
{
    /// <summary>
    /// Hybrid构建器
    /// </summary>
    public class HybridBuilder : IHybridBuilder
    {
        private readonly List<HybridPack> _source;
        private List<HybridPack> _packs;

        /// <summary>
        /// 初始化一个<see cref="HybridBuilder"/>类型的新实例
        /// </summary>
        public HybridBuilder(IServiceCollection services)
        {
            Services = services;
            _packs = new List<HybridPack>();
            _source = GetAllPacks(services);
        }

        /// <summary>
        /// 获取 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        public IEnumerable<HybridPack> Packs => _packs;

        /// <summary>
        /// 获取 Hybrid选项配置委托
        /// </summary>
        public Action<HybridOptions> OptionsAction { get; private set; }

        /// <summary>
        /// 添加指定模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        public IHybridBuilder AddPack<TPack>() where TPack : HybridPack
        {
            Type type = typeof(TPack);
            if (_packs.Any(m => m.GetType() == type))
            {
                return this;
            }

            HybridPack[] tmpPacks = new HybridPack[_packs.Count];
            _packs.CopyTo(tmpPacks);

            HybridPack pack = _source.FirstOrDefault(m => m.GetType() == type);
            if (pack == null)
            {
                throw new HybridException($"类型为“{type.FullName}”的模块实例无法找到");
            }
            _packs.AddIfNotExist(pack);

            // 添加依赖模块
            Type[] dependTypes = pack.GetDependPackTypes();
            foreach (Type dependType in dependTypes)
            {
                HybridPack dependPack = _source.Find(m => m.GetType() == dependType);
                if (dependPack == null)
                {
                    throw new HybridException($"加载模块{pack.GetType().FullName}时无法找到依赖模块{dependType.FullName}");
                }
                _packs.AddIfNotExist(dependPack);
            }

            // 按先层级后顺序的规则进行排序
            _packs = _packs.OrderBy(m => m.Level).ThenBy(m => m.Order).ToList();

            tmpPacks = _packs.Except(tmpPacks).ToArray();
            foreach (HybridPack tmpPack in tmpPacks)
            {
                AddPack(Services, tmpPack);
            }

            return this;
        }

        /// <summary>
        /// 添加Hybrid选项配置
        /// </summary>
        /// <param name="optionsAction">Hybrid操作选项</param>
        /// <returns>Hybrid构建器</returns>
        public IHybridBuilder AddOptions(Action<HybridOptions> optionsAction)
        {
            Check.NotNull(optionsAction, nameof(optionsAction));
            OptionsAction = optionsAction;
            return this;
        }

        private static List<HybridPack> GetAllPacks(IServiceCollection services)
        {
            IHybridPackTypeFinder packTypeFinder =
                services.GetOrAddTypeFinder<IHybridPackTypeFinder>(assemblyFinder => new HybridPackTypeFinder(assemblyFinder));
            Type[] packTypes = packTypeFinder.FindAll();
            return packTypes.Select(m => (HybridPack)Activator.CreateInstance(m)).ToList();
        }

        private static IServiceCollection AddPack(IServiceCollection services, HybridPack pack)
        {
            Type type = pack.GetType();
            Type serviceType = typeof(HybridPack);

            if (type.BaseType?.IsAbstract == false)
            {
                //移除多重继承的模块
                ServiceDescriptor[] descriptors = services.Where(m =>
                    m.Lifetime == ServiceLifetime.Singleton && m.ServiceType == serviceType
                    && m.ImplementationInstance?.GetType() == type.BaseType).ToArray();
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }
            }

            if (!services.Any(m => m.Lifetime == ServiceLifetime.Singleton && m.ServiceType == serviceType && m.ImplementationInstance?.GetType() == type))
            {
                services.AddSingleton(typeof(HybridPack), pack);
                pack.AddServices(services);
            }

            return services;
        }
    }
}