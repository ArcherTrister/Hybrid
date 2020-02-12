using Hybrid.AspNetCore.Mvc.Views;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Views
{
    public abstract class IdentityServerRazorPage<TModel> : BaseRazorPage<TModel>
    {
        protected IdentityServerRazorPage()
        {
            LocalizationSourceName = IdentityServerConsts.LocalizationSourceName;
        }
    }
}