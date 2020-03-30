using Hybrid.Dependency;
using Hybrid.Localization.Sources;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Globalization;

namespace Hybrid.Localization
{
    /// <summary>
    /// This static class is used to simplify getting localized strings.
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Gets a reference to the localization manager.
        /// Inject and use <see cref="ILocalizationManager"/>
        /// wherever it's possible, instead of this shortcut.
        /// </summary>
        public static ILocalizationManager Manager { get { return LocalizationManager.Value; } }

        private static readonly Lazy<ILocalizationManager> LocalizationManager;

        static LocalizationHelper()
        {
            //LocalizationManager = new Lazy<ILocalizationManager>(
            //    () => LocalizationIocManager.ServiceProvider.IsRegistered<ILocalizationManager>()
            //        ? LocalizationIocManager.ServiceProvider.GetRequiredService<ILocalizationManager>()
            //        : NullLocalizationManager.Instance
            //    );

            LocalizationManager = new Lazy<ILocalizationManager>(
                () => ServiceLocator.Instance.ScopedProvider.IsRegistered<ILocalizationManager>()
                    ? ServiceLocator.Instance.ScopedProvider.GetRequiredService<ILocalizationManager>()
                    : NullLocalizationManager.Instance
                );
        }

        /// <summary>
        /// Gets a pre-registered localization source.
        /// </summary>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name)
        {
            return LocalizationManager.Value.GetString(sourceName, name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }
    }
}