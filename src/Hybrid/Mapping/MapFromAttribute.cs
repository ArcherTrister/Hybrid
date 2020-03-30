// -----------------------------------------------------------------------
//  <copyright file="MapFromAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-14 0:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using System;

namespace Hybrid.Mapping
{
    /// <summary>
    /// 标注当前类型从源类型的Mapping映射关系
    /// </summary>
    public class MapFromAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="MapFromAttribute"/>类型的新实例
        /// </summary>
        public MapFromAttribute(params Type[] sourceTypes)
        {
            Check.NotNull(sourceTypes, nameof(sourceTypes));
            SourceTypes = sourceTypes;
        }

        /// <summary>
        /// 源类型
        /// </summary>
        public Type[] SourceTypes { get; }
    }
}