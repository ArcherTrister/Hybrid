// -----------------------------------------------------------------------
//  <copyright file="RoleLimitAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-15 2:45</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.Authorization
{
    /// <summary>
    /// 指定功能只允许特定角色可以访问
    /// </summary>
    public class RoleLimitAttribute : Attribute
    { }
}