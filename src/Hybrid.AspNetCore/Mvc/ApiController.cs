// -----------------------------------------------------------------------
//  <copyright file="ApiController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-04 20:30</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Hybrid.AspNetCore.Mvc.Filters;


namespace Hybrid.AspNetCore.Mvc
{
    /// <summary>
    /// WebApi控制器基类
    /// </summary>
    [AuditOperation]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ApiController : Controller
    {
        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger => HttpContext.RequestServices.GetLogger(GetType());
    }
}