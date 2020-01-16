using Hybrid.Quartz;
using Hybrid.Quartz.SqlServer;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerQuartzBuilderExtensions
    {
        public static QuartzOptions UseSqlServer(this QuartzOptions quartzOptions, Action<SqlServerQuartzOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            quartzOptions.RegisterExtension(new SqlServerQuartzOptionsExtension(options));

            return quartzOptions;
        }
    }
}