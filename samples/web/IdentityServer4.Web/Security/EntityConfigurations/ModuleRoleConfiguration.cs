﻿// -----------------------------------------------------------------------
//  <copyright file="ModuleRoleConfiguration.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore;
using ESoftor.Web.Identity.Entity;
using ESoftor.Web.Security.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace ESoftor.Web.EntityConfiguration.Security
{
    /// <summary>
    /// 模块角色信息映射配置类
    /// </summary>
    public class ModuleRoleConfiguration : EntityTypeConfigurationBase<ModuleRole, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<ModuleRole> builder)
        {
            builder.HasIndex(m => new { m.ModuleId, m.RoleId }).HasName("ModuleRoleIndex").IsUnique();

            builder.HasOne<Module>(mr => mr.Module).WithMany().HasForeignKey(m => m.ModuleId);
            builder.HasOne<Role>(mr => mr.Role).WithMany().HasForeignKey(m => m.RoleId);
        }
    }
}