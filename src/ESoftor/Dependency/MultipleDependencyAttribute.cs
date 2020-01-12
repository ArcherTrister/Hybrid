// -----------------------------------------------------------------------
//  <copyright file="MiltipleDependencyAttribute.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 0:34</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Dependency
{
    /// <summary>
    /// 标记允许多重注入，即一个接口可以注入多个实例
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class MultipleDependencyAttribute : Attribute
    { }
}