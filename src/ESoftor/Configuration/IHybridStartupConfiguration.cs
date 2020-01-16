using Hybrid.Localization.Configuration;

namespace Hybrid.Configuration
{
    public interface IHybridStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Used to set localization configuration.
        /// </summary>
        ILocalizationConfiguration Localization { get; }
    }
}