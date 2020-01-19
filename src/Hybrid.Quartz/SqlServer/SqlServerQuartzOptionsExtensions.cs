using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Quartz.SqlServer;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.AdoJobStore;
using Quartz.Util;

using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class SqlServerQuartzOptionsExtensions
    {
        public static void UseSqlServer(this IServiceCollection services, QuartzOptions sqlServerQuartzOptions)
        {
            IScheduler scheduler = new StdSchedulerFactory(SetProperties(sqlServerQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
            // 初始化数据库
            SqlServerObjectsInstaller.Initialize(sqlServerQuartzOptions.ConnectionStringOrCacheName, sqlServerQuartzOptions.TablePrefix);
        }

        private static NameValueCollection SetProperties(QuartzOptions sqlServerQuartzOptions)
        {
            var properties = new NameValueCollection();

            properties.Set(StdSchedulerFactory.PropertySchedulerName, sqlServerQuartzOptions.SchedulerName);
            properties.Set(StdSchedulerFactory.PropertyJobStoreType, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
            properties.Set("quartz.jobStore.useProperties", "true");
            properties.Set("quartz.jobStore.dataSource", "default");
            properties.Set("quartz.dataSource.default.connectionString", sqlServerQuartzOptions.ConnectionStringOrCacheName);
            properties.Set("quartz.dataSource.default.provider", "SqlServer");

            properties.Set("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
            properties.Set("quartz.jobStore.tablePrefix", sqlServerQuartzOptions.TablePrefix);
            properties.Set("quartz.serializer.type", sqlServerQuartzOptions.SerializerType.ToString());

            if (sqlServerQuartzOptions.IsUseHistoryPlugin)
            {
                // 加载插件
                // properties.Set("quartz.plugin.自定义名称.type","命名空间.类名,程序集名称");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.type", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryPlugin,Hybrid.Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.DataSource", "default");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.DriverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.storeType", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryStore, Hybrid.Quartz");
            }

            return properties;
            //["quartz.scheduler.instanceName"] = "TestScheduler",
            //["quartz.scheduler.instanceId"] = "instance_one",
            //["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
            //["quartz.threadPool.threadCount"] = "5",
            //["quartz.jobStore.misfireThreshold"] = "60000",
            //["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
            //["quartz.jobStore.useProperties"] = "false",
            //["quartz.jobStore.dataSource"] = "default",
            //["quartz.jobStore.tablePrefix"] = "QRTZ_",
            //["quartz.jobStore.clustered"] = "true",
            //["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
            //["quartz.dataSource.default.connectionString"] = TestConstants.SqlServerConnectionString,
            //["quartz.dataSource.default.provider"] = TestConstants.DefaultSqlServerProvider,
            //["quartz.serializer.type"] = "json"
        }

        /// <summary>
        /// Gets the connection and starts a new transaction.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        private static ConnectionAndTransactionHolder GetConnection(string dataSource, IsolationLevel isolationLevel)
        {
            DbConnection conn;
            DbTransaction tx;
            try
            {
                conn = DBConnectionManager.Instance.GetConnection(dataSource);
                conn.Open();
            }
            catch (Exception e)
            {
                throw new JobPersistenceException(
                    $"Failed to obtain DB connection from data source '{dataSource}': {e}", e);
            }
            if (conn == null)
            {
                throw new JobPersistenceException($"Could not get connection from DataSource '{dataSource}'");
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
    }
}