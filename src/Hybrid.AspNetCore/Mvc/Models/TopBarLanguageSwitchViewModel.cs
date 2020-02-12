using Hybrid.Localization;

using System.Collections.Generic;

namespace Hybrid.AspNetCore.Mvc.Models
{
    public class TopBarLanguageSwitchViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }
}