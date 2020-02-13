using Hybrid.AspNetCore.Mvc.Views;
using Hybrid.Localization;

namespace Hybrid.Quartz.Dashboard.Views
{
    public abstract class QuartzViewComponent : BaseViewComponent
    {
        protected QuartzViewComponent()
        {
            LocalizationSourceName = LocalizationConsts.QuartzSourceName;
        }
    }
}