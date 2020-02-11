using Hybrid.Localization;

using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard.Views.Shared.Components.TopBarLanguageSwitch
{
    public class TopBarLanguageSwitchViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }
}