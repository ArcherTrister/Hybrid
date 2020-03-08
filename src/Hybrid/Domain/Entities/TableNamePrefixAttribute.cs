using System;

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// 表名前缀特性，用于给实体类指定生成的表名前缀
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableNamePrefixAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="TableNamePrefixAttribute"/>类型的新实例
        /// </summary>
        public TableNamePrefixAttribute(string prefix)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// 获取 表名前缀
        /// </summary>
        public string Prefix { get; }
    }
}