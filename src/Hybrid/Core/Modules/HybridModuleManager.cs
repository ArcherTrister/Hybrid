// -----------------------------------------------------------------------
//  <copyright file="HybridModuleManager.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:18</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Builders;
using Hybrid.Dependency;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// Hybrid模块管理器
    /// </summary>
    public class HybridModuleManager : IHybridModuleManager
    {
        private readonly List<HybridModule> _sourceModules;

        /// <summary>
        /// 初始化一个<see cref="HybridModuleManager"/>类型的新实例
        /// </summary>
        public HybridModuleManager()
        {
            _sourceModules = new List<HybridModule>();
            LoadedModules = new List<HybridModule>();
        }

        /// <summary>
        /// 获取 自动检索到的所有模块信息
        /// </summary>
        public IEnumerable<HybridModule> SourceModules => _sourceModules;

        /// <summary>
        /// 获取 最终加载的模块信息集合
        /// </summary>
        public IEnumerable<HybridModule> LoadedModules { get; private set; }

        /// <summary>
        /// 加载模块服务
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <returns></returns>
        public virtual IServiceCollection LoadModules(IServiceCollection services)
        {
            IHybridModuleTypeFinder moduleTypeFinder =
                services.GetOrAddTypeFinder<IHybridModuleTypeFinder>(assemblyFinder => new HybridModuleTypeFinder(assemblyFinder));
            Type[] moduleTypes = moduleTypeFinder.FindAll();
            _sourceModules.Clear();
            _sourceModules.AddRange(moduleTypes.Select(m => (HybridModule)Activator.CreateInstance(m)));

            IHybridBuilder builder = services.GetSingletonInstance<IHybridBuilder>();
            List<HybridModule> modules;
            if (builder.AddModules.Any())
            {
                modules = _sourceModules.Where(m => m.Level == ModuleLevel.Core)
                    .Union(_sourceModules.Where(m => builder.AddModules.Contains(m.GetType()))).Distinct()
                    .OrderBy(m => m.Level).ThenBy(m => m.Order).ToList();
                List<HybridModule> dependModules = new List<HybridModule>();
                foreach (HybridModule module in modules)
                {
                    Type[] dependModuleTypes = module.GetDependModuleTypes();
                    foreach (Type dependModuleType in dependModuleTypes)
                    {
                        HybridModule dependModule = _sourceModules.Find(m => m.GetType() == dependModuleType);
                        if (dependModule == null)
                        {
                            throw new HybridException($"加载模块{module.GetType().FullName}时无法找到依赖模块{dependModuleType.FullName}");
                        }
                        dependModules.AddIfNotExist(dependModule);
                    }
                }
                modules = modules.Union(dependModules).Distinct().ToList();
            }
            else
            {
                modules = _sourceModules.ToList();
                modules.RemoveAll(m => builder.ExceptModules.Contains(m.GetType()));
            }

            // 按先层级后顺序的规则进行排序
            modules = modules.OrderBy(m => m.Level).ThenBy(m => m.Order).ToList();
            LoadedModules = modules;
            foreach (HybridModule module in LoadedModules)
            {
                services = module.AddServices(services);
            }

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public virtual void UseModule(IServiceProvider provider)
        {
            ILogger logger = provider.GetLogger<HybridModuleManager>();
            logger.LogInformation("Hybrid框架初始化开始");
            DateTime dtStart = DateTime.Now;

            foreach (HybridModule module in LoadedModules)
            {
                module.UseModule(provider);
                logger.LogInformation($"模块{module.GetType()}加载成功");
            }

            ILocalizationManager localizationManager = provider.GetRequiredService<ILocalizationManager>();
            localizationManager.Initialize();

            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            logger.LogInformation($"Hybrid框架初始化完成，耗时：{ts:g}");
        }
    }
}