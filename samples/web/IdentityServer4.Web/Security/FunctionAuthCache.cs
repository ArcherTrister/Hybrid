// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthCache.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Web.Identity.Entities;
using Hybrid.Web.Security.Entities;
using Hybrid.Zero.Authorization;

using System;

namespace Hybrid.Web.Security
{
    /// <summary>
    /// 功能权限缓存
    /// </summary>
    public class FunctionAuthCache : FunctionAuthCacheBase<ModuleFunction, ModuleRole, ModuleUser, Function, Module, Guid, Role, Guid, User, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="FunctionAuthCacheBase{TModuleFunction, TModuleRole, TModuleUser, TFunction, TModule, TModuleKey,TRole, TRoleKey, TUser, TUserKey}"/>类型的新实例
        /// </summary>
        public FunctionAuthCache(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }
    }
}