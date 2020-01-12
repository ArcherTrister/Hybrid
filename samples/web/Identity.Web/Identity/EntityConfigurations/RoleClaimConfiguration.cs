﻿// -----------------------------------------------------------------------
//  <copyright file="RoleClaimConfiguration.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using ESoftor.Entity;
using ESoftor.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ESoftor.Web.Identity.Entity.EntityConfiguration
{
    public class RoleClaimConfiguration : EntityTypeConfigurationBase<RoleClaim, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasOne(rc => rc.Role).WithMany(r => r.RoleClaims).HasForeignKey(m => m.RoleId).IsRequired();
        }
    }
}