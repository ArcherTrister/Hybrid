// -----------------------------------------------------------------------
//  <copyright file="RoleConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using Hybrid.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.Web.Identity.Entity.EntityConfiguration
{
    public class RoleConfiguration : EntityTypeConfigurationBase<Role, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(m => new { m.NormalizedName, m.IsDeleted }).HasName("RoleNameIndex").IsUnique();

            builder.Property(m => m.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasData(new Role()
            {
                Id = Guid.Parse("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"),
                Name = "系统管理员",
                NormalizedName = "系统管理员",
                Remark = "系统最高权限管理角色",
                ConcurrencyStamp = "97313840-7874-47e5-81f2-565613c8cdcc",
                IsAdmin = true,
                IsSystem = true,
                IsDeleted = false,
                CreatedTime = new DateTime(1970, 1, 1)
            });
        }
    }
}