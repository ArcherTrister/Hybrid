// -----------------------------------------------------------------------
//  <copyright file="IgnoreGenType.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 13:07</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.CodeGenerator
{
    /// <summary>
    /// 标记要在CodeGenerator生成代码时忽略的类型
    /// </summary>
    public class IgnoreGenTypeAttribute : Attribute
    { }
}