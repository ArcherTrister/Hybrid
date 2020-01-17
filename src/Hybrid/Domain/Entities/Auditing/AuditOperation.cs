﻿// -----------------------------------------------------------------------
//  <copyright file="AuditOperation.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:07</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits;
using Hybrid.Data;
using Hybrid.Mapping;

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hybrid.Domain.Entities.Auditing
{
    /// <summary>
    /// 实体类：审计操作信息
    /// </summary>
    [MapFrom(typeof(AuditOperationEntry))]
    [Description("审计操作信息")]
    public class AuditOperation : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 执行的功能名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 获取或设置 当前用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置 当前用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 当前访问IP
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 获取或设置 操作系统
        /// </summary>
        public string OperationSystem { get; set; }

        /// <summary>
        /// 获取或设置 浏览器
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 获取或设置 当前访问UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 获取或设置 操作结果
        /// </summary>
        public AjaxResultType ResultType { get; set; } = AjaxResultType.Success;

        /// <summary>
        /// 获取或设置 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置 执行耗时，单位毫秒
        /// </summary>
        public int Elapsed { get; set; }

        /// <summary>
        /// 获取或设置 信息添加时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 审计实体信息集合
        /// </summary>
        public virtual ICollection<AuditEntity> AuditEntities { get; set; } = new List<AuditEntity>();
    }
}