﻿// -----------------------------------------------------------------------
//  <copyright file="FunctionInputDtoBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Zero.Security.Dtos
{
    /// <summary>
    /// 输入Dto基类：功能信息
    /// </summary>
    public class FunctionInputDtoBase : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 访问类型
        /// </summary>
        [DisplayName("访问类型")]
        public FunctionAccessType AccessType { get; set; }

        /// <summary>
        /// 获取或设置 是否启用操作审计
        /// </summary>
        [DisplayName("是否操作审计")]
        public bool AuditOperationEnabled { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据审计
        /// </summary>
        [DisplayName("是否数据审计")]
        public bool AuditEntityEnabled { get; set; }

        /// <summary>
        /// 获取或设置 数据缓存时间（秒）
        /// </summary>
        [DisplayName("数据缓存秒数")]
        public int CacheExpirationSeconds { get; set; }

        /// <summary>
        /// 获取或设置 是否相对过期时间，否则为绝对过期
        /// </summary>
        [DisplayName("是否相对过期时间")]
        public bool IsCacheSliding { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
    }
}