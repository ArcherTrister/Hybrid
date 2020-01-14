// -----------------------------------------------------------------------
//  <copyright file="DependencyTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-31 20:53</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;
using ESoftor.Finders;
using ESoftor.Reflection;

using System;
using System.Linq;

namespace ESoftor.Dependency
{
    /// <summary>
    /// 依赖注入类型查找器
    /// </summary>
    public class DependencyTypeFinder : FinderBase<Type>, IDependencyTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="DependencyTypeFinder"/>类型的新实例
        /// </summary>
        public DependencyTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type[] baseTypes = new[] { typeof(ISingletonDependency), typeof(IScopeDependency), typeof(ITransientDependency) };
            Type[] types = _allAssemblyFinder.FindAll(true).SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && !type.HasAttribute<IgnoreDependencyAttribute>()
                    && (baseTypes.Any(b => b.IsAssignableFrom(type)) || type.HasAttribute<DependencyAttribute>()))
                .ToArray();
            return types;
        }
    }
}