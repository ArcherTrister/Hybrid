// -----------------------------------------------------------------------
//  <copyright file="ICancellationTokenProvider.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-09 22:54</last-date>
// -----------------------------------------------------------------------

using System.Threading;


namespace Hybrid.Threading
{
    /// <summary>
    /// 定义异步任务取消标识提供器
    /// </summary>
    public interface ICancellationTokenProvider
    {
        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        CancellationToken Token { get; }
    }
}