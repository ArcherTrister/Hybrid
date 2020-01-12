// -----------------------------------------------------------------------
//  <copyright file="NoneCancellationTokenProvider.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-09 22:53</last-date>
// -----------------------------------------------------------------------

using ESoftor.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System.Threading;

namespace ESoftor.Threading
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