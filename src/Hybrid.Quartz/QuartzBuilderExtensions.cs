using Hybrid.Configuration;
using Hybrid.Extensions;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Dictionaries.Json;
using Hybrid.Quartz;
using Hybrid.Quartz.Dashboard;
using Hybrid.Quartz.Dashboard.Hubs.LiveLog;
using Hybrid.Quartz.Plugins.LiveLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

using Quartz;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// app extensions for <see cref="IApplicationBuilder" />
    /// </summary>
    internal static class QuartzBuilderExtensions
    {
        internal static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            IServiceProvider provider = app.ApplicationServices;

            //ServiceLocator.Instance.SetApplicationServiceProvider(provider);

            //var loggingProvider = provider.GetService<LoggingProvider>();
            //if (loggingProvider == null)
            //{
            //    throw new InvalidOperationException(
            //        "AddQuartz() must be called on the service collection.   eg: services.AddQuartz(...)");
            //}

            var scheduler = provider.GetService<IScheduler>();
            if (scheduler == null)
            {
                throw new InvalidOperationException(
                    "You must be config used message queue provider at AddQuartz() options!   eg: services.AddQuartz(options=>{ options.UseInMemory(...) })");
            }

            //:jobFactory quartz.net Execute 使用依赖注入
            //var jobFactory = provider.GetService<JobFactory>();

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

            var dashboardQuartzOptions = provider.GetService<DashboardQuartzOptions>();

            if (dashboardQuartzOptions == null) return app;

            app.UseDashboard(dashboardQuartzOptions);

            return app;
        }

        /// <summary>
        /// 使用仪表板
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseDashboard(this IApplicationBuilder app, DashboardQuartzOptions options)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#else
            app.UseExceptionHandler(options.ErrorPath);
#endif

            app.UseStaticFiles();

            IServiceProvider provider = app.ApplicationServices;
            var Configuration = provider.GetService<IHybridStartupConfiguration>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    QuartzConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QuartzOptions).GetAssembly(), "Hybrid.Quartz.Dashboard.Localization.Sources.JsonSource"
            )));

            //todo:
            app.UseAuthentication();

            app.UseMiddleware<DashboardMiddeware>(options);

            ////todo:UseSignalR
            //app.UseSignalR(routes =>
            //{
            //    //这里要说下，为啥地址要写 /api/xxx
            //    //如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的MVC的路由，而且自己定义的路由
            //    routes.MapHub<LiveLogHub>("/api/liveLogHub");
            //});

            app.UseConnections(routes =>
            {
                //var hubRouteBuilder = provider.GetService<HubRouteBuilder>();
                //hubRouteBuilder.MapHub<LiveLogHub>("/api/liveLogHub");
                //这里要说下，为啥地址要写 /api/xxx
                //如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的MVC的路由，而且自己定义的路由
                new HubRouteBuilder(routes).MapHub<LiveLogHub>("/api/liveLogHub");
            });

            //app.UseMvc(routes =>
            //{
            //    //https://github.com/aspnet/AspNetCore/issues/7772
            //    //routes.MapAreaRoute(
            //    //    name: "QuartzAreaRoute",
            //    //    areaName: "Quartz",
            //    //    template: "Quartz/{controller=Home}/{action=Index}/{id?}"
            //    //);
            //    routes.MapRoute(
            //        name: options.RouteName,
            //        template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            //    );
            //});

            // Verify if AddMvc was done before calling UseMvc
            // We use the MvcMarkerService to make sure if all the services were added.
            if (provider.GetService(typeof(MvcMarkerService)) == null)
            {
                throw new InvalidOperationException(
                    "AddMvc() must be called on the service collection.   eg: services.AddMvc(...)");
            }

            //var middlewarePipelineBuilder = app.ApplicationServices.GetRequiredService<MiddlewareFilterBuilder>();
            //middlewarePipelineBuilder.ApplicationBuilder = app.New();

            var routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = provider.GetRequiredService<MvcRouteHandler>(),
            };

            //configureRoutes(routes);

            //routeBuilder.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(app.ApplicationServices));

            routeBuilder.MapRoute(options.RouteName, "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            app.UseRouter(routeBuilder.Build());

            //app.UseRouter(routes=> {
            //    routes.MapRoute(
            //        name: options.RouteName,
            //        template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            //    );
            //});

            return app;
        }
    }

    internal sealed class QuartzStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseQuartz();
                next(app);
            };
        }
    }
}