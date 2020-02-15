// -----------------------------------------------------------------------
//  <copyright file="LoginLog.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Identity.Entities
{
    /// <summary>
    /// 实体类：用户登录日志
    /// </summary>
    [Description("用户登录日志")]
    public class LoginLog : EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 初始化一个<see cref="LoginLog"/>类型的新实例
        /// </summary>
        public LoginLog()
        {
            Id = CombGuid.NewGuid();
        }

        /// <summary>
        /// 获取或设置 登录IP
        /// </summary>
        [DisplayName("登录IP")]
        public string Ip { get; set; }

        /// <summary>
        /// 获取或设置 用户代理头
        /// </summary>
        [DisplayName("用户代理头")]
        public string UserAgent { get; set; }

        /// <summary>
        /// 获取或设置 退出时间
        /// </summary>
        [DisplayName("退出时间")]
        public DateTime? LogoutTime { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 获取或设置 所属用户
        /// </summary>
        public virtual User User { get; set; }
    }
}