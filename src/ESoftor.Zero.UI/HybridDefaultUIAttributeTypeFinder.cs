using ESoftor.Extensions;
using ESoftor.Finders;
using ESoftor.Reflection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESoftor.Zero.UI
{
    /// <summary>
    /// 标注了<see cref="HybridDefaultUIAttribute"/>标签的类型查找器
    /// </summary>
    internal class HybridDefaultUIAttributeTypeFinder : FinderBase<Type>, IHybridDefaultUIAttributeTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="HybridDefaultUIAttributeTypeFinder"/>类型的新实例
        /// </summary>
        public HybridDefaultUIAttributeTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 获取TypeInfo列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TypeInfo> GetTypeInfos()
        {
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);

            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && type.IsAbstract && type.HasAttribute<HybridDefaultUIAttribute>())
                .Distinct()
                .Select(p => p.GetTypeInfo())
                .ToArray();

            //var classes = Assembly.GetEntryAssembly()?.DefinedTypes;
            //return classes.Where(type => type.IsClass && type.IsAbstract && type.HasAttribute<HybridDefaultUIAttribute>());
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && type.IsAbstract && type.HasAttribute<HybridDefaultUIAttribute>()).Distinct().ToArray();
        }
    }
}
