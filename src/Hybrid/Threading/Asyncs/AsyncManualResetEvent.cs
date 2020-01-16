﻿// -----------------------------------------------------------------------
//  <copyright file="AsyncManualResetEvent.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2016-03-31 23:02</last-date>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Threading.Asyncs
{
    /// <summary>
    /// 异步手动重置事件
    /// </summary>
    public class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        /// <summary>
        /// 方法注释
        /// </summary>
        public Task WaitAsync()
        {
            return _tcs.Task;
        }

        public void Set()
        {
            var tcs = _tcs;
            Task.Factory.StartNew(s => ((TaskCompletionSource<bool>)s).TrySetResult(true),
                tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
            tcs.Task.Wait();
        }

        public void Reset()
        {
            while (true)
            {
                var tcs = _tcs;
                if (tcs.Task.IsCompleted || Interlocked.CompareExchange(ref _tcs, new TaskCompletionSource<bool>(), tcs) == tcs)
                {
                    return;
                }
            }
        }
    }
}