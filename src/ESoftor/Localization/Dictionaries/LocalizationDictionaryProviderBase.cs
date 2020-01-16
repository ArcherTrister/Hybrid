using Hybrid.Localization.Configuration;

using System.Collections.Generic;

namespace Hybrid.Localization.Dictionaries
{
    public abstract class LocalizationDictionaryProviderBase : ILocalizationDictionaryProvider
    {
        public string SourceName { get; private set; }

        public ILocalizationConfiguration LocalizationConfiguration { get; private set; }

        public ILocalizationDictionary DefaultDictionary { get; protected set; }

        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }

        protected LocalizationDictionaryProviderBase()
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();
        }

        public virtual void Initialize(string sourceName, ILocalizationConfiguration localizationConfiguration)
        {
            SourceName = sourceName;
            LocalizationConfiguration = localizationConfiguration;
        }

        public void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            if (!Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out ILocalizationDictionary existingDictionary))
            {
                Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            IReadOnlyList<LocalizedString> localizedStrings = dictionary.GetAllStrings();
            foreach (LocalizedString localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}