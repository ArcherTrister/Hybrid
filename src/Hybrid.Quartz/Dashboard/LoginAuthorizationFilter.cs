using Microsoft.AspNetCore.Http;

using System;
using System.Linq;
using System.Security.Claims;

namespace Hybrid.Quartz.Dashboard
{
    public class LoginAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(HttpContext context, DashboardQuartzOptions dashboardQuartzOptions)
        {
            bool isAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated) return false;
            string role = context.User.Claims.FirstOrDefault(o => o.Type.Equals(ClaimTypes.Role))?.Value;
            return role != null && role.Equals("admin", StringComparison.OrdinalIgnoreCase);
        }
    }
}