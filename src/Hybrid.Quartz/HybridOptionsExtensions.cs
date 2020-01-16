using Hybrid;
using Hybrid.Quartz;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HybridOptionsExtensions
    {
        public static HybridOptions AddQuartz(this HybridOptions options, Action<QuartzOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new QuartzHybridOptionsExtension(x =>
            {
                configure(x);
                x.Version = options.Version;
            }));

            return options;
        }
    }
}