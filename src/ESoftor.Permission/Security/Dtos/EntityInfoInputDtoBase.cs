// -----------------------------------------------------------------------
//  <copyright file="EntityInfoInputDtoBase.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities;

using System;
using System.ComponentModel;

namespace ESoftor.Permission.Security.Dtos
{
    /// <summary>
    /// 输入Dto基类：实体信息
    /// </summary>
    public abstract class EntityInfoInputDtoBase : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据审计
        /// </summary>
        [DisplayName("是否数据审计")]
        public bool AuditEnabled { get; set; }
    }
}