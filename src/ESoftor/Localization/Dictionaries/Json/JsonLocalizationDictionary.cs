﻿
using ESoftor.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Hybrid.Localization.Dictionaries.Json
{
    /// <summary>
    ///     This class is used to build a localization dictionary from json.
    /// </summary>
    /// <remarks>
    ///     Use static Build methods to create instance of this class.
    /// </remarks>
    public class JsonLocalizationDictionary : LocalizationDictionary
    {
        /// <summary>
        ///     Private constructor.
        /// </summary>
        /// <param name="cultureInfo">Culture of the dictionary</param>
        private JsonLocalizationDictionary(CultureInfo cultureInfo)
            : base(cultureInfo)
        {
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionary" /> from given file.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static JsonLocalizationDictionary BuildFromFile(string filePath)
        {
            try
            {
                return BuildFromJsonString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Invalid localization file format! " + filePath, ex);
            }
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionary" /> from given json string.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static JsonLocalizationDictionary BuildFromJsonString(string jsonString)
        {
            JsonLocalizationFile jsonFile;
            try
            {
                jsonFile = JsonConvert.DeserializeObject<JsonLocalizationFile>(
                    jsonString,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            catch (JsonException ex)
            {
                throw new JsonException("Can not parse json string. " + ex.Message);
            }

            string cultureCode = jsonFile.Culture;
            if (string.IsNullOrEmpty(cultureCode))
            {
                throw new ArgumentNullException(nameof(jsonFile), "Culture is empty in language json file.");
            }

            var dictionary = new JsonLocalizationDictionary(CultureInfo.GetCultureInfo(cultureCode));
            var duplicateNames = new List<string>();
            foreach (KeyValuePair<string, string> item in jsonFile.Texts)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new KeyNotFoundException("The key is empty in given json string.");
                }

                if (dictionary.Contains(item.Key))
                {
                    duplicateNames.Add(item.Key);
                }

                dictionary[item.Key] = item.Value.NormalizeLineEndings();
            }

            if (duplicateNames.Count > 0)
            {
                throw new Exception(
                    "A dictionary can not contain same key twice. There are some duplicated names: " +
                    duplicateNames.JoinAsString(", "));
            }

            return dictionary;
        }
    }
}