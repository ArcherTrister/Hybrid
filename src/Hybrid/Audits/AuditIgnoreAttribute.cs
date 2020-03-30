// -----------------------------------------------------------------------
//  <copyright file="AuditIgnoreAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-26 15:06</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Audits
{
    /// <summary>
    /// 标记在审计中忽略的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AuditIgnoreAttribute : Attribute
    { }
}