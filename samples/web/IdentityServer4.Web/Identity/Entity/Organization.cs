// -----------------------------------------------------------------------
//  <copyright file="Organization.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESoftor.Web.Identity.Entity
{
    /// <summary>
    /// 实体类：组织机构
    /// </summary>
    [Description("组织机构信息")]
    public class Organization : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        [Required, DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [DisplayName("描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 父组织机构
        /// </summary>
        [DisplayName("父组织机构编号")]
        public Guid? ParentId { get; set; }
    }
}