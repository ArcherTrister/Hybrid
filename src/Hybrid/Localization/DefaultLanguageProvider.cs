using Hybrid.Localization.Configuration;

using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hybrid.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider
    {
        private readonly ILocalizationConfiguration _configuration;

        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}