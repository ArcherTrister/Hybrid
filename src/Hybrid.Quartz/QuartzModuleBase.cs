using Hybrid.AspNetCore;
using Hybrid.Core.Configuration;
using Hybrid.Core.Packs;
using Hybrid.Data;
using Hybrid.EventBuses;
using Hybrid.Exceptions;
using Hybrid.Localization;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Dictionaries.Json;
using Hybrid.Quartz.Dashboard;
using Hybrid.Quartz.MySql;
using Hybrid.Quartz.Plugins.LiveLog;
using Hybrid.Quartz.SqlServer;
using Hybrid.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.AdoJobStore;
using Quartz.Spi;

using System;

namespace Hybrid.Quartz
{
    /// <summary>
    /// Quartz模块基类
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack), typeof(AspNetCorePack))]
    public abstract class QuartzModuleBase : AspHybridPack
    {
        private bool _enabled = true;

        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            QuartzConfiguration quartzOptions = configuration.GetSection("Hybrid:Quartz").Get<QuartzConfiguration>();
            _enabled = quartzOptions.IsEnabled;
            // _useDashboard = quartzOptions.UseDashboard;
            if (!_enabled)
            {
                return services;
            }
            if (string.IsNullOrWhiteSpace(quartzOptions.TablePrefix))
            {
                quartzOptions.TablePrefix = AdoConstants.DefaultTablePrefix;
            }
            if (string.IsNullOrWhiteSpace(quartzOptions.SchedulerName))
            {
                quartzOptions.SchedulerName = HybridConstants.DefaultSchedulerName;
            }
            services.AddSingleton(quartzOptions);
            if (quartzOptions.StorageType.Equals(QuartzStorageType.InMemory))
            {
                services.UseInMemoryStorage(quartzOptions);
            }
            else if (quartzOptions.StorageType.Equals(QuartzStorageType.SqlServer))
            {
                services.UseSqlServer(quartzOptions);
            }
            else if (quartzOptions.StorageType.Equals(QuartzStorageType.MySql))
            {
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

            UseQuartzUI(services);

            return base.AddServices(services);
        }

        public override void UsePack(IApplicationBuilder app)
        {
            if (!_enabled)
            {
                return;
            }

            IServiceProvider provider = app.ApplicationServices;

            var quartzOptions = provider.GetRequiredService<QuartzConfiguration>();
            if (quartzOptions.StorageType.Equals(QuartzStorageType.InMemory))
            {
                // 初始化数据库
                MySqlObjectsInstaller.Initialize(quartzOptions.ConnectionStringOrCacheName, quartzOptions.TablePrefix);
            }
            if (quartzOptions.StorageType.Equals(QuartzStorageType.SqlServer))
            {
                // 初始化SqlServer数据库
                SqlServerObjectsInstaller.Initialize(quartzOptions.ConnectionStringOrCacheName, quartzOptions.TablePrefix);
            }

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

            scheduler.Start().Wait();

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

            var Configuration = provider.GetService<IHybridStartupConfiguration>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LocalizationConsts.QuartzSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QuartzModuleBase).GetAssembly(), "Hybrid.Quartz.Dashboard.Localization.Sources.JsonSource"
            )));

            app.UseSignalR(routes =>
            {
                //这里要说下，为啥地址要写 /api/xxx
                //如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的MVC的路由，而且自己定义的路由
                routes.MapHub<LiveLogHub>("/api/liveLogHub");
            });

            base.UsePack(app);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        private static void UseQuartzUI(IServiceCollection services)
        {
            //services.AddAuthentication(dashboardQuartzOptions.AuthScheme)
            //    .AddCookie(dashboardQuartzOptions.AuthScheme, options =>
            //    {
            //        options.LoginPath = dashboardQuartzOptions.LoginPath;
            //    });

            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.AreaViewLocationFormats.Clear();
            //    options.AreaViewLocationFormats.Add("/Dashboard/Views/{1}/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Dashboard/Views/Shared/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //});

            services.AddTransient<ISchedulerManager, SchedulerManager>();

            services.ConfigureOptions(typeof(DashboardQuartzUiConfigureOptions));

            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //不改变参数大小写
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略在对象图中找到的循环引用
            //    //options.SerializerSettings.Converters = new JsonConverter[]
            //    //{
            //    //    new HexLongConverter()
            //    //};
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new HybridQuartzViewLocationExpander());
            });
        }
    }
}