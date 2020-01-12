// -----------------------------------------------------------------------
//  <copyright file="MvcControllerTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Functions;
using ESoftor.Finders;
using ESoftor.Reflection;

using System;
using System.Linq;
using System.Reflection;

namespace ESoftor.AspNetCore.Mvc
{
    /// <summary>
    /// MVC控制器类型查找器
    /// </summary>
    public class MvcControllerTypeFinder : FinderBase<Type>, IFunctionTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="MvcControllerTypeFinder"/>类型的新实例
        /// </summary>
        public MvcControllerTypeFinder(IAllAssemblyFinder allAssemblyFinder)
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
            return assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsController()).ToArray();
        }
    }
}