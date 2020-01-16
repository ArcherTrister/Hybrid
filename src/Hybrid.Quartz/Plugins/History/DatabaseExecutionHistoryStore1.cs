//using Hybrid.AspNetCore.Mvc.Models;

//using Quartz;
//using Quartz.Impl.AdoJobStore;
//using Quartz.Impl.AdoJobStore.Common;
//using Quartz.Spi;
//using Quartz.Util;

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Reflection;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Hybrid.Quartz.Plugins.History
//{
//    public class DatabaseExecutionHistoryStore1 : IExecutionHistoryStore
//    {
//        private StdAdoDelegate _driverDelegate;

//        private readonly string _delegateTypeName;
//        private Type _delegateType = typeof(StdAdoDelegate);
//        private readonly string _tablePrefix;
//        private readonly string _dataSource;

//        //private readonly JobHistoryDelegate jobHistoryDelegate = DatabaseExecutionHistoryPlugin.Delegate;
//        private const string PropertySqlInsertJobExecuted =
//    "INSERT INTO {0}JOB_HISTORY (SCHED_NAME, Scheduler_Instance_Id, TRIGGER_NAME, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed," +
//    "TRIGGER_GROUP, JOB_NAME, JOB_GROUP, SCHED_TIME, FIRED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE)  VALUES (@schedulerName, @schedulerInstanceName, @triggerName, @fireInstanceId, @scheduledFireTimeUtc, @actualFireTimeUtc, @finishedTimeUtc, @recovering, @vetoed, @triggerGroup, @jobName, @jobGroup, @scheduledTime, @firedTime, @runTime, @error, @errorMessage)";

//        //private const string PropertySqlServerSelectHistoryEntry =
//        //    "SELECT TOP 25 SCHED_NAME, Scheduler_Instance_Id, TRIGGER_NAME, TRIGGER_GROUP, JOB_NAME, JOB_GROUP, FIRED_TIME, SCHED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName";

//        private const string PropertySqlServerSelectHistoryEntry =
//                "SELECT ENTRY_ID, SCHED_NAME, Scheduler_Instance_Id, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed, TRIGGER_NAME, TRIGGER_GROUP, JOB_NAME, JOB_GROUP, FIRED_TIME, SCHED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE FROM" +
//                " (SELECT ROW_NUMBER() OVER(ORDER BY {1} {2}) AS ROWS FROM {0}JOB_HISTORY) AS T" +
//                " WHERE SCHED_NAME = @schedulerName AND ROWS BETWEEN @page AND @endPage";

//        private const string PropertyMySqlSelectHistoryEntry =
//            "SELECT ENTRY_ID, SCHED_NAME, Scheduler_Instance_Id, Fire_Instance_Id, Scheduled_Fire_Time_Utc, Actual_Fire_Time_Utc, Finished_Time_Utc, Recovering, Vetoed, TRIGGER_NAME, TRIGGER_GROUP, JOB_NAME, JOB_GROUP, FIRED_TIME, SCHED_TIME, RUN_TIME, ERROR, ERROR_MESSAGE FROM {0}JOB_HISTORY WHERE SCHED_NAME = @schedulerName ORDER BY {1} {2} LIMIT @page, @pageSize";

//        private const string PropertySqlSelectHistoryCount = "SELECT COUNT(1) FROM {0}JOB_HISTORY";

//        private const string PropertySqlUpdateHistoryVetoed = "UPDATE {0}JOB_HISTORY SET Vetoed = @vetoed WHERE Fire_Instance_Id = @fireInstanceId";

//        private const string PropertySqlUpdateHistoryError = "UPDATE {0}JOB_HISTORY SET Vetoed = @vetoed WHERE Fire_Instance_Id = @fireInstanceId";



//        public string SchedulerName { get; set; }

//        DateTime _nextPurgeTime = DateTime.UtcNow;
//        //int _updatesFromLastPurge;

//        long _totalJobsExecuted = 0, _totalJobsFailed = 0;

