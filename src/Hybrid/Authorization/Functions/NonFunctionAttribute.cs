// -----------------------------------------------------------------------
//  <copyright file="NonFunction.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-09-24 21:11</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Authorization.Functions
{
    /// <summary>
    /// 标注当前Action不作为Function信息进行收集
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonFunctionAttribute : Attribute
    { }
}