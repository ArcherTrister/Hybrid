using Hybrid.Localization.Sources;

using System.Collections.Generic;
using System.Globalization;

namespace Hybrid.Localization
{
    public class NullLocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullLocalizationManager Instance { get; } = new NullLocalizationManager();

        public LanguageInfo CurrentLanguage => new LanguageInfo(CultureInfo.CurrentUICulture.Name, CultureInfo.CurrentUICulture.DisplayName);

        private readonly IReadOnlyList<LanguageInfo> _emptyLanguageArray = new LanguageInfo[0];

        private readonly IReadOnlyList<ILocalizationSource> _emptyLocalizationSourceArray = new ILocalizationSource[0];

        private NullLocalizationManager()
        {
        }

        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _emptyLanguageArray;
        }

        public ILocalizationSource GetSource(string name)
        {
            return NullLocalizationSource.Instance;
        }

        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _emptyLocalizationSourceArray;
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}