//        //    public async Task InsertJobHistoryEntry(
//        //IJobExecutionContext context,
//        //CancellationToken cancellationToken = default(CancellationToken))
//        //    {
//        //        string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlInsertJobExecuted, _tablePrefix, null);

//        //        using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//        //        {
//        //            using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//        //            {
//        //                Delegate.AddCommandParameter(command, "schedulerName", context.Scheduler.SchedulerName);
//        //                Delegate.AddCommandParameter(command, "schedulerInstanceName", context.Scheduler.SchedulerInstanceId);
//        //                Delegate.AddCommandParameter(command, "jobName", context.JobDetail.Key.Name);
//        //                Delegate.AddCommandParameter(command, "jobGroup", context.JobDetail.Key.Group);
//        //                Delegate.AddCommandParameter(command, "triggerName", context.Trigger.Key.Name);
//        //                Delegate.AddCommandParameter(command, "triggerGroup", context.Trigger.Key.Group);
//        //                Delegate.AddCommandParameter(command, "scheduledTime", Delegate.GetDbDateTimeValue(context.ScheduledFireTimeUtc));
//        //                Delegate.AddCommandParameter(command, "firedTime", Delegate.GetDbDateTimeValue(context.FireTimeUtc));
//        //                Delegate.AddCommandParameter(command, "runTime", Delegate.GetDbTimeSpanValue(context.JobRunTime));
//        //                Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);
//        //                Delegate.AddCommandParameter(command, "scheduledFireTimeUtc", context.ScheduledFireTimeUtc?.UtcDateTime);
//        //                Delegate.AddCommandParameter(command, "actualFireTimeUtc", context.FireTimeUtc.UtcDateTime);
//        //                Delegate.AddCommandParameter(command, "finishedTimeUtc", DateTime.UtcNow);
//        //                Delegate.AddCommandParameter(command, "recovering", context.Recovering);
//        //                Delegate.AddCommandParameter(command, "vetoed", false);

//        //                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//        //                connection.Commit(false);
//        //            }
//        //        }
//        //    }

//        //    public async Task UpdateJobHistoryEntryError(
//        //        IJobExecutionContext context,
//        //        JobExecutionException jobException,
//        //        CancellationToken cancellationToken = default(CancellationToken))
//        //    {
//        //        string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryError, _tablePrefix, null);

//        //        using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//        //        {
//        //            using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//        //            {
//        //                Delegate.AddCommandParameter(command, "error", Delegate.GetDbBooleanValue(jobException != null));
//        //                Delegate.AddCommandParameter(command, "errorMessage", jobException?.GetBaseException()?.Message);
//        //                Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

//        //                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//        //                connection.Commit(false);
//        //            }
//        //        }
//        //    }

//        //    public async Task UpdateJobHistoryEntryVetoed(
//        //        IJobExecutionContext context,
//        //        CancellationToken cancellationToken = default(CancellationToken))
//        //    {
//        //        string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryVetoed, _tablePrefix, null);

//        //        using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//        //        {
//        //            using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//        //            {
//        //                Delegate.AddCommandParameter(command, "vetoed", true);
//        //                Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

//        //                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//        //                connection.Commit(false);
//        //            }
//        //        }
//        //    }

//        //    public async Task<PageResult<ExecutionHistoryEntry>> SelectExecutionHistoryEntries(
//        //        string schedulerName,
//        //        int pageIndex, int pageSize,
//        //        string sortName, string sortOrder,
//        //        CancellationToken cancellationToken = default)
//        //    {
//        //        //string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlServerSelectHistoryEntry, _tablePrefix, null);

//        //        string sql = string.Format(PropertySqlServerSelectHistoryEntry, _tablePrefix, sortName, sortOrder);

//        //        if (Delegate is MySQLDelegate)
//        //        {
//        //            //sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertyMySqlSelectHistoryEntry, _tablePrefix, null);
//        //            sql = string.Format(PropertyMySqlSelectHistoryEntry, _tablePrefix, sortName, sortOrder);
//        //        }

