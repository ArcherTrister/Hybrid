﻿using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.History
{
    /// <summary>
    /// Logs a history of all job and trigger executions.
    /// </summary>
    /// <author>Marko Lahma</author>
    public class DatabaseExecutionHistoryPlugin : ISchedulerPlugin, IJobListener
    {
        //internal static JobHistoryDelegate Delegate { get; private set; }

        private IScheduler _scheduler;

        private IExecutionHistoryStore _store;

        //private static JobHistoryDelegate Delegate { get; set; }

        public Type StoreType { get; set; }

        /// <summary>
        /// Get the name of the <see cref="IJobListener" />.
        /// </summary>
        /// <value></value>
        public virtual string Name { get; private set; }

        public string TablePrefix { get; set; }
        public string DataSource { get; set; }
        public string DriverDelegateType { get; set; }

        /// <summary>
        /// Called during creation of the <see cref="IScheduler" /> in order to give
        /// the <see cref="ISchedulerPlugin" /> a chance to Initialize.
        /// </summary>
        public virtual Task Initialize(string pluginName,
            IScheduler scheduler,
            CancellationToken cancellationToken = default)
        {
            Name = pluginName;
            //Delegate = new JobHistoryDelegate(DataSource, DriverDelegateType, TablePrefix);
            _scheduler = scheduler;
            _scheduler.ListenerManager.AddJobListener(this, EverythingMatcher<JobKey>.AllJobs());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the associated <see cref="IScheduler" /> is started, in order
        /// to let the plug-in know it can now make calls into the scheduler if it
        /// needs to.
        /// </summary>
        public virtual async Task Start(CancellationToken cancellationToken = default)
        {
            // do nothing...
            _store = _scheduler.Context.GetExecutionHistoryStore();

            if (_store == null)
            {
                if (StoreType != null)
                    _store = (IExecutionHistoryStore)Activator.CreateInstance(StoreType, new string[] { DataSource, DriverDelegateType, TablePrefix });

                if (_store == null)
                    throw new Exception(nameof(StoreType) + " is not set.");

                _scheduler.Context.SetExecutionHistoryStore(_store);
            }

            //_store.SchedulerName = _scheduler.SchedulerName;

            await Task.CompletedTask;
        }

        /// <summary>
        /// Called in order to inform the <see cref="ISchedulerPlugin" /> that it
        /// should free up all of it's resources because the scheduler is shutting
        /// down.
        /// </summary>
        public virtual Task Shutdown(CancellationToken cancellationToken = default)
        {
            // nothing to do...
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Called by the <see cref="IScheduler"/> when a <see cref="IJobDetail"/> is
        ///     about to be executed (an associated <see cref="ITrigger"/> has occurred).
        ///     <para>
        ///         This method will not be invoked if the execution of the Job was vetoed by a
        ///         <see cref="ITriggerListener"/>.
        ///     </para>
        /// </summary>
        /// <seealso cref="JobExecutionVetoed(IJobExecutionContext, CancellationToken)"/>
        public virtual Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return _store.CreateJobHistoryEntry(context, cancellationToken);
        }

        /// <summary>
        /// Called by the <see cref="IScheduler" /> after a <see cref="IJobDetail" />
        /// has been executed, and be for the associated <see cref="ITrigger" />'s
        /// <see cref="IOperableTrigger.Triggered" /> method has been called.
        /// </summary>
        public virtual Task JobWasExecuted(
            IJobExecutionContext context,
            JobExecutionException jobException,
            CancellationToken cancellationToken = default)
        {
            return _store.UpdateJobHistoryEntryError(context, jobException, cancellationToken);
        }

        /// <summary>
        /// Called by the <see cref="IScheduler" /> when a <see cref="IJobDetail" />
        /// was about to be executed (an associated <see cref="ITrigger" />
        /// has occurred), but a <see cref="ITriggerListener" /> vetoed it's
        /// execution.
        /// </summary>
        /// <seealso cref="JobToBeExecuted(IJobExecutionContext, CancellationToken)"/>
        public virtual Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return _store.UpdateJobHistoryEntryVetoed(context, cancellationToken);
        }
    }
}