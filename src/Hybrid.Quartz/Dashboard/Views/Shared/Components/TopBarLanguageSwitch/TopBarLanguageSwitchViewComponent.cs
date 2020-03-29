
using Microsoft.AspNetCore.Mvc;

using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Localization;

using System.Linq;

namespace Hybrid.Quartz.Dashboard.Views.Shared.Components.TopBarLanguageSwitch
{
    public class TopBarLanguageSwitchViewComponent : QuartzViewComponent
    {
        private readonly ILanguageManager _languageManager;

        public TopBarLanguageSwitchViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        public IViewComponentResult Invoke()
        {
            var model = new TopBarLanguageSwitchViewModel
            {
                CurrentLanguage = _languageManager.GetCurrentLanguage(LocalCultureInfo.Name),
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).Distinct(new LanguageInfoCompare()).ToList() // _languageManager.GetLanguages().Where(l => !l.IsDisabled).GroupBy(p => p.Name).Select(x=>x.First()).ToList()
            };

            return View(model);
        }
    }
}