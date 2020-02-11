using Hybrid.Data;
using Hybrid.Quartz.Dashboard.Models.Dtos;
using Hybrid.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 仪表盘
    /// </summary>
    public class DashboardController : QuartzBaseController
    {
        private readonly ISchedulerManager _schedulerManager;

        public DashboardController(ISchedulerManager schedulerManager)
        {
            _schedulerManager = schedulerManager;
        }

        public async Task<IActionResult> Index()
        {
            SchedulerHeaderDto schedulerHeader = await _schedulerManager.GetCurrentScheduler(LocalSchedulerName);

            string cookieValue = Crypto.DesEncrypt(schedulerHeader.Name);

            Response.Cookies.Append(
                HybridConstants.SchedulerCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(2),
                    HttpOnly = true
                }
            );

            return View();
        }
    }
}