using Hybrid.AspNetCore.Mvc.Models;

using Quartz;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.History
{
    public interface IExecutionHistoryStore
    {
        ///// <summary>
        ///// 获取调度名称
        ///// </summary>
        //string SchedulerName { get; set; }

        /// <summary>
        /// 创建任务执行历史记录
        /// </summary>
        /// <param name="context">当前执行任务</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateJobHistoryEntry(IJobExecutionContext context, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新任务执行历史记录
        /// </summary>
        /// <param name="context">当前执行任务</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateJobHistoryEntryVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新任务执行历史记录
        /// </summary>
        /// <param name="context">当前执行任务</param>
        /// <param name="jobException">当前执行任务失败异常</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateJobHistoryEntryError(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取执行任务总数
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetAllCount(string schedulerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据执行Id获取任务执行历史记录
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="fireInstanceId">当前执行任务Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExecutionHistoryEntry> GetJobHistoryEntry(string schedulerName, string fireInstanceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页获取任务执行历史记录
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="orderByStr">排序字符串</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PageResult<ExecutionHistoryEntry>> GetPageJobHistoryEntries(
            string schedulerName,
            int pageIndex, int pageSize,
            string orderByStr,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 清空任务执行历史记录
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Purge(string schedulerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据Job分组获取任务执行历史记录列表
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="limitPerJob">获取数目</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryJob(string schedulerName, int limitPerJob, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据Trigger分组获取任务执行历史记录列表
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="limitPerTrigger">获取数目</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryTrigger(string schedulerName, int limitPerTrigger, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据默认排序获取任务执行历史记录列表
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="limit">获取数目</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ExecutionHistoryEntry>> FilterLast(string schedulerName, int limit, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取任务执行成功总次数
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetTotalJobsExecuted(string schedulerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取任务执行失败总次数
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetTotalJobsFailed(string schedulerName, CancellationToken cancellationToken = default);
    }
}
