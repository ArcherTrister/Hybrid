using Hybrid.Localization.Configuration;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Hybrid.Localization.Sources
{
    /// <summary>
    /// Null object pattern for <see cref="ILocalizationSource"/>.
    /// </summary>
    public class NullLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullLocalizationSource Instance { get; } = new NullLocalizationSource();

        public string Name => null;

        private readonly IReadOnlyList<LocalizedString> _emptyStringArray = new LocalizedString[0];

        private NullLocalizationSource()
        {
        }

        public void Initialize(ILocalizationConfiguration configuration, IServiceProvider provider)
        {
        }

        public string GetString(string name)
        {
            return name;
        }

        public string GetString(string name, CultureInfo culture)
        {
            return name;
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return null;
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return _emptyStringArray;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return _emptyStringArray;
        }
    }
}