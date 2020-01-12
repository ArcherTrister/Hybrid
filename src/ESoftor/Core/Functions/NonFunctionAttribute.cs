// -----------------------------------------------------------------------
//  <copyright file="NonFunction.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-09-24 21:11</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Core.Functions
{
    /// <summary>
    /// 标注当前Action不作为Function信息进行收集
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonFunctionAttribute : Attribute
    { }
}