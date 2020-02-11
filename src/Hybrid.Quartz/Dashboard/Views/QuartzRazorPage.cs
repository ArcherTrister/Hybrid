using Hybrid.AspNetCore.Mvc.Views;

namespace Hybrid.Quartz.Dashboard.Views
{
    public abstract class QuartzRazorPage<TModel> : BaseRazorPage<TModel>
    {
        protected QuartzRazorPage()
        {
            LocalizationSourceName = QuartzConsts.LocalizationSourceName;
        }
    }
}