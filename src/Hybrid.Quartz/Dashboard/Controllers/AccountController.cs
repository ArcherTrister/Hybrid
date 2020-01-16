using Hybrid.Quartz.Dashboard.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    public class AccountController : QuartzBaseController
    {
        private readonly DashboardQuartzOptions _dashboardQuartzOptions;

        public AccountController(DashboardQuartzOptions dashboardQuartzOptions)
        {
            _dashboardQuartzOptions = dashboardQuartzOptions;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = "/Quartz")
        {
            if (!_dashboardQuartzOptions.QuartzAuthUsers.Any())
            {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var claimIdenetiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdenetiy));

                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                };

                await HttpContext.SignInAsync(_dashboardQuartzOptions.AuthScheme, new ClaimsPrincipal(claimIdenetiy), props);

                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            // If we got this far, something failed, redisplay form
            if (!ModelState.IsValid) return View(model);
            QuartzAuthUser user = _dashboardQuartzOptions.QuartzAuthUsers.FirstOrDefault(o => o.UserName.Equals(model.UserName) && o.Password.Equals(model.Password));
            if (user != null)
            {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,user.RoleName)
                };
                var claimIdenetiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdenetiy));

                AuthenticationProperties props = null;
                if (model.RememberMe)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                    };
                };

                await HttpContext.SignInAsync(_dashboardQuartzOptions.AuthScheme, new ClaimsPrincipal(claimIdenetiy), props);

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(_dashboardQuartzOptions.AuthScheme);
            return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
        }
    }
}