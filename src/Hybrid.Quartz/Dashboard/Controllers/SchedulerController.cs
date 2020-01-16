using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Extensions;
using Hybrid.Helpers;
using Hybrid.Quartz.Dashboard.Models.Dtos;
using Hybrid.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Quartz;

using System;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 调度程序
    /// </summary>
    public class SchedulerController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string schedulerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            SchedulerMetaData metaData = await scheduler.GetMetaData().ConfigureAwait(false);
            return View(new SchedulerDto(scheduler, metaData));
        }

        public async virtual Task<IActionResult> ChangeScheduler(string schedulerName)
        {
            if (!await GlobalizationHelper.IsValidSchedulerName(schedulerName))
            {
                throw new Exception("Unknown scheduler: " + schedulerName + ". It must be a valid scheduler!");
            }

            string cookieValue = Crypto.DesEncrypt(schedulerName);

            Response.Cookies.Append(
                HybridConsts.SchedulerCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(2),
                    HttpOnly = true
                }
            );

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResponse());
            }

            string queryString = Request.QueryString.Value;

            string returnUrl = queryString.Split("returnUrl=")[1];

            if (!string.IsNullOrWhiteSpace(returnUrl) && UrlHelper.IsLocalUrl(Request, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/"); //: Go to app root
        }
    }
}