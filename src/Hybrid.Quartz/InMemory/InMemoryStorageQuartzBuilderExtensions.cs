using Hybrid.Quartz;
using Hybrid.Quartz.InMemory;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryStorageQuartzBuilderExtensions
    {
        public static QuartzOptions UseInMemoryStorage(this QuartzOptions quartzOptions)
        {
            return quartzOptions.UseInMemoryStorage(opt => { });
        }

        public static QuartzOptions UseInMemoryStorage(this QuartzOptions quartzOptions, Action<InMemoryStorageQuartzOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            quartzOptions.RegisterExtension(new InMemoryStorageOptionsExtension(options));

            return quartzOptions;
        }
    }
}