// -----------------------------------------------------------------------
//  <copyright file="DefaultDbContext.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 4:44</last-date>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using System;

namespace ESoftor.EntityFrameworkCore.Defaults
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