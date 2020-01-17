using Hybrid.Data;
using Hybrid.Extensions;

using Hybrid.Localization.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

namespace Hybrid.Localization.Dictionaries
{
    /// <summary>
    /// This class is used to build a localization source
    /// which works on memory based dictionaries to find strings.
    /// </summary>
    public class DictionaryBasedLocalizationSource : IDictionaryBasedLocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// </summary>
        public string Name { get; }

        public ILocalizationDictionaryProvider DictionaryProvider { get; }

        protected ILocalizationConfiguration LocalizationConfiguration { get; private set; }

        private ILogger _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dictionaryProvider"></param>
        public DictionaryBasedLocalizationSource(string name, ILocalizationDictionaryProvider dictionaryProvider)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNull(dictionaryProvider, nameof(dictionaryProvider));

            Name = name;
            DictionaryProvider = dictionaryProvider;
        }

        /// <inheritdoc/>
        public virtual void Initialize(ILocalizationConfiguration configuration, IServiceProvider provider)
        {
            LocalizationConfiguration = configuration;
            _logger = provider.GetLogger<DictionaryBasedLocalizationSource>();

            DictionaryProvider.Initialize(Name, LocalizationConfiguration);
        }

        /// <inheritdoc/>
        public string GetString(string name)
        {
            return GetString(name, CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc/>
        public string GetString(string name, CultureInfo culture)
        {
            string value = GetStringOrNull(name, culture);

            return value ?? ReturnGivenNameOrThrowException(name, culture);
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return GetStringOrNull(name, CultureInfo.CurrentUICulture, tryDefaults);
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            string cultureName = culture.Name;
            IDictionary<string, ILocalizationDictionary> dictionaries = DictionaryProvider.Dictionaries;

            //Try to get from original dictionary (with country code)
            if (dictionaries.TryGetValue(cultureName, out ILocalizationDictionary originalDictionary))
            {
                LocalizedString strOriginal = originalDictionary.GetOrNull(name);
                if (strOriginal != null)
                {
                    return strOriginal.Value;
                }
            }

            if (!tryDefaults)
            {
                return null;
            }

            //Try to get from same language dictionary (without country code)
            if (cultureName.Contains("-")) //Example: "tr-TR" (length=5)
            {
                if (dictionaries.TryGetValue(GetBaseCultureName(cultureName), out ILocalizationDictionary langDictionary))
                {
                    LocalizedString strLang = langDictionary.GetOrNull(name);
                    if (strLang != null)
                    {
                        return strLang.Value;
                    }
                }
            }

            //Try to get from default language
            ILocalizationDictionary defaultDictionary = DictionaryProvider.DefaultDictionary;

            LocalizedString strDefault = defaultDictionary?.GetOrNull(name);

            return strDefault?.Value;
        }

        /// <inheritdoc/>
        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture, includeDefaults);
        }

        /// <inheritdoc/>
        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            //TODO: Can be optimized (example: if it's already default dictionary, skip overriding)

            IDictionary<string, ILocalizationDictionary> dictionaries = DictionaryProvider.Dictionaries;

            //Create a temp dictionary to build
            var allStrings = new Dictionary<string, LocalizedString>();

            if (includeDefaults)
            {
                //Fill all strings from default dictionary
                ILocalizationDictionary defaultDictionary = DictionaryProvider.DefaultDictionary;
                if (defaultDictionary != null)
                {
                    foreach (LocalizedString defaultDictString in defaultDictionary.GetAllStrings())
                    {
                        allStrings[defaultDictString.Name] = defaultDictString;
                    }
                }

                //Overwrite all strings from the language based on country culture
                if (culture.Name.Contains("-"))
                {
                    if (dictionaries.TryGetValue(GetBaseCultureName(culture.Name), out ILocalizationDictionary langDictionary))
                    {
                        foreach (var langString in langDictionary.GetAllStrings())
                        {
                            allStrings[langString.Name] = langString;
                        }
                    }
                }
            }

            //Overwrite all strings from the original dictionary
            if (!dictionaries.TryGetValue(culture.Name, out ILocalizationDictionary originalDictionary))
                return allStrings.Values.ToImmutableList();
            foreach (LocalizedString originalLangString in originalDictionary.GetAllStrings())
            {
                allStrings[originalLangString.Name] = originalLangString;
            }

            return allStrings.Values.ToImmutableList();
        }

        /// <summary>
        /// Extends the source with given dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source</param>
        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            DictionaryProvider.Extend(dictionary);
        }

        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(
                LocalizationConfiguration,
                Name,
                name,
                culture,
                _logger
            );
        }

        private static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Left(cultureName.IndexOf("-", StringComparison.Ordinal))
                : cultureName;
        }
    }
}