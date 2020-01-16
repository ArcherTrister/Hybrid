// -----------------------------------------------------------------------
//  <copyright file="NoneCancellationTokenProvider.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-09 22:53</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System.Threading;

namespace Hybrid.Threading
{
    /// <summary>
    /// 默认的异步任务取消标识提供者空实现
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class NoneCancellationTokenProvider : ICancellationTokenProvider
    {
        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        public CancellationToken Token { get; } = CancellationToken.None;
    }
}