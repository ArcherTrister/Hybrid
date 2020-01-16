using Microsoft.Extensions.DependencyInjection;

using Quartz;
using Quartz.Impl;

using System;
using System.Collections.Specialized;

namespace Hybrid.Quartz.InMemory
{
    /// <summary>
    ///
    /// </summary>
    internal class InMemoryStorageOptionsExtension : IQuartzOptionsExtension
    {
        private readonly Action<InMemoryStorageQuartzOptions> _inMemoryQuartzOptions;

        public InMemoryStorageOptionsExtension(Action<InMemoryStorageQuartzOptions> inMemoryQuartzOptions)
        {
            _inMemoryQuartzOptions = inMemoryQuartzOptions;
        }

        public void AddServices(IServiceCollection services)
        {
            var inMemoryQuartzOptions = new InMemoryStorageQuartzOptions();

            _inMemoryQuartzOptions?.Invoke(inMemoryQuartzOptions);

            services.AddSingleton(inMemoryQuartzOptions);

            IScheduler scheduler = new StdSchedulerFactory(SetProperties(inMemoryQuartzOptions)).GetScheduler().Result;
            services.AddSingleton(scheduler);
        }

        protected NameValueCollection SetProperties(InMemoryStorageQuartzOptions inMemoryQuartzOptions)
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