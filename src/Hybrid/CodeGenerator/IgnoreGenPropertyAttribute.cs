// -----------------------------------------------------------------------
//  <copyright file="IgnoreGenPropertyAttribute.cs" company="com.esoftor">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 13:08</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.CodeGenerator
{
    /// <summary>
    /// 标记要在CodeGenerator生成代码时忽略的属性
    /// </summary>
    public class IgnoreGenPropertyAttribute : Attribute
    { }
}