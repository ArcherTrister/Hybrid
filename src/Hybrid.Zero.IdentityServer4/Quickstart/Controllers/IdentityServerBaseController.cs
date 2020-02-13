﻿using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    /// <summary>
    /// Base class for all MVC Controllers in IdentityServer system.
    /// </summary>
    public class IdentityServerBaseController : MvcController
    {
        public IdentityServerBaseController()
        {
            LocalizationSourceName = LocalizationConsts.IdentityServerSourceName;
        }
    }
}