//        //        string sqlCount = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlSelectHistoryCount, _tablePrefix, null);

//        //        var pageResult = new PageResult<ExecutionHistoryEntry>();

//        //        using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
//        //        {
//        //            using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sqlCount))
//        //            {
//        //                pageResult.TotalRecords = (long)await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
//        //            }

//        //            using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
//        //            {
//        //                int page = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

//        //                Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
//        //                Delegate.AddCommandParameter(dbCommand, "page", page);

//        //                if (Delegate is SqlServerDelegate)
//        //                {
//        //                    // endPage
//        //                    Delegate.AddCommandParameter(dbCommand, "endPage", page + pageSize);
//        //                }
//        //                if (Delegate is MySQLDelegate)
//        //                {
//        //                    // pageSize
//        //                    Delegate.AddCommandParameter(dbCommand, "pageSize", pageSize);
//        //                }

//        //                using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
//        //                {
//        //                    while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
//        //                    {
//        //                        var entry = new ExecutionHistoryEntry
//        //                        {
//        //                            JobName = reader.GetString("JOB_NAME"),
//        //                            JobGroup = reader.GetString("JOB_GROUP"),
//        //                            TriggerName = reader.GetString("TRIGGER_NAME"),
//        //                            TriggerGroup = reader.GetString("TRIGGER_GROUP"),
//        //                            FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
//        //                            ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
//        //                            RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
//        //                            Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
//        //                            ErrorMessage = reader.GetString("ERROR_MESSAGE")
//        //                        };
//        //                        pageResult.Data.Add(entry);
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        return pageResult;
//        //    }

//        public Task<ExecutionHistoryEntry> GetJobHistoryEntry(string fireInstanceId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task Purge()
//        {
//            //var ids = new HashSet<string>((await FilterLastOfEveryTrigger(10)).Select(x => x.FireInstanceId));

//            //lock (_data)
//            //{
//            //    foreach (var key in _data.Keys.ToArray())
//            //    {
//            //        if (!ids.Contains(key))
//            //            _data.Remove(key);
//            //    }
//            //}
//            await Task.CompletedTask;
//        }

//        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryJob(int limitPerJob)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLastOfEveryTrigger(int limitPerTrigger)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<ExecutionHistoryEntry>> FilterLast(int limit)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<long> GetTotalJobsExecuted()
//        {
//            return Task.FromResult(_totalJobsExecuted);
//        }
//        public Task<long> GetTotalJobsFailed()
//        {
//            return Task.FromResult(_totalJobsFailed);
//        }

//        public Task IncrementTotalJobsExecuted()
//        {
//            Interlocked.Increment(ref _totalJobsExecuted);
//            return Task.FromResult(0);
//        }

//        public Task IncrementTotalJobsFailed()
//        {
//            Interlocked.Increment(ref _totalJobsFailed);
//            return Task.FromResult(0);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task CreateJobHistoryEntry(
//            IJobExecutionContext context,
//            CancellationToken cancellationToken = default(CancellationToken))
//        {
//            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlInsertJobExecuted, _tablePrefix, null);

