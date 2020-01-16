using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Collections;
using Hybrid.Extensions;

using Microsoft.EntityFrameworkCore;

using Quartz;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.History
{
    [Serializable]
    public class InMemoryExecutionHistoryStore : IExecutionHistoryStore
    {
        //Dictionary<string, ExecutionHistoryEntry> _data = new Dictionary<string, ExecutionHistoryEntry>();
        private List<ExecutionHistoryEntry> _data = new List<ExecutionHistoryEntry>();

        //DateTime _nextPurgeTime = DateTime.UtcNow;
        //private int _updatesFromLastPurge;

        //private Dictionary<string, long> totalJobsExecutedDic = new Dictionary<string, long>();
        //private Dictionary<string, long> totalJobsFailedDic = new Dictionary<string, long>();
        //private long _totalJobsExecuted = 0;
        //private long _totalJobsFailed = 0;

        //public string SchedulerName { get; set; }

        /// <summary>
        /// 创建执行任务历史记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateJobHistoryEntry(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            var entry = new ExecutionHistoryEntry()
            {
                FireInstanceId = context.FireInstanceId,
                SchedulerInstanceId = context.Scheduler.SchedulerInstanceId,
                SchedulerName = context.Scheduler.SchedulerName,
                ActualFireTimeUtc = context.FireTimeUtc.UtcDateTime,
                ScheduledFireTimeUtc = context.ScheduledFireTimeUtc?.UtcDateTime,
                Recovering = context.Recovering,
                JobName = context.JobDetail.Key.ToString(),
                TriggerName = context.Trigger.Key.ToString(),
            };
            await Create(entry);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UpdateJobHistoryEntryError(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken)
        {
            lock (_data)
            {
                //var entry = await GetJobHistoryEntry(context.Scheduler.SchedulerName, context.FireInstanceId);
                var entry = _data.Where(p => p.SchedulerName.Equals(context.Scheduler.SchedulerName) && p.FireInstanceId.Equals(context.FireInstanceId)).FirstOrDefault();
                if (entry != null)
                {
                    entry.FinishedTimeUtc = DateTime.UtcNow;
                    entry.ErrorMessage = jobException?.GetBaseException()?.Message;
                    entry.Error = jobException != null;
                    //await Create(entry);
                }
                return Task.CompletedTask;
                //if (jobException == null)
                //    await IncrementTotalJobsExecuted(context.Scheduler.SchedulerName);
                //else
                //    await IncrementTotalJobsFailed(context.Scheduler.SchedulerName);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateJobHistoryEntryVetoed(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            var entry = await GetJobHistoryEntry(context.Scheduler.SchedulerName, context.FireInstanceId);
            if (entry != null)
            {
                entry.Vetoed = true;
                await Create(entry);
            }
        }

        public Task<long> GetAllCount(string schedulerName, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                var result = _data.Where(p => p.SchedulerName.Equals(schedulerName)).LongCount();
                return Task.FromResult(result);
            }
        }

        /// <summary>
        /// 根据执行Id获取任务历史记录
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="fireInstanceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ExecutionHistoryEntry> GetJobHistoryEntry(string schedulerName, string fireInstanceId, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                return Task.FromResult(_data.Where(p => p.SchedulerName.Equals(schedulerName) && p.FireInstanceId.Equals(fireInstanceId)).FirstOrDefault());
            }
        }

        public async Task<PageResult<ExecutionHistoryEntry>> GetPageJobHistoryEntries(string schedulerName, int pageIndex, int pageSize, string orderByStr, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<ExecutionHistoryEntry> query = _data.AsQueryable();
                return new PageResult<ExecutionHistoryEntry>
                {
                    TotalRecords = query.WhereIf(!string.IsNullOrEmpty(schedulerName), q => q.SchedulerName.Equals(schedulerName)).LongCount(),
                    Data = await query.WhereIf(!string.IsNullOrEmpty(schedulerName), q => q.SchedulerName.Equals(schedulerName)).MultiOrderBy(orderByStr).Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToListAsync()
                };
            }
            catch (Exception) { }
            return new PageResult<ExecutionHistoryEntry>();
        }

        public Task Purge(string schedulerName, CancellationToken cancellationToken = default)
        {
            //var ids = new HashSet<string>((await FilterLastOfEveryTrigger(schedulerName, 10)).Select(x => x.FireInstanceId));

            lock (_data)
            {
                try
                {
                    _data.RemoveAll(p => p.SchedulerName.Equals(schedulerName));
                }
                catch (ArgumentNullException)
                {
                }
                return Task.CompletedTask;
                //foreach (var key in _data.ToArray())
                //{
                //    if (!ids.Contains(key))
                //        _data.Remove(key);
                //}
            }
        }

        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryJob(string schedulerName, int limitPerJob, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                IEnumerable<ExecutionHistoryEntry> result = _data
                    .Where(x => x.SchedulerName.Equals(schedulerName))
                    .GroupBy(x => x.JobName)
                    .Select(x => x.OrderByDescending(y => y.ActualFireTimeUtc).Take(limitPerJob).Reverse())
                    .SelectMany(x => x).ToArray();
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryTrigger(string schedulerName, int limitPerTrigger, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                IEnumerable<ExecutionHistoryEntry> result = _data
                    .Where(x => x.SchedulerName.Equals(schedulerName))
                    .GroupBy(x => x.TriggerName)
                    .Select(x => x.OrderByDescending(y => y.ActualFireTimeUtc).Take(limitPerTrigger).Reverse())
                    .SelectMany(x => x).ToArray();
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLast(string schedulerName, int limit, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                IEnumerable<ExecutionHistoryEntry> result = _data
                    .Where(x => x.SchedulerName.Equals(schedulerName))
                    .OrderByDescending(y => y.ActualFireTimeUtc).Take(limit).Reverse().ToArray();
                return Task.FromResult(result);
            }
        }

        public Task<long> GetTotalJobsExecuted(string schedulerName, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                long totalJobsExecuted = _data.Where(x => x.SchedulerName.Equals(schedulerName) && x.Error.Equals(false)).LongCount();
                return Task.FromResult(totalJobsExecuted);
            }
        }

        public Task<long> GetTotalJobsFailed(string schedulerName, CancellationToken cancellationToken = default)
        {
            lock (_data)
            {
                long totalJobsFailed = _data.Where(x => x.SchedulerName.Equals(schedulerName) && x.Error.Equals(true)).LongCount();
                return Task.FromResult(totalJobsFailed);
            }
        }

        #region 私有方法

        private async Task Create(ExecutionHistoryEntry entry)
        {
            if (_data.Count >= 100)
            {
                await Purge(entry.SchedulerName);
            }

            lock (_data)
            {
                _data.Add(entry);
            }
        }

        #endregion 私有方法
    }
}