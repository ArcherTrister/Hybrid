using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace Hybrid.Quartz.Dashboard
{
    public interface IDashboardAuthorizationFilter
    {
        bool Authorize([NotNull] HttpContext context, DashboardQuartzOptions dashboardQuartzOptions);
    }
}