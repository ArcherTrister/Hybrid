// -----------------------------------------------------------------------
//  <copyright file="AdminApiController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc;
using Hybrid.Authorization;

using Microsoft.AspNetCore.Authorization;

namespace LeXun.Demo.Web.Areas.Admin.Controllers
{
    [AreaInfo("Admin", Display = "管理")]
    [RoleLimit]
    [Authorize(Policy = FunctionRequirement.HybridPolicy)]
    public abstract class AdminApiController : AreaApiController
    { }
}