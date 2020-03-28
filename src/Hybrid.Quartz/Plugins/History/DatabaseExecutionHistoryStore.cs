using Hybrid.Application.Services.Dtos;

using Quartz;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.AdoJobStore.Common;
using Quartz.Simpl;
using Quartz.Spi;
using Quartz.Util;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Plugins.History
{
    public class DatabaseExecutionHistoryStore : IExecutionHistoryStore
    {
        //private static readonly ILog log = LogProvider.For<JobHistoryDelegate>();
        private StdAdoDelegate _driverDelegate;

        private readonly string _delegateTypeName;
        private Type _delegateType = typeof(StdAdoDelegate);
        private readonly string _tablePrefix;
        private readonly string _dataSource;
        private readonly string _provider;

        protected ITypeLoadHelper TypeLoadHelper { get; private set; }

        public DatabaseExecutionHistoryStore(string dataSource, string delegateTypeName, string tablePrefix, string provider)
        {
            _dataSource = dataSource;
            _provider = provider;
            _delegateTypeName = delegateTypeName;
            _tablePrefix = tablePrefix ?? AdoConstants.DefaultTablePrefix;
            TypeLoadHelper = new SimpleTypeLoadHelper();
            TypeLoadHelper.Initialize();
        }

        //public string SchedulerName { get; set; }

        //        private const string PropertySqlInsertJobExecuted =
        //"INSERT INTO {0}JOB_HISTORY (SCHED_NAME, Scheduler_Instance_Id, TRIGGER_NAME, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed," +
        //"TRIGGER_GROUP, JOB_NAME, JOB_GROUP, SCHED_TIME, FIRED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE)  VALUES (@schedulerName, @schedulerInstanceName, @triggerName, @fireInstanceId, @scheduledFireTimeUtc, @actualFireTimeUtc, @finishedTimeUtc, @recovering, @vetoed, @triggerGroup, @jobName, @jobGroup, @scheduledTime, @firedTime, @runTime, @error, @errorMessage)";

        private const string PropertySqlInsertJobExecuted =
"INSERT INTO {0}JOB_HISTORY (SCHED_NAME, Scheduler_Instance_Id, TRIGGER_NAME, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed," +
"TRIGGER_GROUP, JOB_NAME, JOB_GROUP, SCHED_TIME, FIRED_TIME, RUN_TIME, ERROR)  VALUES (@schedulerName, @schedulerInstanceName, @triggerName, @fireInstanceId, @scheduledFireTimeUtc, @actualFireTimeUtc, @finishedTimeUtc, @recovering, @vetoed, @triggerGroup, @jobName, @jobGroup, @scheduledTime, @firedTime, @runTime, @error)";

        public async Task CreateJobHistoryEntry(
            IJobExecutionContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlInsertJobExecuted, _tablePrefix, null);

            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
                {
                    Delegate.AddCommandParameter(command, "schedulerName", context.Scheduler.SchedulerName);
                    Delegate.AddCommandParameter(command, "schedulerInstanceName", context.Scheduler.SchedulerInstanceId);
                    Delegate.AddCommandParameter(command, "jobName", context.JobDetail.Key.Name);
                    Delegate.AddCommandParameter(command, "jobGroup", context.JobDetail.Key.Group);
                    Delegate.AddCommandParameter(command, "triggerName", context.Trigger.Key.Name);
                    Delegate.AddCommandParameter(command, "triggerGroup", context.Trigger.Key.Group);
                    Delegate.AddCommandParameter(command, "scheduledTime", Delegate.GetDbDateTimeValue(context.ScheduledFireTimeUtc));
                    Delegate.AddCommandParameter(command, "firedTime", Delegate.GetDbDateTimeValue(context.FireTimeUtc));
                    Delegate.AddCommandParameter(command, "runTime", Delegate.GetDbTimeSpanValue(context.JobRunTime));
                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);
                    Delegate.AddCommandParameter(command, "scheduledFireTimeUtc", context.ScheduledFireTimeUtc?.UtcDateTime);
                    Delegate.AddCommandParameter(command, "actualFireTimeUtc", context.FireTimeUtc.UtcDateTime);
                    Delegate.AddCommandParameter(command, "finishedTimeUtc", DateTime.UtcNow);
                    Delegate.AddCommandParameter(command, "recovering", context.Recovering);
                    Delegate.AddCommandParameter(command, "vetoed", false);
                    Delegate.AddCommandParameter(command, "error", false);
                    try
                    {
                        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                        connection.Commit(false);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private const string PropertySqlUpdateHistoryError = "UPDATE {0}JOB_HISTORY SET Vetoed = @vetoed WHERE Fire_Instance_Id = @fireInstanceId";

        public async Task UpdateJobHistoryEntryError(
            IJobExecutionContext context,
            JobExecutionException jobException,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryError, _tablePrefix, null);

            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
                {
                    Delegate.AddCommandParameter(command, "error", Delegate.GetDbBooleanValue(jobException != null));
                    Delegate.AddCommandParameter(command, "errorMessage", jobException?.GetBaseException()?.Message);
                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                    connection.Commit(false);
                }
            }
        }

        private const string PropertySqlUpdateHistoryVetoed = "UPDATE {0}JOB_HISTORY SET Vetoed = @vetoed WHERE Fire_Instance_Id = @fireInstanceId";

        public async Task UpdateJobHistoryEntryVetoed(
            IJobExecutionContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryVetoed, _tablePrefix, null);

            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
                {
                    Delegate.AddCommandParameter(command, "vetoed", true);
                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                    connection.Commit(false);
                }
            }
        }

        private const string PropertySqlSelectHistoryCount = "SELECT COUNT(1) FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName";

        public async Task<int> GetAllCount(string schedulerName, CancellationToken cancellationToken = default)
        {
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sqlCount = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlSelectHistoryCount, _tablePrefix, null);

                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sqlCount))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
                    return (int)await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public Task<ExecutionHistoryEntry> GetJobHistoryEntry(string schedulerName, string fireInstanceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private const string PropertySqlServerSelectHistoryEntryPage =
            "SELECT ENTRY_ID, SCHED_NAME, Scheduler_Instance_Id, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering,Vetoed, " +
            "TRIGGER_NAME, TRIGGER_GROUP, JOB_NAME, JOB_GROUP, FIRED_TIME, SCHED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY {1}) AS RowNumber FROM {0}JOB_HISTORY ) as B " +
            "WHERE SCHED_NAME = @schedulerName AND RowNumber BETWEEN @page AND @endPage";

        private const string PropertyMySqlSelectHistoryEntryPage =
            "SELECT ENTRY_ID, SCHED_NAME, Scheduler_Instance_Id, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed, " +
            "TRIGGER_NAME, TRIGGER_GROUP, JOB_NAME, JOB_GROUP, FIRED_TIME, SCHED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE FROM {0}JOB_HISTORY " +
            "WHERE SCHED_NAME = @schedulerName ORDER BY {1} LIMIT @page, @pageSize";

        public async Task<PagedResultDto<ExecutionHistoryEntry>> GetPageJobHistoryEntries(
            string schedulerName,
            int pageIndex, int pageSize,
            string orderByStr,
            CancellationToken cancellationToken = default)
        {
            var pageResult = new PagedResultDto<ExecutionHistoryEntry>();

            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                //string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlServerSelectHistoryEntry, _tablePrefix, null);

                string sql = string.Format(PropertySqlServerSelectHistoryEntryPage, _tablePrefix, orderByStr);

                if (_provider.Equals("MySql"))
                {
                    //sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertyMySqlSelectHistoryEntry, _tablePrefix, null);
                    sql = string.Format(PropertyMySqlSelectHistoryEntryPage, _tablePrefix, orderByStr);
                }

                pageResult.TotalCount = await GetAllCount(schedulerName, cancellationToken);

                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
                {
                    int page = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
                    Delegate.AddCommandParameter(dbCommand, "page", page);

                    if (_provider.Equals("MySql"))
                    {
                        // pageSize
                        Delegate.AddCommandParameter(dbCommand, "pageSize", pageSize);
                    }
                    else
                    {
                        // endPage
                        Delegate.AddCommandParameter(dbCommand, "endPage", page + pageSize);
                    }

                    using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                    {
                        List<ExecutionHistoryEntry> list = new List<ExecutionHistoryEntry>();
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            var entry = new ExecutionHistoryEntry
                            {
                                Recovering = Delegate.GetBooleanFromDbValue(reader["Recovering"]),
                                Vetoed = Delegate.GetBooleanFromDbValue(reader["Vetoed"]),
                                ActualFireTimeUtc = Convert.ToDateTime(reader["Actual_Fire_Time_Utc"]),
                                FinishedTimeUtc = Convert.ToDateTime(reader["Finished_Time_Utc"]),
                                FireInstanceId = reader.GetString("Fire_Instance_Id"),
                                ScheduledFireTimeUtc = Convert.ToDateTime(reader["Scheduled_Fire_Time_Utc"]),
                                SchedulerInstanceId = reader.GetString("Scheduler_Instance_Id"),
                                SchedulerName = reader.GetString("SCHED_NAME"),
                                JobName = reader.GetString("JOB_NAME"),
                                JobGroup = reader.GetString("JOB_GROUP"),
                                TriggerName = reader.GetString("TRIGGER_NAME"),
                                TriggerGroup = reader.GetString("TRIGGER_GROUP"),
                                FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
                                ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
                                RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
                                Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
                                ErrorMessage = reader.GetString("ERROR_MESSAGE")
                            };
                            list.Add(entry);
                        }
                        pageResult.Items = list.AsReadOnly();
                    }
                }
            }
            return pageResult;
        }

        public Task Purge(string schedulerName, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        private const string PropertySqlServerSelectHistoryGroupByJob = "SELECT TOP @limit * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName GROUP BY JOB_NAME ORDER BY Actual_Fire_Time_Utc desc";
        private const string PropertyMySqlSelectHistoryGroupByJob = "SELECT * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName GROUP BY JOB_NAME ORDER BY Actual_Fire_Time_Utc desc limit @limit";

        public async Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryJob(string schedulerName, int limitPerJob, CancellationToken cancellationToken = default)
        {
            List<ExecutionHistoryEntry> list = new List<ExecutionHistoryEntry>();
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sql = string.Format(PropertySqlServerSelectHistoryGroupByJob, _tablePrefix);

                if (_provider.Equals("MySql"))
                {
                    sql = string.Format(PropertyMySqlSelectHistoryGroupByJob, _tablePrefix);
                }

                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);

                    Delegate.AddCommandParameter(dbCommand, "limit", limitPerJob);

                    using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            var entry = new ExecutionHistoryEntry
                            {
                                Recovering = Delegate.GetBooleanFromDbValue(reader["Recovering"]),
                                Vetoed = Delegate.GetBooleanFromDbValue(reader["Vetoed"]),
                                ActualFireTimeUtc = Convert.ToDateTime(reader["Actual_Fire_Time_Utc"]),
                                FinishedTimeUtc = Convert.ToDateTime(reader["Finished_Time_Utc"]),
                                FireInstanceId = reader.GetString("Fire_Instance_Id"),
                                ScheduledFireTimeUtc = Convert.ToDateTime(reader["Scheduled_Fire_Time_Utc"]),
                                SchedulerInstanceId = reader.GetString("Scheduler_Instance_Id"),
                                SchedulerName = reader.GetString("SCHED_NAME"),
                                JobName = reader.GetString("JOB_NAME"),
                                JobGroup = reader.GetString("JOB_GROUP"),
                                TriggerName = reader.GetString("TRIGGER_NAME"),
                                TriggerGroup = reader.GetString("TRIGGER_GROUP"),
                                FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
                                ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
                                RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
                                Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
                                ErrorMessage = reader.GetString("ERROR_MESSAGE")
                            };
                            list.Add(entry);
                        }
                    }
                }
            }
            list.Reverse();
            return list;
        }

        private const string PropertySqlServerSelectHistoryGroupByTrigger = "SELECT TOP @limit * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName GROUP BY TRIGGER_NAME ORDER BY Actual_Fire_Time_Utc desc";
        private const string PropertyMySqlSelectHistoryGroupByTrigger = "SELECT * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName GROUP BY TRIGGER_NAME ORDER BY Actual_Fire_Time_Utc desc limit @limit";

        public async Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryTrigger(string schedulerName, int limitPerTrigger, CancellationToken cancellationToken = default)
        {
            List<ExecutionHistoryEntry> list = new List<ExecutionHistoryEntry>();
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sql = string.Format(PropertySqlServerSelectHistoryGroupByTrigger, _tablePrefix);

                if (_provider.Equals("MySql"))
                {
                    sql = string.Format(PropertyMySqlSelectHistoryGroupByTrigger, _tablePrefix);
                }

                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);

                    Delegate.AddCommandParameter(dbCommand, "limit", limitPerTrigger);

                    using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            var entry = new ExecutionHistoryEntry
                            {
                                Recovering = Delegate.GetBooleanFromDbValue(reader["Recovering"]),
                                Vetoed = Delegate.GetBooleanFromDbValue(reader["Vetoed"]),
                                ActualFireTimeUtc = Convert.ToDateTime(reader["Actual_Fire_Time_Utc"]),
                                FinishedTimeUtc = Convert.ToDateTime(reader["Finished_Time_Utc"]),
                                FireInstanceId = reader.GetString("Fire_Instance_Id"),
                                ScheduledFireTimeUtc = Convert.ToDateTime(reader["Scheduled_Fire_Time_Utc"]),
                                SchedulerInstanceId = reader.GetString("Scheduler_Instance_Id"),
                                SchedulerName = reader.GetString("SCHED_NAME"),
                                JobName = reader.GetString("JOB_NAME"),
                                JobGroup = reader.GetString("JOB_GROUP"),
                                TriggerName = reader.GetString("TRIGGER_NAME"),
                                TriggerGroup = reader.GetString("TRIGGER_GROUP"),
                                FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
                                ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
                                RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
                                Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
                                ErrorMessage = reader.GetString("ERROR_MESSAGE")
                            };
                            list.Add(entry);
                        }
                    }
                }
            }
            list.Reverse();
            return list;
        }

        private const string PropertySqlServerSelectHistory = "SELECT TOP @limit * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName ORDER BY Actual_Fire_Time_Utc desc";
        private const string PropertyMySqlSelectHistory = "SELECT * FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName ORDER BY Actual_Fire_Time_Utc desc limit @limit";

        public async Task<IEnumerable<ExecutionHistoryEntry>> FilterLast(string schedulerName, int limit, CancellationToken cancellationToken = default)
        {
            //return await Task.FromResult(new List<ExecutionHistoryEntry>());
            List<ExecutionHistoryEntry> list = new List<ExecutionHistoryEntry>();
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sql = string.Format(PropertySqlServerSelectHistory, _tablePrefix);

                if (_provider.Equals("MySql"))
                {
                    sql = string.Format(PropertyMySqlSelectHistory, _tablePrefix);
                }

                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);

                    Delegate.AddCommandParameter(dbCommand, "limit", limit);

                    using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            var entry = new ExecutionHistoryEntry
                            {
                                Recovering = Delegate.GetBooleanFromDbValue(reader["Recovering"]),
                                Vetoed = Delegate.GetBooleanFromDbValue(reader["Vetoed"]),
                                ActualFireTimeUtc = Convert.ToDateTime(reader["Actual_Fire_Time_Utc"]),
                                FinishedTimeUtc = Convert.ToDateTime(reader["Finished_Time_Utc"]),
                                FireInstanceId = reader.GetString("Fire_Instance_Id"),
                                ScheduledFireTimeUtc = Convert.ToDateTime(reader["Scheduled_Fire_Time_Utc"]),
                                SchedulerInstanceId = reader.GetString("Scheduler_Instance_Id"),
                                SchedulerName = reader.GetString("SCHED_NAME"),
                                JobName = reader.GetString("JOB_NAME"),
                                JobGroup = reader.GetString("JOB_GROUP"),
                                TriggerName = reader.GetString("TRIGGER_NAME"),
                                TriggerGroup = reader.GetString("TRIGGER_GROUP"),
                                FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
                                ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
                                RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
                                Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
                                ErrorMessage = reader.GetString("ERROR_MESSAGE")
                            };
                            list.Add(entry);
                        }
                    }
                }
            }
            list.Reverse();
            return list;
        }

        private const string PropertySqlSelectHistorySuccessCount = "SELECT COUNT(1) FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName AND Error = false";

        public async Task<long> GetTotalJobsExecuted(string schedulerName, CancellationToken cancellationToken = default)
        {
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sqlCount = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlSelectHistorySuccessCount, _tablePrefix, null);
                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sqlCount))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
                    return (long)await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private const string PropertySqlSelectHistoryFailedCount = "SELECT COUNT(1) FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName AND Error = true";

        public async Task<long> GetTotalJobsFailed(string schedulerName, CancellationToken cancellationToken = default)
        {
            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
            {
                string sqlCount = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlSelectHistoryFailedCount, _tablePrefix, null);
                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sqlCount))
                {
                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
                    return (long)await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        #region 受保护的方法

        /// <summary>
        /// Get the driver delegate for DB operations.
        /// </summary>
        protected virtual IDbAccessor Delegate
        {
            get
            {
                lock (this)
                {
                    if (_driverDelegate != null) return _driverDelegate;
                    try
                    {
                        if (_delegateTypeName != null)
                        {
                            _delegateType = TypeLoadHelper.LoadType(_delegateTypeName);
                        }

                        IDbProvider dbProvider = DBConnectionManager.Instance.GetDbProvider(_dataSource);
                        var args = new DelegateInitializationArgs { DbProvider = dbProvider };

                        ConstructorInfo ctor = _delegateType.GetConstructor(new Type[0]);
                        if (ctor == null)
                        {
                            throw new InvalidConfigurationException("Configured delegate does not have public constructor that takes no arguments");
                        }

                        _driverDelegate = (StdAdoDelegate)ctor.Invoke(null);
                        _driverDelegate.Initialize(args);
                    }
                    catch (Exception e)
                    {
                        throw new NoSuchDelegateException("Couldn't instantiate delegate: " + e.Message, e);
                    }
                }
                return _driverDelegate;
            }
        }

        /// <summary>
        /// Gets the connection and starts a new transaction.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        protected ConnectionAndTransactionHolder GetConnection(IsolationLevel isolationLevel)
        {
            DbConnection conn;
            DbTransaction tx;
            try
            {
                conn = DBConnectionManager.Instance.GetConnection(_dataSource);
                conn.Open();
            }
            catch (Exception e)
            {
                throw new JobPersistenceException(
                    $"Failed to obtain DB connection from data source '{_dataSource}': {e}", e);
            }
            if (conn == null)
            {
                throw new JobPersistenceException($"Could not get connection from DataSource '{_dataSource}'");
            }

            try
            {
                tx = conn.BeginTransaction(isolationLevel);
            }
            catch (Exception e)
            {
                conn.Close();
                throw new JobPersistenceException("Failure setting up connection.", e);
            }

            return new ConnectionAndTransactionHolder(conn, tx);
        }

        #endregion 受保护的方法
    }
}