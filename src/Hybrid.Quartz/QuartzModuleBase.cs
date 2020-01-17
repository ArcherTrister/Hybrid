using Hybrid.AspNetCore;
using Hybrid.Core.Modules;
using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.EventBuses;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Quartz.Extensions;
using Hybrid.Quartz.Plugins.LiveLog;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using System;

namespace Hybrid.Quartz
{
    /// <summary>
    /// Quartz模块基类
    /// </summary>
    [DependsOnModules(typeof(EventBusModule), typeof(AspNetCoreModule))]
    public abstract class QuartzModuleBase : AspHybridModule
    {
        private bool _enabled = true;

        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Framework;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //TODO:Options
            IConfiguration configuration = services.GetConfiguration();
            _enabled = configuration["Hybrid:Quartz:Enabled"].CastTo(false);
            if (!_enabled)
            {
                return services;
            }
            QuartzStorageType storageType = configuration["Hybrid:Quartz:StorageType"].CastTo(default(QuartzStorageType));

            if (storageType.Equals(QuartzStorageType.InMemory))
            {
                QuartzOptions quartzOptions = configuration.GetSection("Hybrid:Quartz").Get<QuartzOptions>();
                services.UseInMemoryStorage(quartzOptions);
            }
            else if (storageType.Equals(QuartzStorageType.SqlServer)) {
                QuartzOptions quartzOptions = configuration.GetSection("Hybrid:Quartz").Get<QuartzOptions>();
                services.UseSqlServer(quartzOptions);
            }
            else if(storageType.Equals(QuartzStorageType.SqlServer)){
                QuartzOptions quartzOptions = configuration.GetSection("Hybrid:Quartz").Get<QuartzOptions>();
                services.UseMySql(quartzOptions);
            }
            else
            {
                throw new HybridException("配置文件中Quartz节点的StorageType错误");
            }

            //初始化定时任务查找器
            services.TryAddSingleton<IJobTypeFinder, JobTypeFinder>();
            //注入所有IJob
            services.InitializeJobs();

            services.AddTransient<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<IJobFactory, JobFactory>();
            //services.AddSingleton(typeof(IJobFactory), new JobFactory(services.BuildServiceProvider()));
            //services.AddSingleton(new JobFactory(services.BuildServiceProvider()));
            //services.AddTransient<LoggingProvider>();
            return base.AddServices(services);
        }

        public override void UseModule(IApplicationBuilder app)
        {
            if (!_enabled)
            {
                return;
            }
            IServiceProvider provider = app.ApplicationServices;

            var scheduler = provider.GetService<IScheduler>();

            //:jobFactory quartz.net Execute 使用依赖注入
            //scheduler.JobFactory = jobFactory ?? throw new InvalidOperationException(
            //        "You must be config used message queue provider at AddQuartz() options!   eg: services.AddQuartz(options=>{ options.UseInMemory(...) })");

            scheduler.JobFactory = new JobFactory(provider);

            //LogProvider.SetCurrentLogProvider(loggingProvider);

            var liveLogPlugin = new LiveLogPlugin(provider);
            scheduler.ListenerManager.AddJobListener(liveLogPlugin);
            scheduler.ListenerManager.AddTriggerListener(liveLogPlugin);
            scheduler.ListenerManager.AddSchedulerListener(liveLogPlugin);

            scheduler.Start();

            //todo:启动 关闭
            //var lifetime = provider.GetService<IApplicationLifetime>();
            //lifetime.ApplicationStarted.Register(() =>
            //{
            //    scheduler.Start().Wait(); //网站启动完成执行
            //});

            //lifetime.ApplicationStopped.Register(() =>
            //{
            //    scheduler.Shutdown().Wait(); //网站停止完成执行
            //});

            //var dashboardQuartzOptions = provider.GetService<DashboardQuartzOptions>();

            //if (dashboardQuartzOptions == null) return;

            //app.UseDashboard(dashboardQuartzOptions);

            base.UseModule(app);
        }
    }
}