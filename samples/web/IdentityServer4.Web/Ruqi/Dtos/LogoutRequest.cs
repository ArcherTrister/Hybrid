﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Web.Ruqi.Dtos
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; }
    }
}