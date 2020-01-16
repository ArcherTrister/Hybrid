// -----------------------------------------------------------------------
//  <copyright file="AuditingConfiguration.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Hybrid.Audits
{
    /// <summary>
    /// 审计日志配置
    /// </summary>
    public sealed class AuditingConfiguration
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 匿名登录是否审计
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; } = false;

        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        public List<Type> IgnoredTypes { get; } = new List<Type>();

        /// <summary>
        /// 是否保存返回数据
        /// </summary>
        public bool SaveReturnValues { get; set; } = false;
    }
}