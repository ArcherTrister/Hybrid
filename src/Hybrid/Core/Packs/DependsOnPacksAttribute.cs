// -----------------------------------------------------------------------
//  <copyright file="DependsOnPacksAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:19</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Core.Packs
{
    /// <summary>
    /// 定义Hybrid模块依赖
    /// </summary>
    public class DependsOnPacksAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个 Hybrid模块依赖<see cref="DependsOnPacksAttribute"/>类型的新实例
        /// </summary>
        public DependsOnPacksAttribute(params Type[] dependedPackTypes)
        {
            DependedPackTypes = dependedPackTypes;
        }

        /// <summary>
        /// 获取 当前模块的依赖模块类型集合
        /// </summary>
        public Type[] DependedPackTypes { get; }
    }
}