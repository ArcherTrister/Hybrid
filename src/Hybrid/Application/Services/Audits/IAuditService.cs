﻿// -----------------------------------------------------------------------
//  <copyright file="IAuditService.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Domain.Entities.Auditing;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Application.Services.Audits
{
    /// <summary>
    /// 业务契约：审计模块
    /// </summary>
    public interface IAuditService : IApplicationService
    {
        #region 操作审计信息业务

        /// <summary>
        /// 获取 操作审计信息查询数据集
        /// </summary>
        IQueryable<AuditOperation> AuditOperations { get; }

        /// <summary>
        /// 删除操作审计信息信息
        /// </summary>
        /// <param name="ids">要删除的操作审计信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteAuditOperations(params Guid[] ids);

        #endregion 操作审计信息业务

        #region 数据审计信息业务

        /// <summary>
        /// 获取 数据审计信息查询数据集
        /// </summary>
        IQueryable<AuditEntity> AuditEntities { get; }

        /// <summary>
        /// 获取 数据属性审计信息查询数据集
        /// </summary>
        IQueryable<AuditProperty> AuditProperties { get; }

        /// <summary>
        /// 删除数据审计信息信息
        /// </summary>
        /// <param name="ids">要删除的数据审计信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteAuditEntities(params Guid[] ids);

        #endregion 数据审计信息业务
    }
}