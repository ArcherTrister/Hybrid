// -----------------------------------------------------------------------
//  <copyright file="SiteApiController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-07 1:12</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc;
using Hybrid.Authorization;

using Microsoft.AspNetCore.Authorization;

using System.ComponentModel;

namespace LeXun.Demo.Web.Controllers
{
    [DisplayName("网站")]
    [Authorize(Policy = FunctionRequirement.HybridPolicy)]
    public abstract class SiteApiController : ApiController
    { }
}