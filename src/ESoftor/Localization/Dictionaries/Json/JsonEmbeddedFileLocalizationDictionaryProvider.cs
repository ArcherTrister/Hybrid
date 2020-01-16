using Hybrid.Localization.Configuration;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hybrid.Localization.Dictionaries.Json
{
    /// <summary>
    /// Provides localization dictionaries from JSON files embedded into an <see cref="Assembly"/>.
    /// </summary>
    public class JsonEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;

        /// <summary>
        /// Creates a new <see cref="JsonEmbeddedFileLocalizationDictionaryProvider"/> object.
        /// </summary>
        /// <param name="assembly">Assembly that contains embedded json files</param>
        /// <param name="rootNamespace">
        /// <para>
        /// Namespace of the embedded json dictionary files
        /// </para>
        /// <para>
        /// Notice : Json folder name is different from Xml folder name.
        /// </para>
        /// <para>
        /// You must name it like this : Json**** and Xml****; Do not name : ****Json and ****Xml
        /// </para>
        /// </param>
        public JsonEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        public override void Initialize(string sourceName, ILocalizationConfiguration localizationConfiguration)
        {
            CultureInfo[] allCultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            List<string> resourceNames = _assembly.GetManifestResourceNames().Where(resourceName =>
                allCultureInfos.Any(culture => resourceName.EndsWith($"{sourceName}.json", true, null) ||
                                               resourceName.EndsWith($"{sourceName}-{culture.Name}.json", true,
                                                   null))).ToList();
            foreach (string resourceName in resourceNames)
            {
                if (!resourceName.StartsWith(_rootNamespace)) continue;
                using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
                {
                    string jsonString = Utf8Helper.ReadStringFromStream(stream);

                    JsonLocalizationDictionary dictionary = CreateJsonLocalizationDictionary(jsonString);
                    string dicCultureInfoName = dictionary.CultureInfo.Name;
                    if (Dictionaries.ContainsKey(dicCultureInfoName))
                    {
                        throw new Exception(sourceName + " source contains more than one dictionary for the culture: " + dicCultureInfoName);
                    }
                    Dictionaries[dicCultureInfoName] = dictionary;
                    LanguageIcon languageIcon = LocalizationConsts.LanguageIcons.Where(p => dicCultureInfoName.StartsWith(p.CountryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    localizationConfiguration.Languages.Add(
                        new LanguageInfo(dicCultureInfoName, dictionary.CultureInfo.NativeName, icon: languageIcon?.Icon, isDefault: CultureInfo.CurrentUICulture.Name.Equals(dicCultureInfoName)));

                    if (!resourceName.EndsWith(sourceName + ".json")) continue;
                    if (DefaultDictionary != null)
                    {
                        throw new Exception("Only one default localization dictionary can be for source: " + sourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual JsonLocalizationDictionary CreateJsonLocalizationDictionary(string jsonString)
        {
            return JsonLocalizationDictionary.BuildFromJsonString(jsonString);
        }
    }
}