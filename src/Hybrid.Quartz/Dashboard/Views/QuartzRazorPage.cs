using Hybrid.AspNetCore.Mvc.Views;
using Hybrid.Localization;

namespace Hybrid.Quartz.Dashboard.Views
{
    public abstract class QuartzRazorPage<TModel> : BaseRazorPage<TModel>
    {
        protected QuartzRazorPage()
        {
            LocalizationSourceName = LocalizationConsts.QuartzSourceName;
        }
    }
}