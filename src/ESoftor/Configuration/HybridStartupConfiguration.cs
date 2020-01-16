using Hybrid.Localization.Configuration;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Configuration
{
    internal class HybridStartupConfiguration : DictionaryBasedConfig, IHybridStartupConfiguration
    {
        public HybridStartupConfiguration(IServiceProvider serviceProvider)
        {
            Localization = serviceProvider.GetService<ILocalizationConfiguration>();
        }

        /// <summary>
        /// Used to set localization configuration.
        /// </summary>
        public ILocalizationConfiguration Localization { get; private set; }
    }
}