using System.Collections.Generic;

namespace Hybrid.Localization
{
    public interface ILanguageManager
    {
        LanguageInfo CurrentLanguage { get; }

        LanguageInfo GetCurrentLanguage(string cultureName);

        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}