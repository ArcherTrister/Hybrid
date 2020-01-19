﻿// -----------------------------------------------------------------------
//  <copyright file="SecurityController" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 14:05:58</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc;
using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.Core.Functions;
using Hybrid.Core.ModuleInfos;
using Hybrid.Extensions;
using Hybrid.Security;
using Hybrid.Web.Security;
using Hybrid.Web.Security.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace IdentityServer4.Web.Controllers
{
    [Description("网站-授权")]
    [ModuleInfo(Order = 2)]
    public class SecurityController : LocalApiController
    {
        private readonly SecurityManager _securityManager;
        private readonly ILogger<SecurityController> _logger;

        public SecurityController(
            SecurityManager securityManager,
            ILoggerFactory loggerFactory)
        {
            _securityManager = securityManager;
            _logger = loggerFactory.CreateLogger<SecurityController>();
        }

        /// <summary>
        /// 检查URL授权
        /// </summary>
        /// <param name="url">要检查的URL</param>
        /// <returns>是否有权</returns>
        [HttpGet]
        [ModuleInfo]
        [Description("检查URL授权")]
        public bool CheckUrlAuth(string url)
        {
            bool ok = this.CheckFunctionAuth(url);
            return ok;
        }

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <returns>权限节点</returns>
        [HttpGet]
        [ModuleInfo]
        [Description("获取授权信息")]
        public List<string> GetAuthInfo()
        {
            Module[] modules = _securityManager.Modules.ToArray();
            List<AuthItem> list = new List<AuthItem>();
            foreach (Module module in modules)
            {
                if (CheckFuncAuth(module, out bool empty))
                {
                    list.Add(new AuthItem { Code = GetModuleTreeCode(module, modules), HasFunc = !empty });
                }
            }
            List<string> codes = new List<string>();
            foreach (AuthItem item in list)
            {
                if (item.HasFunc)
                {
                    codes.Add(item.Code);
                }
                else if (list.Any(m => m.Code.Length > item.Code.Length && m.Code.Contains(item.Code) && m.HasFunc))
                {
                    codes.Add(item.Code);
                }
            }
            return codes;
        }

        /// <summary>
        /// 验证是否拥有指定模块的权限
        /// </summary>
        /// <param name="module">要验证的模块</param>
        /// <param name="empty">返回模块是否为空模块，即是否分配有功能</param>
        /// <returns></returns>
        private bool CheckFuncAuth(Module module, out bool empty)
        {
            IServiceProvider services = HttpContext.RequestServices;
            IFunctionAuthorization authorization = services.GetRequiredService<IFunctionAuthorization>();

            Function[] functions = _securityManager.ModuleFunctions.Where(m => m.ModuleId == module.Id).Select(m => m.Function).ToArray();
            empty = functions.Length == 0;
            if (empty)
            {
                return true;
            }

            foreach (Function function in functions)
            {
                if (!authorization.Authorize(function, User).IsOk)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取模块的树形路径代码串
        /// </summary>
        private static string GetModuleTreeCode(Module module, Module[] source)
        {
            var pathIds = module.TreePathIds;
            string[] names = pathIds.Select(m => source.First(n => n.Id == m)).Select(m => m.Code).ToArray();
            return names.ExpandAndToString(".");
        }

        private class AuthItem
        {
            public string Code { get; set; }

            public bool HasFunc { get; set; }
        }
    }
}