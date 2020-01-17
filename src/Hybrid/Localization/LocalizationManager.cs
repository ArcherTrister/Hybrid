using Hybrid.Localization.Configuration;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Sources;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hybrid.Localization
{
    internal class LocalizationManager : ILocalizationManager
    {
        public ILogger Logger { get; set; }

        //private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;

        private readonly IServiceProvider _provider;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationManager(
            //ILanguageManager languageManager,
            ILocalizationConfiguration configuration,
            IServiceProvider provider
            )
        {
            Logger = NullLogger.Instance;
            //_languageManager = languageManager;
            _configuration = configuration;
            _provider = provider;
            _sources = new Dictionary<string, ILocalizationSource>();
        }

        public void Initialize()
        {
            InitializeSources();
        }

        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                Logger.LogDebug("Localization disabled.");
                return;
            }

            Logger.LogDebug(string.Format("Initializing {0} localization sources.", _configuration.Sources.Count));
            foreach (ILocalizationSource source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new Exception("There are more than one source with name: " + source.Name + "! Source name must be unique!");
                }

                _sources[source.Name] = source;
                source.Initialize(_configuration, _provider);

                //Extending dictionaries
                if (!(source is IDictionaryBasedLocalizationSource dictionaryBasedSource)) continue;
                List<LocalizationSourceExtensionInfo> extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                foreach (LocalizationSourceExtensionInfo extension in extensions)
                {
                    extension.DictionaryProvider.Initialize(dictionaryBasedSource.Name, _configuration);
                    foreach (ILocalizationDictionary extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
                    {
                        dictionaryBasedSource.Extend(extensionDictionary);
                    }
                }

                Logger.LogDebug("Initialized localization source: " + source.Name);
            }
        }

        /// <summary>
        /// Gets a localization source with name.
        /// </summary>
        /// <param name="name">Unique name of the localization source</param>
        /// <returns>The localization source</returns>
        public ILocalizationSource GetSource(string name)
        {
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!_sources.TryGetValue(name, out ILocalizationSource source))
            {
                throw new ArgumentException("Can not find a source with name: " + name);
            }

            return source;
        }

        /// <summary>
        /// Gets all registered localization sources.
        /// </summary>
        /// <returns>List of sources</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}