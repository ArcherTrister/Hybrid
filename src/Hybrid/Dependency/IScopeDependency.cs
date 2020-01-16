// -----------------------------------------------------------------------
//  <copyright file="IScopeDependency.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-16 22:34</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.Dependency
{
    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="ServiceLifetime.Scoped"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface IScopeDependency
    { }
}