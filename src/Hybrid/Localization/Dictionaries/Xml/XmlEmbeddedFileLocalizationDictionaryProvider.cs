using Hybrid.Localization.Configuration;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hybrid.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files embedded into an <see cref="Assembly"/>.
    /// </summary>
    public class XmlEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;

        /// <summary>
        /// Creates a new <see cref="XmlEmbeddedFileLocalizationDictionaryProvider"/> object.
        /// </summary>
        /// <param name="assembly">Assembly that contains embedded xml files</param>
        /// <param name="rootNamespace">Namespace of the embedded xml dictionary files</param>
        public XmlEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        public override void Initialize(string sourceName, ILocalizationConfiguration localizationConfiguration)
        {
            CultureInfo[] allCultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            List<string> resourceNames = _assembly.GetManifestResourceNames().Where(resourceName =>
                allCultureInfos.Any(culture => resourceName.EndsWith($"{sourceName}.xml", true, null) ||
                                               resourceName.EndsWith($"{sourceName}-{culture.Name}.xml", true,
                                                   null))).ToList();
            foreach (string resourceName in resourceNames)
            {
                if (!resourceName.StartsWith(_rootNamespace)) continue;
                using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
                {
                    string xmlString = Utf8Helper.ReadStringFromStream(stream);

                    XmlLocalizationDictionary dictionary = CreateXmlLocalizationDictionary(xmlString);
                    string dicCultureInfoName = dictionary.CultureInfo.Name;
                    if (Dictionaries.ContainsKey(dicCultureInfoName))
                    {
                        throw new Exception(sourceName + " source contains more than one dictionary for the culture: " + dicCultureInfoName);
                    }

                    Dictionaries[dicCultureInfoName] = dictionary;

                    LanguageIcon languageIcon = LocalizationConsts.LanguageIcons.Where(p => dicCultureInfoName.StartsWith(p.CountryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    localizationConfiguration.Languages.Add(
                        new LanguageInfo(dicCultureInfoName, dictionary.CultureInfo.NativeName, icon: languageIcon?.Icon, isDefault: CultureInfo.CurrentUICulture.Name.Equals(dicCultureInfoName)));

                    if (!resourceName.EndsWith(sourceName + ".xml")) continue;
                    if (DefaultDictionary != null)
                    {
                        throw new Exception("Only one default localization dictionary can be for source: " + sourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string xmlString)
        {
            return XmlLocalizationDictionary.BuildFomXmlString(xmlString);
        }
    }
}