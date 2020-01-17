// -----------------------------------------------------------------------
//  <copyright file="RoleSetModuleDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-08 13:10</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Web.Security.Dtos
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