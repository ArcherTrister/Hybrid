// -----------------------------------------------------------------------
//  <copyright file="HomeController.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Models;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class IdentityServerController : IdentityServerBaseController
    {
        private readonly IIdentityServerInteractionService _interaction;

        //private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        public IdentityServerController(IIdentityServerInteractionService interaction,
            //IWebHostEnvironment environment,
            ILogger<IdentityServerController> logger)
        {
            _interaction = interaction;
            //_environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //if (_environment.IsDevelopment())
            //{
            //    // only show in development
            //    return View();
            //}

            //_logger.LogInformation("Homepage is disabled in production. Returning 404.");
            //return NotFound();
            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new HybridErrorViewModel();
            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = new ErrorInfos
                {
                    ClientId = message.ClientId,
                    DisplayMode = message.DisplayMode,
                    Error = message.Error,
                    ErrorDescription = message.ErrorDescription,
                    RedirectUri = message.RedirectUri,
                    RequestId = message.RequestId,
                    ResponseMode = message.ResponseMode,
                    UiLocales = message.UiLocales
                };

                //if (!_environment.IsDevelopment())
                //{
                //    // only show in development
                //    message.ErrorDescription = null;
                //}
            }

            return View(vm);
        }
    }
}