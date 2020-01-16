using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hybrid.Localization
{
    public class LanguageManager : ILanguageManager
    {
        public LanguageInfo CurrentLanguage => GetCurrentLanguage();

        private readonly ILanguageProvider _languageProvider;

        public LanguageManager(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _languageProvider.GetLanguages();
        }

        private LanguageInfo GetCurrentLanguage()
        {
            IReadOnlyList<LanguageInfo> languages = _languageProvider.GetLanguages();
            if (languages.Count <= 0)
            {
                throw new Exception("No language defined in this application.");
            }

            string currentCultureName = CultureInfo.CurrentUICulture.Name;
            //string currentCultureName = LocalizationIocManager.CultureName;

            //if (string.IsNullOrEmpty(currentCultureName))
            //{
            //    return languages[0];
            //}

            //Try to find exact match
            LanguageInfo currentLanguage = languages.FirstOrDefault(l => l.Name.Equals(currentCultureName));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            currentLanguage = languages.FirstOrDefault(l => currentCultureName.StartsWith(l.Name));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            currentLanguage = languages.FirstOrDefault(l => l.IsDefault);
            return currentLanguage ?? languages[0];

            //Get first one
        }

        public LanguageInfo GetCurrentLanguage(string cultureName)
        {
            IReadOnlyList<LanguageInfo> languages = _languageProvider.GetLanguages();
            if (languages.Count <= 0)
            {
                throw new Exception("No language defined in this application.");
            }

            //Try to find exact match
            LanguageInfo currentLanguage = languages.FirstOrDefault(l => l.Name.Equals(cultureName));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            currentLanguage = languages.FirstOrDefault(l => cultureName.StartsWith(l.Name));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            currentLanguage = languages.FirstOrDefault(l => l.IsDefault);
            return currentLanguage ?? languages[0];
        }
    }
}