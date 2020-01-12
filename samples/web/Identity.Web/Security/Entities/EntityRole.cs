// -----------------------------------------------------------------------
//  <copyright file="EntityRole.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.EntityInfos;
using ESoftor.Permission.Security;
using ESoftor.Web.Identity.Entity;

using System;
using System.ComponentModel;

namespace ESoftor.Web.Security.Entities
{
    /// <summary>
    /// 实体：数据角色信息
    /// </summary>
    [Description("数据角色信息")]
    public class EntityRole : EntityRoleBase<Guid>
    {
        /// <summary>
        /// 获取或设置 所属角色信息
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 获取或设置 所属实体信息
        /// </summary>
        public virtual EntityInfo EntityInfo { get; set; }
    }
}