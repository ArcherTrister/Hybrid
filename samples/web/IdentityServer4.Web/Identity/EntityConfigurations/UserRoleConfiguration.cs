// -----------------------------------------------------------------------
//  <copyright file="UserRoleConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-06 14:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.Web.Identity.Entity.EntityConfiguration
{
    public class UserRoleConfiguration : EntityTypeConfigurationBase<UserRole, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(m => new { m.UserId, m.RoleId, m.IsDeleted }).HasName("UserRoleIndex").IsUnique();
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(m => m.RoleId);
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(m => m.UserId);
            builder.HasData(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("8d86feea-83d5-4a0c-9733-305ac6cfe58d"),
                RoleId = Guid.Parse("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"),
                IsLocked = false,
                CreatedTime = new DateTime(1970, 1, 1)
            });
        }
    }
}