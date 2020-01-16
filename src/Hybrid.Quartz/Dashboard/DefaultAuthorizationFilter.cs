using Microsoft.AspNetCore.Http;

using System.Net;

namespace Hybrid.Quartz.Dashboard
{
    public class DefaultAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(HttpContext context, DashboardQuartzOptions dashboardQuartzOptions)
        {
            IPAddress remoteIp = context.Connection.RemoteIpAddress;
            if (remoteIp == null)
                return false;

            if (IPAddress.IsLoopback(remoteIp))
                return true;

            if (remoteIp == context.Connection.LocalIpAddress)
                return true;

            return false;
        }
    }
}