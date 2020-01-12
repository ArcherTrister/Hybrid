// -----------------------------------------------------------------------
//  <copyright file="TestController.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.Linq;

namespace ESoftor.Web.Controllers
{
    [Description("网站-测试")]
    public class TestController : LocalApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value });
        }
    }
}