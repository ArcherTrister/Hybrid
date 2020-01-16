﻿// -----------------------------------------------------------------------
//  <copyright file="AsyncCountdownEvent.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2016-03-31 23:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Threading.Asyncs
{
    public class AsyncCountdownEvent
    {
        private readonly AsyncManualResetEvent _resetEvent = new AsyncManualResetEvent();
        private int _count;

        public AsyncCountdownEvent(int initialCount)
        {
            if (initialCount <= 0)
            {
                throw new ArgumentOutOfRangeException("initialCount");
            }
            _count = initialCount;
        }

        public Task WaitAsync()
        {
            return _resetEvent.WaitAsync();
        }

        public void Signal()
        {
            if (_count <= 0)
            {
                throw new InvalidOperationException();
            }

            int newCount = Interlocked.Decrement(ref _count);
            if (newCount == 0)
            {
                _resetEvent.Set();
            }
            else if (newCount < 0)
            {
                throw new InvalidOperationException();
            }
        }

        public Task SignalAndWait()
        {
            Signal();
            return WaitAsync();
        }
    }
}