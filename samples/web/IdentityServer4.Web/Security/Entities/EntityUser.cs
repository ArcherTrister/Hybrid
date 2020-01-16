// -----------------------------------------------------------------------
//  <copyright file="EntityUser.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.EntityInfos;
using Hybrid.Zero.Security;
using Hybrid.Web.Identity.Entity;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Security.Entities
{
    /// <summary>
    /// 实体：数据用户信息
    /// </summary>
    [Description("数据用户信息")]
    public class EntityUser : EntityUserBase<Guid>
    {
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取或设置 所属实体信息
        /// </summary>
        public virtual EntityInfo EntityInfo { get; set; }
    }
}