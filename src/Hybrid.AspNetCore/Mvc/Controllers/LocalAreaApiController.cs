﻿// -----------------------------------------------------------------------
//  <copyright file="LocalAreaApiController.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hybrid.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// WebApi的区域控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[area]/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = HybridConsts.LocalApi.AuthenticationScheme)]
    public abstract class LocalAreaApiController : ControllerBase
    {
        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger => HttpContext.RequestServices.GetLogger(GetType());
    }
}