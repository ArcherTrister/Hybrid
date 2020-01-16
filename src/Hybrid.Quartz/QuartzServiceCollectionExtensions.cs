using Hybrid.Quartz;
using Hybrid.Quartz.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class QuartzServiceCollectionExtensions
    {
        /// <summary>
        /// Adds and configures the consistence services for the consistency.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <param name="setupAction">An action to configure the <see cref="QuartzOptions" />.</param>
        /// <returns>An <see cref="IServiceCollection" /> for application services.</returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, Action<QuartzOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            //Options and extension service
            var options = new QuartzOptions();
            setupAction.Invoke(options);

            //List<IQuartzOptionsExtension> quartzOptionsExtensions = options.Extensions.Where(p => p is MySqlQuartzOptionsExtension || p is SqlServerQuartzOptionsExtension || p is InMemoryStorageOptionsExtension).ToList();

            //if (quartzOptionsExtensions.Count > 1)
            //{
            //    throw new ArgumentException("目前只允许配置一种持久化类型");
            //}

            foreach (IQuartzOptionsExtension serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.AddSingleton(options);

            //初始化定时任务查找器
            services.TryAddSingleton<IJobTypeFinder, JobTypeFinder>();
            //注入所有IJob
            services.InitializeJob();

            services.AddTransient<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<IJobFactory, JobFactory>();
            //services.AddSingleton(typeof(IJobFactory), new JobFactory(services.BuildServiceProvider()));
            //services.AddSingleton(new JobFactory(services.BuildServiceProvider()));
            //services.AddTransient<LoggingProvider>();

            //Startup and Middleware
            services.AddTransient<IStartupFilter, QuartzStartupFilter>();

            return services;
        }
    }
}