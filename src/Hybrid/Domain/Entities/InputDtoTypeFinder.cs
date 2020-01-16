// -----------------------------------------------------------------------
//  <copyright file="InputDtoTypeFinder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;
using Hybrid.Finders;
using Hybrid.Reflection;

using System;
using System.Linq;
using System.Reflection;

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// <see cref="IInputDto{T}"/>类型查找器
    /// </summary>
    public class InputDtoTypeFinder : FinderBase<Type>, IInputDtoTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="InputDtoTypeFinder"/>类型的新实例
        /// </summary>
        public InputDtoTypeFinder(IAllAssemblyFinder allAssemblyFinder)
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
                .Where(type => type.IsDeriveClassFrom(typeof(IInputDto<>))).Distinct().ToArray();
        }
    }
}