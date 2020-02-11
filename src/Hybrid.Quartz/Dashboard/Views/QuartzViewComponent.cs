using Hybrid.AspNetCore.Mvc.Views;

namespace Hybrid.Quartz.Dashboard.Views
{
    public abstract class QuartzViewComponent : BaseViewComponent
    {
        protected QuartzViewComponent()
        {
            LocalizationSourceName = QuartzConsts.LocalizationSourceName;
        }
    }
}