using Hybrid.AspNetCore.Mvc.Views;
using Hybrid.Localization;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Views
{
    public abstract class IdentityServerViewComponent : BaseViewComponent
    {
        protected IdentityServerViewComponent()
        {
            LocalizationSourceName = LocalizationConsts.IdentityServerSourceName;
        }
    }
}