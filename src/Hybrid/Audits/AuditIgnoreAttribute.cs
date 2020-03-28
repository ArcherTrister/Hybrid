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