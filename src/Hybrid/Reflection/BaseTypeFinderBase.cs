// -----------------------------------------------------------------------
//  <copyright file="BaseTypeFinderBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-03 23:59</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;
using Hybrid.Finders;

using System;
using System.Linq;
using System.Reflection;

namespace Hybrid.Reflection
{
    /// <summary>
    /// 指定基类的实现类型查找器基类
    /// </summary>
    /// <typeparam name="TBaseType">要查找类型的基类</typeparam>
    public abstract class BaseTypeFinderBase<TBaseType> : FinderBase<Type>, ITypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="BaseTypeFinderBase{TBaseType}"/>类型的新实例
        /// </summary>
        protected BaseTypeFinderBase(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom<TBaseType>()).Distinct().ToArray();
        }
    }
}