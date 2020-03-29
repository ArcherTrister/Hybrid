﻿// -----------------------------------------------------------------------
//  <copyright file="MvcFunctionTypeFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-15 1:22</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Hybrid.Authorization.Functions;
using Hybrid.Finders;
using Hybrid.Reflection;


namespace Hybrid.AspNetCore.Mvc
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