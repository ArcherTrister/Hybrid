// -----------------------------------------------------------------------
//  <copyright file="IMapFromTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-13 23:51</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;

namespace Hybrid.Mapping
{
    /// <summary>
    /// 标注了<see cref="MapFromAttribute"/>标签的类型查找器
    /// </summary>
    public interface IMapFromAttributeTypeFinder : ITypeFinder
    { }
}