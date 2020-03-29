using Hybrid.Core.Configuration;

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
    /// <summary>
    ///
    /// </summary>
    internal static class MySqlQuartzOptionsExtensions
    {
        public static void UseMySql(this IServiceCollection services, QuartzConfiguration mySqlQuartzOptions)
        {
            IScheduler scheduler = new StdSchedulerFactory(SetProperties(mySqlQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
        }

        private static NameValueCollection SetProperties(QuartzConfiguration mySqlQuartzOptions)
        {
            var properties = new NameValueCollection();

            // properties.Set(StdSchedulerFactory.PropertySchedulerName, mySqlQuartzOptions.SchedulerName);
            // 实例名称
            properties.Set("quartz.scheduler.instanceName", mySqlQuartzOptions.SchedulerName);
            // 实例Id
            // 线程池
            properties.Set("quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz");
            // 设置线程池的最大线程数量
            properties.Set("quartz.threadPool.threadCount", mySqlQuartzOptions.ThreadCount.ToString());
            // 设置作业中每个线程的优先级，可取 System.Threading.ThreadPriority 中的枚举
            properties.Set("quartz.threadPool.threadPriority", mySqlQuartzOptions.ThreadPriority.ToString());

            properties.Set(StdSchedulerFactory.PropertyJobStoreType, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");

            properties.Set("quartz.jobStore.useProperties", "true");
            properties.Set("quartz.jobStore.dataSource", "default");
            properties.Set("quartz.dataSource.default.connectionString", mySqlQuartzOptions.ConnectionStringOrCacheName);
            properties.Set("quartz.dataSource.default.provider", "MySql");
            properties.Set("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz");
            properties.Set("quartz.jobStore.tablePrefix", mySqlQuartzOptions.TablePrefix);
            properties.Set("quartz.serializer.type", mySqlQuartzOptions.SerializerType.ToString());

            if (mySqlQuartzOptions.IsClustered)
            {
                //是否集群，集群模式下要设置为true
                properties["quartz.jobStore.clustered"] = "true";
                //集群模式下设置为auto，自动获取实例的Id，集群下一定要id不一样，不然不会自动恢复
                properties["quartz.scheduler.instanceId"] = "AUTO";
            }

            if (mySqlQuartzOptions.IsUseHistoryPlugin)
            {
                // 加载插件
                // properties.Set("quartz.plugin.自定义名称.type","命名空间.类名,程序集名称");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.type", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryPlugin,Hybrid.Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.dataSource", "default");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.driverDelegateType", "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.storeType", "Hybrid.Quartz.Plugins.History.DatabaseExecutionHistoryStore, Hybrid.Quartz");
                properties.Set("quartz.plugin.DatabaseExecutionHistory.tablePrefix", mySqlQuartzOptions.TablePrefix);
                properties.Set("quartz.plugin.DatabaseExecutionHistory.provider", "MySql");
            }

            return properties;
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