// -----------------------------------------------------------------------
//  <copyright file="UserDetail.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using System.ComponentModel;

namespace LeXun.Demo.Identity.Entities
{
    /// <summary>
    /// 实体类：用户详细信息
    /// </summary>
    [Description("用户详细信息")]
    [TableNamePrefix("Identity")]
    public class UserDetail : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 注册IP
        /// </summary>
        [DisplayName("注册IP")]
        public string RegisterIp { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual User User { get; set; }
    }
}