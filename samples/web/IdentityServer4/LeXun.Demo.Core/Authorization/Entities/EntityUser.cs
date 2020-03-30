// -----------------------------------------------------------------------
//  <copyright file="EntityUser.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Entities;
using Hybrid.Authorization.EntityInfos;

using LeXun.Demo.Identity.Entities;

using System.ComponentModel;

namespace LeXun.Demo.Authorization.Entities
{
    /// <summary>
    /// 实体：数据用户信息
    /// </summary>
    [Description("数据用户信息")]
    public class EntityUser : EntityUserBase<int>
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