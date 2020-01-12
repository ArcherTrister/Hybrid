// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthorizationFilter" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 15:11:41</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Threading.Tasks;

namespace ESoftor.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 功能权限授权验证
    /// </summary>
    public class FunctionAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            return Task.CompletedTask;
        }
    }
}
