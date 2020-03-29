// -----------------------------------------------------------------------
//  <copyright file="TableNamePrefixAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-17 14:36</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.Entity
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