// -----------------------------------------------------------------------
//  <copyright file="ESoftorModuleTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-09 22:22</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;

using System;
using System.Linq;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// Hybrid模块类型查找器
    /// </summary>
    public class ESoftorModuleTypeFinder : BaseTypeFinderBase<ESoftorModule>, IESoftorModuleTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="ESoftorModuleTypeFinder"/>类型的新实例
        /// </summary>
        public ESoftorModuleTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            //排除被继承的Module实类
            Type[] types = base.FindAllItems();
            Type[] baseModuleTypes = types.Select(m => m.BaseType).Where(m => m != null && m.IsClass && !m.IsAbstract).ToArray();
            return types.Except(baseModuleTypes).ToArray();
        }
    }
}