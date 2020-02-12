using Hybrid.AspNetCore.Mvc.Views;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Views
{
    public abstract class IdentityServerViewComponent : BaseViewComponent
    {
        protected IdentityServerViewComponent()
        {
            LocalizationSourceName = IdentityServerConsts.LocalizationSourceName;
        }
    }
}