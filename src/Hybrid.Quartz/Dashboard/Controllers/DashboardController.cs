using Hybrid.Extensions;
using Hybrid.Quartz.Dashboard.Models;
using Hybrid.Quartz.Dashboard.Models.Dtos;
using Hybrid.Quartz.Extensions;
using Hybrid.Quartz.Plugins.History;
using Hybrid.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Quartz;
using Quartz.Impl.Matchers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 仪表盘
    /// </summary>
    public class DashboardController : QuartzBaseController
    {
        private readonly ISchedulerManager _schedulerManager;

        public DashboardController(ISchedulerManager schedulerManager)
        {
            _schedulerManager = schedulerManager;
        }

        public async Task<IActionResult> Index()
        {
            SchedulerHeaderDto schedulerHeader = await _schedulerManager.GetCurrentScheduler(LocalSchedulerName);

            string schedulerName = schedulerHeader.Name;

            string cookieValue = Crypto.DesEncrypt(schedulerName);

            HttpContext.Response.Cookies.Append(
                HybridConsts.SchedulerCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(2),
                    HttpOnly = true
                }
            );

            IScheduler Scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            IEnumerable<object> pausedJobGroups = null;
            IEnumerable<object> pausedTriggerGroups = null;
            IEnumerable<ExecutionHistoryEntry> execHistory = null;

            var histStore = Scheduler.Context.GetExecutionHistoryStore();
            var metadata = await Scheduler.GetMetaData();
            var jobKeys = await Scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            var triggerKeys = await Scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
            var currentlyExecutingJobs = await Scheduler.GetCurrentlyExecutingJobs();

            try
            {
                pausedJobGroups = await GetGroupPauseState(await Scheduler.GetJobGroupNames(), async x => await Scheduler.IsJobGroupPaused(x));
            }
            catch (NotImplementedException) { }

            try
            {
                pausedTriggerGroups = await GetGroupPauseState(await Scheduler.GetTriggerGroupNames(), async x => await Scheduler.IsTriggerGroupPaused(x));
            }
            catch (NotImplementedException) { }

            long failedJobs = 0;
            long totalJobs = 0;
            long executedJobs = metadata.NumberOfJobsExecuted;

            if (histStore != null)
            {
                execHistory = await histStore?.FilterLast(schedulerName, 10);
                executedJobs = await histStore?.GetTotalJobsExecuted(schedulerName);
                failedJobs = await histStore?.GetTotalJobsFailed(schedulerName);
                totalJobs = await histStore?.GetAllCount(schedulerName);
            }

            var histogram = execHistory.ToHistogram(detailed: true) ?? Histogram.CreateEmpty();
            histogram.Layout();

            histogram.BarWidth = 14;

            return View(new DashboardDto
            {
                History = histogram,
                MetaData = metadata,
                RunningSince = metadata.RunningSince != null ? metadata.RunningSince.Value.UtcDateTime.ToDefaultFormat() + " UTC" : "N / A",
                MachineName = Environment.MachineName,
                CommandLine = Environment.CommandLine,
                JobsCount = jobKeys.Count,
                TriggerCount = triggerKeys.Count,
                ExecutingJobs = currentlyExecutingJobs.Count,
                ExecutedJobs = executedJobs,
                FailedJobs = failedJobs,
                TotalJobs = totalJobs,
                JobGroups = pausedJobGroups,
                TriggerGroups = pausedTriggerGroups,
                HistoryEnabled = histStore != null,
            });

            //return View();
        }

        private async Task<IEnumerable<object>> GetGroupPauseState(IEnumerable<string> groups, Func<string, Task<bool>> func)
        {
            var result = new List<object>();

            foreach (var name in groups.OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase))
                result.Add(new { Name = name, IsPaused = await func(name) });

            return result;
        }
    }
}