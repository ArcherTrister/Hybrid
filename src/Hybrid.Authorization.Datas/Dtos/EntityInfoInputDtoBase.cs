// -----------------------------------------------------------------------
//  <copyright file="EntityInfoInputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-11-15 17:21</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using System;
using System.ComponentModel;

namespace Hybrid.Authorization.Dtos
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