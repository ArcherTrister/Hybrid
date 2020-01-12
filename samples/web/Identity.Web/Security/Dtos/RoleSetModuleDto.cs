// -----------------------------------------------------------------------
//  <copyright file="RoleSetModuleDto.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-07-08 13:10</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Web.Security.Dtos
{
    /// <summary>
    /// 角色设置权限DTO
    /// </summary>
    public class RoleSetModuleDto
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 获取或设置 要设置的模块编号集合
        /// </summary>
        public Guid[] ModuleIds { get; set; }
    }
}