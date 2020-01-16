using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Extensions;
using Hybrid.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

using System;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    public class LocalizationController : QuartzBaseController
    {
        public virtual ActionResult ChangeCulture(string cultureName)
        {
            if (!Localization.GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new Exception("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            string queryString = Request.QueryString.Value;

            string returnUrl = queryString.Split("returnUrl=")[1];

            string cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName, cultureName));

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(2),
                    HttpOnly = true
                }
            );

            //LocalizationIocManager.CultureName = cultureName;

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResponse());
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && UrlHelper.IsLocalUrl(Request, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/"); //: Go to app root
        }
    }
}