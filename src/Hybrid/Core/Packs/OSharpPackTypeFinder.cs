// -----------------------------------------------------------------------
//  <copyright file="HybridPackTypeFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-09 22:22</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using Hybrid.Reflection;


namespace Hybrid.Core.Packs
{
    /// <summary>
    /// Hybrid模块类型查找器
    /// </summary>
    public class HybridPackTypeFinder : BaseTypeFinderBase<HybridPack>, IHybridPackTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="HybridPackTypeFinder"/>类型的新实例
        /// </summary>
        public HybridPackTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            //排除被继承的Pack实类
            Type[] types = base.FindAllItems();
            Type[] basePackTypes = types.Select(m => m.BaseType).Where(m => m != null && m.IsClass && !m.IsAbstract).ToArray();
            return types.Except(basePackTypes).ToArray();
        }
    }
}