// -----------------------------------------------------------------------
//  <copyright file="DependOnFunctionAttribute.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 0:34</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Core.ModuleInfos
{
    /// <summary>
    /// 模块依赖的功能信息，用于提取模块信息Module时确定模块依赖的功能（模块依赖当前功能和此特性设置的其他功能）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class DependOnFunctionAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="DependOnFunctionAttribute"/>类型的新实例
        /// </summary>
        public DependOnFunctionAttribute(string action)
        {
            Action = action;
        }

        /// <summary>
        /// 获取或设置 区域名称，为null（不设置）则使用当前功能所在区域，如要表示无区域的功能，需设置为空字符串""
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称，为null（不设置）则使用当前功能所在控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 获取 功能名称Action，不能为空
        /// </summary>
        public string Action { get; }
    }
}