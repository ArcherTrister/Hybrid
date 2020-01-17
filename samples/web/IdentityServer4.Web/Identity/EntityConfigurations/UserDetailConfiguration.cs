// -----------------------------------------------------------------------
//  <copyright file="UserDetailConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using Hybrid.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.Web.Identity.Entity.EntityConfiguration
{
    public class UserDetailConfiguration : EntityTypeConfigurationBase<UserDetail, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasOne(ud => ud.User).WithOne(u => u.UserDetail).HasForeignKey<UserDetail>(ud => ud.UserId).IsRequired();
        }
    }
}