// -----------------------------------------------------------------------
//  <copyright file="LoginedAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-15 2:44</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.Authorization
{
    /// <summary>
    /// 指定功能需要登录才能访问
    /// </summary>
    public class LoggedInAttribute : Attribute
    { }
}