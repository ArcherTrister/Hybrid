﻿// -----------------------------------------------------------------------
//  <copyright file="IModuleInfoPicker.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 17:13</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Core.ModuleInfos
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