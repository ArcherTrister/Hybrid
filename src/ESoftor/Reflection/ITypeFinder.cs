// -----------------------------------------------------------------------
//  <copyright file="ITypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-15 23:23</last-date>
// -----------------------------------------------------------------------

using ESoftor.Dependency;
using ESoftor.Finders;

using System;

namespace ESoftor.Reflection
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    [IgnoreDependency]
    public interface ITypeFinder : IFinder<Type>
    { }
}