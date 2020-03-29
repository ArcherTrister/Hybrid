﻿// -----------------------------------------------------------------------
//  <copyright file="UserRoleConfiguration.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-06 14:27</last-date>
// -----------------------------------------------------------------------

using System;

using LeXun.Demo.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Hybrid.Entity;


namespace LeXun.Demo.EntityConfiguration.Identity
{
    public partial class UserRoleConfiguration : EntityTypeConfigurationBase<UserRole, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(m => new { m.UserId, m.RoleId, m.DeletedTime }).HasName("UserRoleIndex").IsUnique();
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(m => m.RoleId);
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(m => m.UserId);

            EntityConfigurationAppend(builder);
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void EntityConfigurationAppend(EntityTypeBuilder<UserRole> builder);
    }
}