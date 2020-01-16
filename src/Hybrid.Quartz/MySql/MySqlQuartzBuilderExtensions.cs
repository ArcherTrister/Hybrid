using Hybrid.Quartz;
using Hybrid.Quartz.MySql;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MySqlQuartzBuilderExtensions
    {
        public static QuartzOptions UseMySql(this QuartzOptions quartzOptions, Action<MySqlQuartzOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            quartzOptions.RegisterExtension(new MySqlQuartzOptionsExtension(options));

            return quartzOptions;
        }
    }
}