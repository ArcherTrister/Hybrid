﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.Core.Configuration
{
    /// <summary>
    /// 审计日志配置
    /// </summary>
    public sealed class AuditingConfiguration : IAuditingConfiguration
    {
        /// <summary>
        /// Creates a new <see cref="AuditingConfiguration"/>.
        /// </summary>
        public AuditingConfiguration()
        {
            IsEnabled = true;
            IgnoredTypes = new List<Type>();
            SaveReturnValues = false;
            IsEnabledForAnonymousUsers = false;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 匿名登录是否审计
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        public List<Type> IgnoredTypes { get; } = new List<Type>();

        /// <summary>
        /// 是否保存返回数据
        /// </summary>
        public bool SaveReturnValues { get; set; }
    }
}
