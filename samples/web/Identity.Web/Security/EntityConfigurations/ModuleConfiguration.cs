// -----------------------------------------------------------------------
//  <copyright file="ModuleConfiguration.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore;
using ESoftor.Web.Security.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ESoftor.Web.EntityConfiguration.Security
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