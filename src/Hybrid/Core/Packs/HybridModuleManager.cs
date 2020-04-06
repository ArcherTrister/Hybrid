using Hybrid.Collections;
using Hybrid.Core.Builders;
using Hybrid.Exceptions;
using Hybrid.Localization;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hybrid.Core.Packs
{
    /// <summary>
    /// Hybrid模块管理器
    /// </summary>
    public class HybridModuleManager : IHybridModuleManager
    {
        private readonly List<HybridPack> _sourceModules;

        /// <summary>
        /// 初始化一个<see cref="HybridModuleManager"/>类型的新实例
        /// </summary>
        public HybridModuleManager()
        {
            _sourceModules = new List<HybridPack>();
            LoadedModules = new List<HybridPack>();
        }

        /// <summary>
        /// 获取 自动检索到的所有模块信息
        /// </summary>
        public IEnumerable<HybridPack> SourceModules => _sourceModules;

        /// <summary>
        /// 获取 最终加载的模块信息集合
        /// </summary>
        public IEnumerable<HybridPack> LoadedModules { get; private set; }

        /// <summary>
        /// 加载模块服务
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <returns></returns>
        public virtual IServiceCollection LoadModules(IServiceCollection services)
        {
            IHybridPackTypeFinder moduleTypeFinder =
                services.GetOrAddTypeFinder<IHybridPackTypeFinder>(assemblyFinder => new HybridPackTypeFinder(assemblyFinder));
            Type[] moduleTypes = moduleTypeFinder.FindAll();
            _sourceModules.Clear();
            _sourceModules.AddRange(moduleTypes.Select(m => (HybridPack)Activator.CreateInstance(m)));

            IHybridBuilder builder = services.GetSingletonInstance<IHybridBuilder>();
            List<HybridPack> modules;
            if (builder.AddModules.Any())
            {
                modules = _sourceModules.Where(m => m.Level == PackLevel.Core)
                    .Union(_sourceModules.Where(m => builder.AddModules.Contains(m.GetType()))).Distinct()
                    .OrderBy(m => m.Level).ThenBy(m => m.Order).ToList();
                List<HybridPack> dependModules = new List<HybridPack>();
                foreach (HybridPack module in modules)
                {
                    Type[] dependModuleTypes = module.GetDependPackTypes();
                    foreach (Type dependModuleType in dependModuleTypes)
                    {
                        HybridPack dependModule = _sourceModules.Find(m => m.GetType() == dependModuleType);
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
            foreach (HybridPack module in LoadedModules)
            {
                //TODO:自动加载
                //services = module.AddAutoServices(services);
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
            Stopwatch watch = Stopwatch.StartNew();

            foreach (HybridPack module in LoadedModules)
            {
                module.UsePack(provider);
                logger.LogInformation($"模块{module.GetType()}加载成功");
            }

            // TODO:初始化国际化
            ILocalizationManager localizationManager = provider.GetRequiredService<ILocalizationManager>();
            localizationManager.Initialize();

            watch.Stop();
            logger.LogInformation($"Hybrid框架初始化完毕，耗时：{watch.Elapsed}");
        }
    }
}
