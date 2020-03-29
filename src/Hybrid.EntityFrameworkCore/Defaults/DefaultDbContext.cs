// -----------------------------------------------------------------------
//  <copyright file="DefaultDbContext.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 4:44</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;


namespace Hybrid.Entity
{
    /// <summary>
    /// 默认EntityFramework数据上下文
    /// </summary>
    public class DefaultDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultDbContext"/>类型的新实例
        /// </summary>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}