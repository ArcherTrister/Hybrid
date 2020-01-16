using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Logging;
using Hybrid.Quartz.Dashboard.Models;
using Hybrid.Quartz.Dashboard.Models.Dtos;
using Hybrid.Quartz.Dashboard.Models.Request;
using Hybrid.Quartz.Dashboard.Repositorys;
using Hybrid.Quartz.Plugins.History;

using Microsoft.AspNetCore.Mvc;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class QuartzController : ControllerBase
    {
        #region Calendars

        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IReadOnlyCollection<CalendarDto>> Calendars(string schedulerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            IReadOnlyCollection<string> calendarNames = await scheduler.GetCalendarNames().ConfigureAwait(false);
            return calendarNames.Select((p, index) => new CalendarDto
            {
                Id = index,
                CalendarName = p
            }).ToList();
        }

        /// <summary>
        /// 日历详情
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="calendarName">日历名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<CalendarDetailDto> CalendarDetails(string schedulerName, string calendarName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            ICalendar calendar = await scheduler.GetCalendar(calendarName).ConfigureAwait(false);
            return CalendarDetailDto.Create(calendar, calendarName);
        }

        /// <summary>
        /// 添加日历
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="calendarName">日历名称</param>
        /// <param name="replace">是否替换</param>
        /// <param name="updateTriggers">是否更新触发器</param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddCalendar(string schedulerName, string calendarName, bool replace, bool updateTriggers)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.AddCalendar(calendarName, null, replace, updateTriggers).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="calendarName">日历名称</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteCalendar(string schedulerName, string calendarName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.DeleteCalendar(calendarName).ConfigureAwait(false);
        }

        ///// <summary>
        ///// 更新日历
        ///// </summary>
        ///// <param name="schedulerName">调度名称</param>
        ///// <param name="calendarName">日历名称</param>
        ///// <returns></returns>
        //[HttpPut]
        //public async Task UpdateCalendar(string schedulerName, string calendarName)
        //{
        //    IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
        //    await scheduler.DeleteCalendar(calendarName).ConfigureAwait(false);
        //}

        #endregion Calendars

        #region Jobs

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse<IReadOnlyList<KeyDto>>> Jobs(GroupMatcherDto groupMatcher)
        {
            AjaxResponse<IReadOnlyList<KeyDto>> ajaxResponse = new AjaxResponse<IReadOnlyList<KeyDto>>();
            try
            {
                IScheduler scheduler = await GetScheduler(groupMatcher.SchedulerName).ConfigureAwait(false);
                GroupMatcher<JobKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetJobGroupMatcher();
                IReadOnlyCollection<JobKey> jobKeys = await scheduler.GetJobKeys(matcher).ConfigureAwait(false);
                ajaxResponse.Result = jobKeys.Select(x => new KeyDto(x)).ToList();
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 任务详情
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="jobGroup">任务分组</param>
        /// <param name="jobName">任务名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResponse<JobDetailDto>> JobDetails(string schedulerName, string jobGroup, string jobName)
        {
            AjaxResponse<JobDetailDto> ajaxResponse = new AjaxResponse<JobDetailDto>();
            try
            {
                IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
                IJobDetail jobDetail = await scheduler.GetJobDetail(new JobKey(jobName, jobGroup)).ConfigureAwait(false);
                ajaxResponse.Result = new JobDetailDto(jobDetail, schedulerName);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 当前执行任务
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResponse<IReadOnlyList<CurrentlyExecutingJobDto>>> CurrentlyExecutingJobs(string schedulerName)
        {
            AjaxResponse<IReadOnlyList<CurrentlyExecutingJobDto>> ajaxResponse = new AjaxResponse<IReadOnlyList<CurrentlyExecutingJobDto>>();
            try
            {
                IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
                IReadOnlyCollection<IJobExecutionContext> currentlyExecutingJobs = await scheduler.GetCurrentlyExecutingJobs().ConfigureAwait(false);
                ajaxResponse.Result = currentlyExecutingJobs.Select(x => new CurrentlyExecutingJobDto(x)).ToList();
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="operateJobRequest">Job操作请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> PauseJob(OperateJobRequest operateJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(operateJobRequest.SchedulerName).ConfigureAwait(false);
                await scheduler.PauseJob(new JobKey(operateJobRequest.JobName, operateJobRequest.JobGroup)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> PauseJobs(GroupMatcherDto groupMatcher)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(groupMatcher.SchedulerName).ConfigureAwait(false);
                GroupMatcher<JobKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetJobGroupMatcher();
                await scheduler.PauseJobs(matcher).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="operateJobRequest">Job操作请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> ResumeJob(OperateJobRequest operateJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(operateJobRequest.SchedulerName).ConfigureAwait(false);
                await scheduler.ResumeJob(new JobKey(operateJobRequest.JobName, operateJobRequest.JobGroup)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> ResumeJobs(GroupMatcherDto groupMatcher)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(groupMatcher.SchedulerName).ConfigureAwait(false);
                GroupMatcher<JobKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetJobGroupMatcher();
                await scheduler.ResumeJobs(matcher).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 触发任务
        /// </summary>
        /// <param name="operateJobRequest">Job操作请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> TriggerJob(OperateJobRequest operateJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(operateJobRequest.SchedulerName).ConfigureAwait(false);
                await scheduler.TriggerJob(new JobKey(operateJobRequest.JobName, operateJobRequest.JobGroup)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="operateJobRequest">Job操作请求模型</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<AjaxResponse> DeleteJob(OperateJobRequest operateJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(operateJobRequest.SchedulerName).ConfigureAwait(false);
                await scheduler.DeleteJob(new JobKey(operateJobRequest.JobName, operateJobRequest.JobGroup)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 中断任务
        /// </summary>
        /// <param name="operateJobRequest">Job操作请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> InterruptJob(OperateJobRequest operateJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(operateJobRequest.SchedulerName).ConfigureAwait(false);
                await scheduler.Interrupt(new JobKey(operateJobRequest.JobName, operateJobRequest.JobGroup)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="createJobRequest">创建任务请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> CreateJob(CreateJobRequest createJobRequest)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                IScheduler scheduler = await GetScheduler(createJobRequest.SchedulerName).ConfigureAwait(false);
                var jobDetail = new JobDetailImpl(createJobRequest.JobName, createJobRequest.JobGroup, Type.GetType(createJobRequest.JobType), createJobRequest.Durable, createJobRequest.RequestsRecovery);
                await scheduler.AddJob(jobDetail, createJobRequest.Replace).ConfigureAwait(false);


                //JobDetailImpl jobWithData = new JobDetailImpl("datajob", "jobgroup", typeof(NoOpJob));
                //jobWithData.JobDataMap["testkey"] = "testvalue";
                //IOperableTrigger triggerWithData = new SimpleTriggerImpl("datatrigger", "triggergroup", 20, TimeSpan.FromSeconds(5));
                //triggerWithData.JobDataMap.Add("testkey", "testvalue");
                //triggerWithData.EndTimeUtc = DateTime.UtcNow.AddYears(10);
                //triggerWithData.StartTimeUtc = DateTime.Now.AddMilliseconds(1000L);
                //sched.ScheduleJob(jobWithData, triggerWithData);
            }
            catch (Exception ex)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = ex.Message };
            }
            return ajaxResponse;
        }

        #endregion Jobs

        #region Schedulers

        /// <summary>
        /// 获取所有调度程序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IReadOnlyList<SchedulerHeaderDto>> AllSchedulers()
        {
            IReadOnlyList<IScheduler> schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            return schedulers.Select(x => new SchedulerHeaderDto(x)).ToList();
        }

        /// <summary>
        /// 调度程序详情
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SchedulerDto> SchedulerDetails(string schedulerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            SchedulerMetaData metaData = await scheduler.GetMetaData().ConfigureAwait(false);
            return new SchedulerDto(scheduler, metaData);
        }

        ///// <summary>
        ///// 启动调度程序
        ///// </summary>
        ///// <param name="schedulerName">调度名称</param>
        ///// <param name="delayMilliseconds">延时毫秒</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task Start(string schedulerName, int? delayMilliseconds = null)
        //{
        //    IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
        //    if (delayMilliseconds == null)
        //    {
        //        await scheduler.Start().ConfigureAwait(false);
        //    }
        //    else
        //    {
        //        await scheduler.StartDelayed(TimeSpan.FromMilliseconds(delayMilliseconds.Value)).ConfigureAwait(false);
        //    }
        //}

        ///// <summary>
        ///// 准备调度程序
        ///// </summary>
        ///// <param name="schedulerName">调度名称</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task Standby(string schedulerName)
        //{
        //    IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
        //    await scheduler.Standby().ConfigureAwait(false);
        //}

        ///// <summary>
        ///// 关闭调度程序
        ///// </summary>
        ///// <param name="schedulerName">调度名称</param>
        ///// <param name="waitForJobsToComplete">等待任务完成</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task Shutdown(string schedulerName, bool waitForJobsToComplete = false)
        //{
        //    IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
        //    await scheduler.Shutdown(waitForJobsToComplete).ConfigureAwait(false);
        //}

        /// <summary>
        /// 清除调度程序
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <returns></returns>
        [HttpPost]
        public async Task Clear(string schedulerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.Clear().ConfigureAwait(false);
        }

        #endregion Schedulers

        #region Servers

        /// <summary>
        /// 获取所有服务器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<ServerHeaderDto> AllServers()
        {
            IReadOnlyList<Server> servers = ServerRepository.LookupAll();

            return servers.Select(x => new ServerHeaderDto(x)).ToList();
        }

        /// <summary>
        /// 服务器详细信息
        /// </summary>
        /// <param name="serverName">服务器名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServerDetailsDto> ServerDetails(string serverName)
        {
            IReadOnlyList<IScheduler> schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            return new ServerDetailsDto(schedulers);
        }

        #endregion Servers

        #region Triggers

        /// <summary>
        /// 获取所有触发器
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IReadOnlyList<KeyDto>> Triggers(string schedulerName, GroupMatcherDto groupMatcher)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            GroupMatcher<TriggerKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetTriggerGroupMatcher();
            IReadOnlyCollection<TriggerKey> jobKeys = await scheduler.GetTriggerKeys(matcher).ConfigureAwait(false);

            return jobKeys.Select(x => new KeyDto(x)).ToList();
        }

        /// <summary>
        /// 触发器详细信息
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="triggerGroup">触发器分组</param>
        /// <param name="triggerName">触发器名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TriggerDetailDto> TriggerDetails(string schedulerName, string triggerGroup, string triggerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            ITrigger trigger = await scheduler.GetTrigger(new TriggerKey(triggerName, triggerGroup)).ConfigureAwait(false);
            ICalendar calendar = trigger.CalendarName != null
                ? await scheduler.GetCalendar(trigger.CalendarName).ConfigureAwait(false)
                : null;
            return TriggerDetailDto.Create(trigger, calendar);
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="triggerGroup">触发器分组</param>
        /// <param name="triggerName">触发器名称</param>
        /// <returns></returns>
        [HttpPost]
        public async Task PauseTrigger(string schedulerName, string triggerGroup, string triggerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.PauseTrigger(new TriggerKey(triggerName, triggerGroup)).ConfigureAwait(false);
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task PauseTriggers(string schedulerName, GroupMatcherDto groupMatcher)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            GroupMatcher<TriggerKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetTriggerGroupMatcher();
            await scheduler.PauseTriggers(matcher).ConfigureAwait(false);
        }

        /// <summary>
        /// 恢复触发器
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="triggerGroup">触发器分组</param>
        /// <param name="triggerName">触发器名称</param>
        /// <returns></returns>
        [HttpPost]
        public async Task ResumeTrigger(string schedulerName, string triggerGroup, string triggerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.ResumeTrigger(new TriggerKey(triggerName, triggerGroup)).ConfigureAwait(false);
        }

        /// <summary>
        /// 恢复触发器
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="groupMatcher">分组匹配</param>
        /// <returns></returns>
        [HttpPost]
        public async Task ResumeTriggers(string schedulerName, GroupMatcherDto groupMatcher)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            GroupMatcher<TriggerKey> matcher = (groupMatcher ?? new GroupMatcherDto()).GetTriggerGroupMatcher();
            await scheduler.ResumeTriggers(matcher).ConfigureAwait(false);
        }

        #endregion Triggers

        #region SchedulerHistory

        private static readonly ILog Logger = LogProvider.For<QuartzController>();

        /// <summary>
        /// 调度历史记录
        /// </summary>
        /// <param name="schedulerName">调度名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResponse<PageResult<ExecutionHistoryEntry>>> SchedulerHistory(string schedulerName, string searchText, int pageSize, int pageNumber, string sortName = "FIRED_TIME", string sortOrder = "desc")
        {
            var result = new AjaxResponse<PageResult<ExecutionHistoryEntry>>();
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            var histStore = scheduler.Context.GetExecutionHistoryStore();
            //JobHistoryDelegate jobHistoryDelegate = DatabaseExecutionHistoryPlugin.Delegate;
            if (histStore == null)
            {
                Logger.ErrorException("Error while retrieving history entries", null);
                result.Success = false;
                result.Error = new ErrorInfo { Message = "历史记录插件尚未启用！" };
            }
            else
            {
                result.Result = await histStore.GetPageJobHistoryEntries(schedulerName, pageNumber, pageSize, sortName +" "+ sortOrder).ConfigureAwait(false);
            }
            return result;
        }

        #endregion SchedulerHistory

        #region 私有方法

        private static async Task<IScheduler> GetScheduler(string schedulerName)
        {
            IScheduler scheduler = await SchedulerRepository.Instance.Lookup(schedulerName).ConfigureAwait(false);
            if (scheduler == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                throw new KeyNotFoundException($"Scheduler {schedulerName} not found!");
            }
            return scheduler;
        }

        #endregion 私有方法
    }
}