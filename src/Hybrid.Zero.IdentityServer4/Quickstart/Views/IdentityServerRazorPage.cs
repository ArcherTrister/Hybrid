using Hybrid.AspNetCore.Mvc.Views;
using Hybrid.Localization;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Views
{
    public abstract class IdentityServerRazorPage<TModel> : BaseRazorPage<TModel>
    {
        protected IdentityServerRazorPage()
        {
            LocalizationSourceName = LocalizationConsts.IdentityServerSourceName;
        }
    }
}