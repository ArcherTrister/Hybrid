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
    /// <summary>
    ///
    /// </summary>
    internal static class MySqlQuartzOptionsExtensions
    {
        public static void UseMySql(this IServiceCollection services, QuartzOptions mySqlQuartzOptions)
        {
            IScheduler scheduler = new StdSchedulerFactory(SetProperties(mySqlQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
        }

        private static NameValueCollection SetProperties(QuartzOptions mySqlQuartzOptions)
        {
            var properties = new NameValueCollection();

            properties.Set(StdSchedulerFactory.PropertySchedulerName, mySqlQuartzOptions.SchedulerName);
            properties.Set(StdSchedulerFactory.PropertyJobStoreType, "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");

            properties.Set("quartz.jobStore.useProperties", "true");
            properties.Set("quartz.jobStore.dataSource", "default");
            properties.Set("quartz.dataSource.default.connectionString", mySqlQuartzOptions.ConnectionStringOrCacheName);
            properties.Set("quartz.dataSource.default.provider", "MySql");
            properties.Set("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz");
            properties.Set("quartz.jobStore.tablePrefix", mySqlQuartzOptions.TablePrefix);
            properties.Set("quartz.serializer.type", mySqlQuartzOptions.SerializerType.ToString());

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