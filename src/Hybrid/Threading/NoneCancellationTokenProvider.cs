// -----------------------------------------------------------------------
//  <copyright file="NoneCancellationTokenProvider.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-09 22:53</last-date>
// -----------------------------------------------------------------------

using System.Threading;

namespace Hybrid.Threading
{
    /// <summary>
    /// 默认的异步任务取消标识提供者空实现
    /// </summary>
    public class NoneCancellationTokenProvider : ICancellationTokenProvider
    {
        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        public CancellationToken Token { get; } = CancellationToken.None;
    }
}