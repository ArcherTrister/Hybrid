using Hybrid.Domain.Entities;

using System;
using System.Collections.Generic;

namespace Hybrid.Audits.Configuration
{
    public interface IAuditingConfiguration : IEnabled
    {
        /// <summary>
        /// 匿名登录是否审计
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        List<Type> IgnoredTypes { get; }

        /// <summary>
        /// 是否保存返回数据
        /// </summary>
        bool SaveReturnValues { get; set; }
    }
}