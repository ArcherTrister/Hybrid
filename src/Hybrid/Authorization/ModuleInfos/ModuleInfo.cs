﻿// -----------------------------------------------------------------------
//  <copyright file="ModuleInfo.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 11:38</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Domain.Entities;
using System;
using System.Diagnostics;

namespace Hybrid.Authorization.ModuleInfos
{
    /// <summary>
    /// 从程序集中提取的模块信息载体，包含模块基本信息和模块依赖的功能信息集合
    /// </summary>
    [DebuggerDisplay("{ToDebugDisplay()}")]
    public class ModuleInfo : IEntityHash
    {
        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 层次序号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 获取或设置 模块位置，父模块Code以点号 . 相连的字符串
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 获取或设置 父级模块名称，需要创建父级模块的时候设置值
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 获取或设置 依赖功能
        /// </summary>
        public IFunction[] DependOnFunctions { get; set; } = new IFunction[0];

        private string ToDebugDisplay()
        {
            return $"{Name}[{Code}]({Position}),FunctionCount:{DependOnFunctions.Length}";
        }

        #region Overrides of Object

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ModuleInfo info))
            {
                return false;
            }
            return $"{info.Position}.{info.Code}" == $"{Position}.{Code}";
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Code);
        }

        #endregion Overrides of Object
    }
}