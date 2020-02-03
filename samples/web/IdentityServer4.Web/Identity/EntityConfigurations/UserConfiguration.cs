// -----------------------------------------------------------------------
//  <copyright file="UserConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.Web.Identity.Entity.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfigurationBase<User, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(m => new { m.NormalizedUserName, m.IsDeleted }).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(m => new { m.NormalizedEmail, m.IsDeleted }).HasName("EmailIndex");

            builder.Property(m => m.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasData(new User()
            {
                Id = Guid.Parse("8d86feea-83d5-4a0c-9733-305ac6cfe58d"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                NickName = "SuperAdmin",
                Email = "Admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEB6lgMDV9JoidhR4cfIK+bKOQfo9eE6M02N68wV0KxCbx+c5gxkBrZWOp0FwI5Id8g==",
                AvatarUrl = null,
                SecurityStamp = "RRYXXETXCDKPXE6QPNDGLMCYNBA2ZF4P",
                ConcurrencyStamp = "e50ea89e-c966-4ade-8fe4-6fe94de83777",
                PhoneNumber = "18100000000",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                IsSystem = true,
                IsLocked = false,
                CreatedTime = new DateTime(1970, 1, 1),
                IsDeleted = false,
                Remark = null
            });
        }
    }
}