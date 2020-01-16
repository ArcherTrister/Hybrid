// -----------------------------------------------------------------------
//  <copyright file="RoleNode.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Web.Identity.Dtos
{
    /// <summary>
    /// 角色节点
    /// </summary>
    public class RoleNode
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}