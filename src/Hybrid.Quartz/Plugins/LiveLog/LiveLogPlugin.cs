using Hybrid.Quartz.Dashboard.Models.Dtos;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

using Quartz;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.LiveLog
{
    public class LiveLogPlugin : ITriggerListener, IJobListener, ISchedulerListener
    {
        private readonly IServiceProvider _serviceProvider;

        public LiveLogPlugin(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IHubContext<LiveLogHub> HubContent => _serviceProvider.GetService<IHubContext<LiveLogHub>>();

        #region ITriggerListener Impl

        /// <summary>
        /// 触发器完成
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="triggerInstructionCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.triggerComplete(new KeyDto(trigger.Key)));
            await HubContent.Clients.All.SendAsync("TriggerComplete", new KeyDto(trigger.Key), cancellationToken);
        }

        /// <summary>
        /// 触发器触发
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.triggerFired(new KeyDto(trigger.Key)));
            await HubContent.Clients.All.SendAsync("TriggerFired", new KeyDto(trigger.Key), cancellationToken);
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.triggerMisfired(new KeyDto(trigger.Key)));
            await HubContent.Clients.All.SendAsync("TriggerMisfired", new KeyDto(trigger.Key), cancellationToken);
        }

        /// <summary>
        /// 否决任务执行
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        #endregion ITriggerListener Impl

        #region IJobListener Impl

        public string Name => "Quartz Live Logs Plugin";

        /// <summary>
        /// 任务执行被否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 要执行的任务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.jobToBeExecuted(new KeyDto(context.JobDetail.Key), new KeyDto(context.Trigger.Key)));
            await HubContent.Clients.All.SendAsync("JobToBeExecuted", new KeyDto(context.JobDetail.Key), new KeyDto(context.Trigger.Key), cancellationToken);
        }

        /// <summary>
        /// 任务已执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.jobWasExecuted(new KeyDto(context.JobDetail.Key), new KeyDto(context.Trigger.Key), jobException?.Message));
            await HubContent.Clients.All.SendAsync("JobWasExecuted", new KeyDto(context.JobDetail.Key), new KeyDto(context.Trigger.Key), jobException?.Message, cancellationToken);
        }

        #endregion IJobListener Impl

        #region ISchedulerListener Impl

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //return Task.CompletedTask;
            await HubContent.Clients.All.SendAsync("JobDeleted", jobKey, cancellationToken);
        }

        /// <summary>
        /// 任务中断
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //return Task.CompletedTask;
            await HubContent.Clients.All.SendAsync("JobInterrupted", jobKey, cancellationToken);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.jobPaused(jobKey));
            await HubContent.Clients.All.SendAsync("JobPaused", jobKey, cancellationToken);
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.jobResumed(jobKey));
            await HubContent.Clients.All.SendAsync("JobResumed", jobKey, cancellationToken);
        }

        /// <summary>
        /// 任务已计划
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 暂停整个分组任务
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 恢复整个分组任务
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 未计划的任务
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.triggerPaused(new KeyDto(triggerKey)));
            await HubContent.Clients.All.SendAsync("TriggerPaused", new KeyDto(triggerKey), cancellationToken);
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            //return SendToClients(x => x.triggerResumed(new KeyDto(triggerKey)));
            await HubContent.Clients.All.SendAsync("TriggerResumed", new KeyDto(triggerKey), cancellationToken);
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        #endregion ISchedulerListener Impl

        //private Task SendToClients(Action<dynamic> action)
        //{
        //    //var context = ConnectionManager.GetHubContext<LiveLogHub>();
        //    action(HubContent.Clients.All);
        //    return Task.CompletedTask;
        //}
    }
}