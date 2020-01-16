using Hybrid.Quartz;
using Hybrid.Quartz.Dashboard;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DashboardBuilderExtensions
    {
        public static QuartzOptions UseDashboard(this QuartzOptions quartzOptions)
        {
            return quartzOptions.UseDashboard(opt => { });
        }

        public static QuartzOptions UseDashboard(this QuartzOptions quartzOptions, Action<DashboardQuartzOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            quartzOptions.RegisterExtension(new DashboardQuartzOptionsExtension(options));

            return quartzOptions;
        }
    }
}