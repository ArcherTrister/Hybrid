// -----------------------------------------------------------------------
//  <copyright file="AsyncBarrier.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2016-03-31 23:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Threading.Asyncs
{
    public class AsyncBarrier
    {
        private readonly int _participantCount;
        private int _remainingParticipants;
        private TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public AsyncBarrier(int participantCount)
        {
            if (participantCount <= 0)
            {
                throw new ArgumentOutOfRangeException("participantCount");
            }
            _remainingParticipants = _participantCount = participantCount;
        }

        public Task SignalAndWait()
        {
            var tcs = _tcs;
            if (Interlocked.Decrement(ref _remainingParticipants) == 0)
            {
                _remainingParticipants = _participantCount;
                _tcs = new TaskCompletionSource<bool>();
                tcs.SetResult(true);
            }
            return tcs.Task;
        }

        public class AsyncBarrier1
        {
            private readonly int _participantCount;
            private int _remainingParticipants;
            private ConcurrentStack<TaskCompletionSource<bool>> _waiters;

            public AsyncBarrier1(int participantCount)
            {
                if (participantCount <= 0)
                {
                    throw new ArgumentOutOfRangeException("participantCount");
                }
                _remainingParticipants = _participantCount = participantCount;
                _waiters = new ConcurrentStack<TaskCompletionSource<bool>>();
            }

            public Task SignalAndWait()
            {
                var tcs = new TaskCompletionSource<bool>();
                _waiters.Push(tcs);
                if (Interlocked.Decrement(ref _remainingParticipants) == 0)
                {
                    _remainingParticipants = _participantCount;
                    var waiters = _waiters;
                    _waiters = new ConcurrentStack<TaskCompletionSource<bool>>();
                    Parallel.ForEach(waiters, w => w.SetResult(true));
                }
                return tcs.Task;
            }
        }
    }
}