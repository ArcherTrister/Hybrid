// -----------------------------------------------------------------------
//  <copyright file="UserDetail.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Identity.Entity
{
    /// <summary>
    /// 实体类：用户详细信息
    /// </summary>
    [Description("用户详细信息")]
    public class UserDetail : EntityBase<Guid>
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
        public Guid UserId { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual User User { get; set; }
    }
}