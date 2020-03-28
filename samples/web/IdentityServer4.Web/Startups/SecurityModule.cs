// -----------------------------------------------------------------------
//  <copyright file="SecurityModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.Authorization.EntityInfos;
using Hybrid.Authorization.Functions;
using Hybrid.Web.Authorization;
using Hybrid.Web.Authorization.Dtos;
using Hybrid.Web.Authorization.Entities;
using Hybrid.Zero.Authorization;
using Hybrid.Zero.Authorization.Dtos;
using System;
using System.ComponentModel;

namespace Hybrid.Web.Startups
{
    /// <summary>
    /// 权限安全模块
    /// </summary>
    [Description("权限安全模块")]
    public class SecurityModule
        : SecurityModuleBase<SecurityManager, FunctionAuthorization, FunctionAuthCache, DataAuthCache, ModuleHandler,
            Function, FunctionInputDto, EntityInfo, EntityInfoInputDto, Module, ModuleInputDto, Guid, ModuleFunction,
            ModuleRole, ModuleUser, EntityRole, EntityRoleInputDto, Guid, Guid>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;
    }
}