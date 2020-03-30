// -----------------------------------------------------------------------
//  <copyright file="RoleOutputDto2.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;
using Hybrid.Mapping;

using LeXun.Demo.Identity.Entities;

namespace LeXun.Demo.Identity.Dtos
{
    /// <summary>
    /// 简单角色输出DTO
    /// </summary>
    [MapFrom(typeof(Role))]
    public class RoleOutputDto2 : IOutputDto
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否管理
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}