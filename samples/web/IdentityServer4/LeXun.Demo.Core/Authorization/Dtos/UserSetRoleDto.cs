// -----------------------------------------------------------------------
//  <copyright file="UserSetRoleDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-08 12:21</last-date>
// -----------------------------------------------------------------------

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// 用户设置角色DTO
    /// </summary>
    public class UserSetRoleDto
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 要设置的角色编号
        /// </summary>
        public int[] RoleIds { get; set; }
    }
}