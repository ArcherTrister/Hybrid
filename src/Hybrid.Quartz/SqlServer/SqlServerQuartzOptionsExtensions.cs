using Hybrid.Core.Options;

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
        }

        private static NameValueCollection SetProperties(QuartzOptions sqlServerQuartzOptions)
        {
            var properties = new NameValueCollection();

            properties.Set(StdSchedulerFactory.PropertySchedulerName, sqlServerQuartzOptions.SchedulerName);

            // 数据连接字符串
            properties.Set("quartz.dataSource.myDS.connectionString", sqlServerQuartzOptions.ConnectionStringOrCacheName);
            // 数据库类型
            properties.Set("quartz.dataSource.myDS.provider", "SqlServer");
            // 设置存储类型
            properties.Set(StdSchedulerFactory.PropertyJobStoreType, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
            // 驱动类型
            properties.Set("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
            // 数据源名称
            properties.Set("quartz.jobStore.dataSource", "myDS");
            // JobDataMaps 中的值只能是字符串，具体可以看官方推荐这样设置的原因
            properties.Set("quartz.jobStore.useProperties", "true");
            properties.Set("quartz.jobStore.selectWithLockSQL", $"SELECT * FROM {sqlServerQuartzOptions.TablePrefix}LOCKS WITH(UPDLOCK) WHERE SCHED_NAME = @schedulerName AND LOCK_NAME = @lockName");

            properties.Set("quartz.jobStore.tablePrefix", sqlServerQuartzOptions.TablePrefix);
            // 数据存储序列化方式
            properties.Set("quartz.serializer.type", sqlServerQuartzOptions.SerializerType.ToString());

            if (sqlServerQuartzOptions.IsClustered)
            {
                //是否集群，集群模式下要设置为true
                properties["quartz.jobStore.clustered"] = "true";
                properties["quartz.scheduler.instanceName"] = "TestScheduler";
                //集群模式下设置为auto，自动获取实例的Id，集群下一定要id不一样，不然不会自动恢复
                properties["quartz.scheduler.instanceId"] = "AUTO";
            }

            if (sqlServerQuartzOptions.IsUseHistoryPlugin)
            {
                // 加载插件
                // properties.Set("quartz.plugin.自定义名称.type","命名空间.类名,程序集名称");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.type", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryPlugin,Hybrid.Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.dataSource", "myDS");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.storeType", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryStore, Hybrid.Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.tablePrefix", sqlServerQuartzOptions.TablePrefix);
                properties.Set("quartz.plugin.DatabaseExecutionHistory.provider", "SqlServer");
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