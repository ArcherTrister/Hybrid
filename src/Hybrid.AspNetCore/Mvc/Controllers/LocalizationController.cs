using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

using System;

namespace Hybrid.AspNetCore.Mvc.Controllers
{
    public class LocalizationController : MvcController
    {
        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet]
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
                HybridConstants.CultureCookieName,
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
                return Json(new AjaxResult());
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl.IsLocalUrl(Request))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/"); //: Go to app root
        }
    }
}