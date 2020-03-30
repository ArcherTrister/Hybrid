// -----------------------------------------------------------------------
//  <copyright file="AuditOperationConfiguration.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using LeXun.Demo.Systems.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace LeXun.Demo.EntityConfiguration.Systems
{
    public partial class AuditOperationConfiguration : EntityTypeConfigurationBase<AuditOperation, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<AuditOperation> builder)
        {
            EntityConfigurationAppend(builder);
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void EntityConfigurationAppend(EntityTypeBuilder<AuditOperation> builder);
    }
}