// -----------------------------------------------------------------------
//  <copyright file="DependsOnModulesAttribute.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:19</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// 定义Hybrid模块依赖
    /// </summary>
    public class DependsOnModulesAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个 Hybrid模块依赖<see cref="DependsOnModulesAttribute"/>类型的新实例
        /// </summary>
        public DependsOnModulesAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }

        /// <summary>
        /// 获取 当前模块的依赖模块类型集合
        /// </summary>
        public Type[] DependedModuleTypes { get; }
    }
}