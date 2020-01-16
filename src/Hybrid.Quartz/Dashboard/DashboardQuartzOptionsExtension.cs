using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Quartz.Dashboard
{
    /// <summary>
    ///
    /// </summary>
    internal sealed class DashboardQuartzOptionsExtension : IQuartzOptionsExtension
    {
        private readonly Action<DashboardQuartzOptions> _dashboardQuartzOptions;

        public DashboardQuartzOptionsExtension(Action<DashboardQuartzOptions> dashboardQuartzOptions)
        {
            _dashboardQuartzOptions = dashboardQuartzOptions;
        }

        public void AddServices(IServiceCollection services)
        {
            var dashboardQuartzOptions = new DashboardQuartzOptions();

            _dashboardQuartzOptions?.Invoke(dashboardQuartzOptions);

            services.AddSingleton(dashboardQuartzOptions);

            services.AddAuthentication(dashboardQuartzOptions.AuthScheme)
                .AddCookie(dashboardQuartzOptions.AuthScheme, options =>
                {
                    options.LoginPath = dashboardQuartzOptions.LoginPath;
                });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Dashboard/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Dashboard/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddTransient<ISchedulerManager, SchedulerManager>();

            services.ConfigureOptions(typeof(DashboardQuartzUiConfigureOptions));
        }
    }
}