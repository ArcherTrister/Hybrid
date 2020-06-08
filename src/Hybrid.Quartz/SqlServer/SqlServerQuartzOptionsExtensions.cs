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
    internal static class SqlServerQuartzOptionsExtensions
    {
        public static void UseSqlServer(this IServiceCollection services, QuartzConfiguration sqlServerQuartzOptions)
        {
            IScheduler scheduler = new StdSchedulerFactory(SetProperties(sqlServerQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
        }

        private static NameValueCollection SetProperties(QuartzConfiguration sqlServerQuartzOptions)
        {
            var properties = new NameValueCollection();
            // 调度名称
            // properties.Set(StdSchedulerFactory.PropertySchedulerName, sqlServerQuartzOptions.SchedulerName);
            // 实例名称
            properties.Set("quartz.scheduler.instanceName", sqlServerQuartzOptions.SchedulerName);
            // 实例Id
            // 线程池
            properties.Set("quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz");
            // 设置线程池的最大线程数量
            properties.Set("quartz.threadPool.threadCount", sqlServerQuartzOptions.ThreadCount.ToString());
            // 设置作业中每个线程的优先级，可取 System.Threading.ThreadPriority 中的枚举
            properties.Set("quartz.threadPool.threadPriority", sqlServerQuartzOptions.ThreadPriority.ToString());

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
            // 您可以并且应该使用最新版本的驱动程序（如果有更新的话），只需创建程序集绑定重定向即可
            // 如果您的调度程序非常忙（即几乎总是执行与线程池大小相同的作业数），那么您可能应该将数据源中的连接数设置为线程池大小的大约 + 1。通常在ADO.NET连接字符串中配置它 - 有关详细信息，请参见驱动程序实现。
            // 可以将“quartz.jobStore.useProperties”配置参数设置为“ true”（默认为false），以指示AdoJobStore JobDataMaps中的所有值都是字符串，因此可以作为名称 - 值对存储，而不是存储BLOB列中序列化形式的更复杂对象。从长远来看，这样做更加安全，因为可以避免将非String类序列化为BLOB时出现的类版本控制问题。
            properties.Set("quartz.jobStore.useProperties", "true");
            // 表前缀
            properties.Set("quartz.jobStore.tablePrefix", sqlServerQuartzOptions.TablePrefix);
            // 数据存储序列化方式
            properties.Set("quartz.serializer.type", sqlServerQuartzOptions.SerializerType.ToString());

            if (sqlServerQuartzOptions.IsClustered)
            {
                //是否集群，集群模式下要设置为true
                properties["quartz.jobStore.clustered"] = "true";
                //集群模式下设置为auto，自动获取实例的Id，集群下一定要id不一样，不然不会自动恢复
                properties["quartz.scheduler.instanceId"] = "AUTO";
                //
                if (sqlServerQuartzOptions.IsUseSelectWithLockSQL)
                {
                    string selectWithLockSQL = $"select * from {sqlServerQuartzOptions.TablePrefix}LOCKS UPDLOCK WHERE LOCK_NAME = @lockName";
                    //$"SELECT * FROM {sqlServerQuartzOptions.TablePrefix}LOCKS WITH(UPDLOCK) WHERE {AdoConstants.ColumnSchedulerName} = @schedulerName AND LOCK_NAME = @lockName"
                    properties.Set("quartz.jobStore.selectWithLockSQL", selectWithLockSQL);
                }
            }

            // 加载插件
            if (sqlServerQuartzOptions.IsUseHistoryPlugin)
            {
                // 加载获取历史记录插件
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
            //initialization plugin handles our xml reading, without it defaults are used
            //quartz.plugin.xml.type = Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz
            //quartz.plugin.xml.fileNames = ~/ quartz_jobs.xml
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