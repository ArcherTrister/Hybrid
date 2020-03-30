// -----------------------------------------------------------------------
//  <copyright file="ModuleRole.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Entities;

using LeXun.Demo.Identity.Entities;

using System.ComponentModel;

namespace LeXun.Demo.Authorization.Entities
{
    /// <summary>
    /// 实体类：模块角色信息
    /// </summary>
    [Description("角色模块信息")]
    public class ModuleRole : ModuleRoleBase<int, int>
    {
        /// <summary>
        /// 获取或设置 模块信息
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}