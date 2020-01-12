﻿// -----------------------------------------------------------------------
//  <copyright file="IMapToTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-13 23:52</last-date>
// -----------------------------------------------------------------------

using ESoftor.Reflection;

namespace ESoftor.Mapping
{
    /// <summary>
    /// 标注了<see cref="MapToAttribute"/>标签的类型查找器
    /// </summary>
    public interface IMapToAttributeTypeFinder : ITypeFinder
    { }
}