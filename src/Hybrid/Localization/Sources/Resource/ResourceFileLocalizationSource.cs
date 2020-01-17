using Hybrid.Dependency;
using Hybrid.Localization.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Hybrid.Localization.Sources.Resource
{
    /// <summary>
    /// This class is used to simplify to create a localization source that
    /// uses resource a file.
    /// </summary>
    public class ResourceFileLocalizationSource : ILocalizationSource, ISingletonDependency
    {
        /// <summary>
        /// Unique Name of the source.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Reference to the <see cref="ResourceManager"/> object related to this localization source.
        /// </summary>
        public ResourceManager ResourceManager { get; }

        private ILogger _logger;

        private ILocalizationConfiguration _configuration;

        /// <param name="name">Unique Name of the source</param>
        /// <param name="resourceManager">Reference to the <see cref="ResourceManager"/> object related to this localization source</param>
        public ResourceFileLocalizationSource(string name, ResourceManager resourceManager)
        {
            Name = name;
            ResourceManager = resourceManager;
        }

        /// <summary>
        /// This method is called by Hybrid before first usage.
        /// </summary>
        public virtual void Initialize(ILocalizationConfiguration configuration, IServiceProvider provider)
        {
            _configuration = configuration;
            _logger = provider.GetLogger<ResourceFileLocalizationSource>();
        }

        public virtual string GetString(string name)
        {
            string value = GetStringOrNull(name);
            return value ?? ReturnGivenNameOrThrowException(name, CultureInfo.CurrentUICulture);
        }

        public virtual string GetString(string name, CultureInfo culture)
        {
            string value = GetStringOrNull(name, culture);
            return value ?? ReturnGivenNameOrThrowException(name, culture);
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            //WARN: tryDefaults is not implemented!
            return ResourceManager.GetString(name);
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            //WARN: tryDefaults is not implemented!
            return ResourceManager.GetString(name, culture);
        }

        /// <summary>
        /// Gets all strings in current language.
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture, includeDefaults);
        }

        /// <summary>
        /// Gets all strings in specified culture.
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return ResourceManager
                .GetResourceSet(culture, true, includeDefaults)
                .Cast<DictionaryEntry>()
                .Select(entry => new LocalizedString(entry.Key.ToString(), entry.Value.ToString(), culture))
                .ToImmutableList();
        }

        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(
                _configuration,
                Name,
                name,
                culture,
                _logger
            );
        }
    }
}