//            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//            {
//                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//                {
//                    Delegate.AddCommandParameter(command, "schedulerName", context.Scheduler.SchedulerName);
//                    Delegate.AddCommandParameter(command, "schedulerInstanceName", context.Scheduler.SchedulerInstanceId);
//                    Delegate.AddCommandParameter(command, "jobName", context.JobDetail.Key.Name);
//                    Delegate.AddCommandParameter(command, "jobGroup", context.JobDetail.Key.Group);
//                    Delegate.AddCommandParameter(command, "triggerName", context.Trigger.Key.Name);
//                    Delegate.AddCommandParameter(command, "triggerGroup", context.Trigger.Key.Group);
//                    Delegate.AddCommandParameter(command, "scheduledTime", Delegate.GetDbDateTimeValue(context.ScheduledFireTimeUtc));
//                    Delegate.AddCommandParameter(command, "firedTime", Delegate.GetDbDateTimeValue(context.FireTimeUtc));
//                    Delegate.AddCommandParameter(command, "runTime", Delegate.GetDbTimeSpanValue(context.JobRunTime));
//                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);
//                    Delegate.AddCommandParameter(command, "scheduledFireTimeUtc", context.ScheduledFireTimeUtc?.UtcDateTime);
//                    Delegate.AddCommandParameter(command, "actualFireTimeUtc", context.FireTimeUtc.UtcDateTime);
//                    Delegate.AddCommandParameter(command, "finishedTimeUtc", DateTime.UtcNow);
//                    Delegate.AddCommandParameter(command, "recovering", context.Recovering);
//                    Delegate.AddCommandParameter(command, "vetoed", false);

