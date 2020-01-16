using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.History
{
    public class InMemoryExecutionHistoryPlugin : ISchedulerPlugin, IJobListener
    {
        private IScheduler _scheduler;
        private IExecutionHistoryStore _store;

        public string Name { get; set; }
        public Type StoreType { get; set; }

        public Task Initialize(string pluginName, IScheduler scheduler, CancellationToken cancellationToken = default(CancellationToken))
        {
            Name = pluginName;
            _scheduler = scheduler;
            _scheduler.ListenerManager.AddJobListener(this, EverythingMatcher<JobKey>.AllJobs());

            return Task.FromResult(0);
        }

        public async Task Start(CancellationToken cancellationToken = default(CancellationToken))
        {
            _store = _scheduler.Context.GetExecutionHistoryStore();

            if (_store == null)
            {
                if (StoreType != null)
                    _store = (IExecutionHistoryStore)Activator.CreateInstance(StoreType);

                if (_store == null)
                    throw new Exception(nameof(StoreType) + " is not set.");

                _scheduler.Context.SetExecutionHistoryStore(_store);
            }

            //_store.SchedulerName = _scheduler.SchedulerName;

            //await _store.Purge();

            await Task.CompletedTask;
        }

        public Task Shutdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _store.CreateJobHistoryEntry(context, cancellationToken);
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _store.UpdateJobHistoryEntryError(context, jobException, cancellationToken);
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _store.UpdateJobHistoryEntryVetoed(context, cancellationToken);
        }
    }
}
