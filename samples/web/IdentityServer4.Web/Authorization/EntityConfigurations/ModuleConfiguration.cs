// -----------------------------------------------------------------------
//  <copyright file="ModuleConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using Hybrid.EntityFrameworkCore;
using Hybrid.Web.Authorization.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.Web.EntityConfiguration.Security
{
    /// <summary>
    /// 模块信息映射配置类
    /// </summary>
    public class ModuleConfiguration : EntityTypeConfigurationBase<Module, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).IsRequired(false);

            builder.HasData(
                new Module() { Id = Guid.Parse("eb972ec2-e163-45e8-b314-aaef01716b06"), Name = "根节点", Remark = "系统根节点", Code = "Root", OrderCode = 1, TreePathString = "$eb972ec2-e163-45e8-b314-aaef01716b06$" }
            );
        }
    }
}