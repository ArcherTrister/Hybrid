using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Data;
using Hybrid.Quartz.Dashboard.Models.Dtos;
using Hybrid.Security;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Diagnostics;
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
                HybridConsts.SchedulerCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(2),
                    HttpOnly = true
                }
            );

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new HybridErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}