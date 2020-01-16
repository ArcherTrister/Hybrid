using Hybrid.Localization.Configuration;

using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Hybrid.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files in a directory.
    /// </summary>
    public class XmlFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        /// Creates a new <see cref="XmlFileLocalizationDictionaryProvider"/>.
        /// </summary>
        /// <param name="directoryPath">Path of the dictionary that contains all related XML files</param>
        public XmlFileLocalizationDictionaryProvider(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public override void Initialize(string sourceName, ILocalizationConfiguration localizationConfiguration)
        {
            string[] fileNames = Directory.GetFiles(_directoryPath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (string fileName in fileNames)
            {
                XmlLocalizationDictionary dictionary = CreateXmlLocalizationDictionary(fileName);
                string dicCultureInfoName = dictionary.CultureInfo.Name;
                if (Dictionaries.ContainsKey(dicCultureInfoName))
                {
                    throw new Exception(sourceName + " source contains more than one dictionary for the culture: " + dicCultureInfoName);
                }

                Dictionaries[dicCultureInfoName] = dictionary;

                LanguageIcon languageIcon = LocalizationConsts.LanguageIcons.Where(p => dicCultureInfoName.StartsWith(p.CountryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                localizationConfiguration.Languages.Add(
                    new LanguageInfo(dicCultureInfoName, dictionary.CultureInfo.NativeName, icon: languageIcon?.Icon, isDefault: CultureInfo.CurrentUICulture.Name.Equals(dicCultureInfoName)));

                if (!fileName.EndsWith(sourceName + ".xml")) continue;
                if (DefaultDictionary != null)
                {
                    throw new Exception("Only one default localization dictionary can be for source: " + sourceName);
                }

                DefaultDictionary = dictionary;
            }
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string fileName)
        {
            return XmlLocalizationDictionary.BuildFomFile(fileName);
        }
    }
}