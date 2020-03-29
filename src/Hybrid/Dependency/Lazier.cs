// -----------------------------------------------------------------------
//  <copyright file="Lazier.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-04 23:57</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;


namespace Hybrid.Dependency
{
    /// <summary>
    /// Lazy延迟加载解析器
    /// </summary>
    internal class Lazier<T> : Lazy<T> where T : class
    {
        /// <summary>
        /// 初始化一个<see cref="Lazier{T}"/>类型的新实例
        /// </summary>
        public Lazier(IServiceProvider provider)
            : base(provider.GetRequiredService<T>)
        { }
    }
}