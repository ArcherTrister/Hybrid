// -----------------------------------------------------------------------
//  <copyright file="CommonService.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-03 12:59</last-date>
// -----------------------------------------------------------------------

using Hybrid.Collections;
using Hybrid.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace LeXun.Demo.Common
{
    /// <summary>
    /// 业务实现：通用业务
    /// </summary>
    public class CommonService : ICommonContract, IScopeDependency
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化一个<see cref="CommonService"/>类型的新实例
        /// </summary>
        public CommonService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 测试测试
        /// </summary>
        public string Test()
        {
            List<object> list = new List<object>();

            ClaimsPrincipal user = _serviceProvider.GetCurrentUser();
            list.Add(user == null);
            list.Add(user?.GetType());
            list.Add(user?.Identity.Name);
            list.Add(user?.Identity.GetType());
            list.Add(user?.Identity.AuthenticationType);

            return list.ExpandAndToString("\r\n");
        }
    }
}