// -----------------------------------------------------------------------
//  <copyright file="DiagnosticsController.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : IdentityServerBaseController
    {
        public async Task<IActionResult> Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
            if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return NotFound();
            }

            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
            return View(model);
        }
    }
}