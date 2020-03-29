// -----------------------------------------------------------------------
//  <copyright file="IAssemblyFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-15 23:25</last-date>
// -----------------------------------------------------------------------

using System.Reflection;

using Hybrid.Dependency;
using Hybrid.Finders;



namespace Hybrid.Reflection
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    [IgnoreDependency]
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}