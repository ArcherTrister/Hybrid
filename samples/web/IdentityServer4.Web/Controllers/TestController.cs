// -----------------------------------------------------------------------
//  <copyright file="TestController.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.Linq;

namespace Hybrid.Web.Controllers
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