//                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//                    connection.Commit(false);
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="jobException"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task UpdateJobHistoryEntryError(
//            IJobExecutionContext context,
//            JobExecutionException jobException,
//            CancellationToken cancellationToken = default(CancellationToken))
//        {
//            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryError, _tablePrefix, null);

//            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//            {
//                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//                {
//                    Delegate.AddCommandParameter(command, "error", Delegate.GetDbBooleanValue(jobException != null));
//                    Delegate.AddCommandParameter(command, "errorMessage", jobException?.GetBaseException()?.Message);
//                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

//                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//                    connection.Commit(false);
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task UpdateJobHistoryEntryVetoed(
//            IJobExecutionContext context,
//            CancellationToken cancellationToken = default(CancellationToken))
//        {
//            string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlUpdateHistoryVetoed, _tablePrefix, null);

//            using (ConnectionAndTransactionHolder connection = GetConnection(IsolationLevel.ReadUncommitted))
//            {
//                using (DbCommand command = Delegate.PrepareCommand(connection, sql))
//                {
//                    Delegate.AddCommandParameter(command, "vetoed", true);
//                    Delegate.AddCommandParameter(command, "fireInstanceId", context.FireInstanceId);

//                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
//                    connection.Commit(false);
//                }
//            }
//        }

//        public async Task<PageResult<ExecutionHistoryEntry>> SelectExecutionHistoryEntries(
//            string schedulerName,
//            int pageIndex, int pageSize,
//            string sortName, string sortOrder,
//            CancellationToken cancellationToken = default)
//        {
//            //string sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlServerSelectHistoryEntry, _tablePrefix, null);

//            string sql = string.Format(PropertySqlServerSelectHistoryEntry, _tablePrefix, sortName, sortOrder);

//            if (Delegate is MySQLDelegate)
//            {
//                //sql = AdoJobStoreUtil.ReplaceTablePrefix(PropertyMySqlSelectHistoryEntry, _tablePrefix, null);
//                sql = string.Format(PropertyMySqlSelectHistoryEntry, _tablePrefix, sortName, sortOrder);
//            }

//            string sqlCount = AdoJobStoreUtil.ReplaceTablePrefix(PropertySqlSelectHistoryCount, _tablePrefix, null);

//            var pageResult = new PageResult<ExecutionHistoryEntry>();

//            using (ConnectionAndTransactionHolder dbConnection = GetConnection(IsolationLevel.ReadUncommitted))
//            {
//                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sqlCount))
//                {
//                    pageResult.TotalRecords = (long)await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
//                }

//                using (DbCommand dbCommand = Delegate.PrepareCommand(dbConnection, sql))
//                {
//                    int page = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

//                    Delegate.AddCommandParameter(dbCommand, "schedulerName", schedulerName);
//                    Delegate.AddCommandParameter(dbCommand, "page", page);

//                    if (Delegate is SqlServerDelegate)
//                    {
//                        // endPage
//                        Delegate.AddCommandParameter(dbCommand, "endPage", page + pageSize);
//                    }
//                    if (Delegate is MySQLDelegate)
//                    {
//                        // pageSize
//                        Delegate.AddCommandParameter(dbCommand, "pageSize", pageSize);
//                    }

//                    using (DbDataReader reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
//                    {
//                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
//                        {
//                            var entry = new ExecutionHistoryEntry
//                            {
//                                JobName = reader.GetString("JOB_NAME"),
//                                JobGroup = reader.GetString("JOB_GROUP"),
//                                TriggerName = reader.GetString("TRIGGER_NAME"),
//                                TriggerGroup = reader.GetString("TRIGGER_GROUP"),
//                                FiredTime = Delegate.GetDateTimeFromDbValue(reader["FIRED_TIME"]).GetValueOrDefault(),
//                                ScheduledTime = Delegate.GetDateTimeFromDbValue(reader["SCHED_TIME"]).GetValueOrDefault(),
//                                RunTime = Delegate.GetTimeSpanFromDbValue(reader["RUN_TIME"]).GetValueOrDefault(),
//                                Error = Delegate.GetBooleanFromDbValue(reader["ERROR"]),
//                                ErrorMessage = reader.GetString("ERROR_MESSAGE")
//                            };
//                            pageResult.Data.Add(entry);
//                        }
//                    }
//                }
//            }
//            return pageResult;
//        }

//        #region 受保护的方法

//        protected ITypeLoadHelper TypeLoadHelper { get; private set; }

//        /// <summary>
//        /// Get the driver delegate for DB operations.
//        /// </summary>
//        protected virtual IDbAccessor Delegate
//        {
//            get
//            {
//                lock (this)
//                {
//                    if (_driverDelegate != null) return _driverDelegate;
//                    try
//                    {
//                        if (_delegateTypeName != null)
//                        {
//                            _delegateType = TypeLoadHelper.LoadType(_delegateTypeName);
//                        }

//                        IDbProvider dbProvider = DBConnectionManager.Instance.GetDbProvider(_dataSource);
//                        var args = new DelegateInitializationArgs { DbProvider = dbProvider };

//                        ConstructorInfo ctor = _delegateType.GetConstructor(new Type[0]);
//                        if (ctor == null)
//                        {
//                            throw new InvalidConfigurationException("Configured delegate does not have public constructor that takes no arguments");
//                        }

//                        _driverDelegate = (StdAdoDelegate)ctor.Invoke(null);
//                        _driverDelegate.Initialize(args);
//                    }
//                    catch (Exception e)
//                    {
//                        throw new NoSuchDelegateException("Couldn't instantiate delegate: " + e.Message, e);
//                    }
//                }
//                return _driverDelegate;
//            }
//        }

//        /// <summary>
//        /// Gets the connection and starts a new transaction.
//        /// </summary>
//        /// <param name="isolationLevel"></param>
//        /// <returns></returns>
//        protected ConnectionAndTransactionHolder GetConnection(IsolationLevel isolationLevel)
//        {
//            DbConnection conn;
//            DbTransaction tx;
//            try
//            {
//                conn = DBConnectionManager.Instance.GetConnection(_dataSource);
//                conn.Open();
//            }
//            catch (Exception e)
//            {
//                throw new JobPersistenceException(
//                    $"Failed to obtain DB connection from data source '{_dataSource}': {e}", e);
//            }
//            if (conn == null)
//            {
//                throw new JobPersistenceException($"Could not get connection from DataSource '{_dataSource}'");
//            }

//            try
//            {
//                tx = conn.BeginTransaction(isolationLevel);
//            }
//            catch (Exception e)
//            {
//                conn.Close();
//                throw new JobPersistenceException("Failure setting up connection.", e);
//            }

//            return new ConnectionAndTransactionHolder(conn, tx);
//        }

//        public Task<PageResult<ExecutionHistoryEntry>> GetPageJobHistoryEntries(string schedulerName, int pageIndex, int pageSize, string sortName, string sortOrder, CancellationToken cancellationToken = default)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion
//    }
//}
