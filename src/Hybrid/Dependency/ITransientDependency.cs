// -----------------------------------------------------------------------
//  <copyright file="ITransientDependency.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-16 22:36</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.Dependency
{
    /// <summary>
    /// 实现此接口的类型将自动注册为<see cref="ServiceLifetime.Transient"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface ITransientDependency
    { }
}