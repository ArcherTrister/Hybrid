// -----------------------------------------------------------------------
//  <copyright file="IModuleInfoPicker.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-10 20:13</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Authorization.Modules
{
    /// <summary>
    /// 定义模块信息提取器，从程序集中提取模块信息
    /// </summary>
    public interface IModuleInfoPicker
    {
        /// <summary>
        /// 从程序集收集模块信息
        /// </summary>
        /// <returns></returns>
        ModuleInfo[] Pickup();
    }
}