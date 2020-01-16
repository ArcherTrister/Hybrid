// -----------------------------------------------------------------------
//  <copyright file="IAuditEntityProvider.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 4:29</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace Hybrid.EntityFrameworkCore
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