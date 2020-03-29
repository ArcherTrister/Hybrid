// -----------------------------------------------------------------------
//  <copyright file="IAuditEntityProvider.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 4:29</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Hybrid.Audits;


namespace Hybrid.Entity
{
    /// <summary>
    /// 定义数据审计信息提供者
    /// </summary>
    public interface IAuditEntityProvider
    {
        /// <summary>
        /// 从指定上下文中获取数据审计信息
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <returns>数据审计信息的集合</returns>
        IEnumerable<AuditEntityEntry> GetAuditEntities(DbContext context);
    }
}