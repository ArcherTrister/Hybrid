using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Extensions;
using Quartz;
using Quartz.Impl;

using System.Collections.Specialized;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 使用内存存储
    /// </summary>
    internal static class InMemoryStorageOptionsExtensions
    {
        public static void UseInMemoryStorage(this IServiceCollection services, QuartzOptions inMemoryQuartzOptions)
        {
            if (inMemoryQuartzOptions.SchedulerName.IsMissing())
            {
                inMemoryQuartzOptions.SchedulerName = HybridConsts.DefaultSchedulerName;
            }
            IScheduler scheduler = new StdSchedulerFactory(SetProperties(inMemoryQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
        }

        private static NameValueCollection SetProperties(QuartzOptions inMemoryQuartzOptions)
        {
            var properties = new NameValueCollection();
            properties.Set(StdSchedulerFactory.PropertySchedulerName, inMemoryQuartzOptions.SchedulerName);
            properties.Set(StdSchedulerFactory.PropertyJobStoreType, "Quartz.Simpl.RAMJobStore, Quartz");
            //properties.Set(QuartzConsts.PropertySerializerType, inMemoryQuartzOptions.SerializerType.ToString());
            //properties.Remove(QuartzConsts.PropertyDataSourceDatabaseConnectionString);
            //properties.Remove(QuartzConsts.PropertyDataSourceDatabaseProvider);
            //properties.Remove(QuartzConsts.PropertyJobStoreDataSource);
            //properties.Remove(QuartzConsts.PropertyJobStoreDriverDelegateType);
            //properties.Remove(QuartzConsts.PropertyJobStoreTablePrefix);
            //properties.Remove(QuartzConsts.PropertyJobStoreUseProperties);

            //if (inMemoryQuartzOptions.IsClustered)
            //{
            //    //是否集群，集群模式下要设置为true
            //    properties["quartz.jobStore.clustered"] = "true";
            //    //集群模式下设置为auto，自动获取实例的Id，集群下一定要id不一样，不然不会自动恢复
            //    properties["quartz.scheduler.instanceId"] = "AUTO";
            //}

            if (inMemoryQuartzOptions.IsUseHistoryPlugin)
            {
                // 加载插件
                // properties.Set("quartz.plugin.自定义名称.type","命名空间.类名,程序集名称");
                properties.Set("quartz.plugin.InMemoryExecutionHistory.type", "Hybrid.Quartz.Plugins.History.InMemoryExecutionHistoryPlugin, Hybrid.Quartz");
                properties.Set("quartz.plugin.InMemoryExecutionHistory.storeType", "Hybrid.Quartz.Plugins.History.InMemoryExecutionHistoryStore, Hybrid.Quartz");
            }

            return properties;
        }
    }
}