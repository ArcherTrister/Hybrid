// -----------------------------------------------------------------------
//  <copyright file="AuditEntity.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:17</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities.Auditing;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace ESoftor.EntityFrameworkCore.EntityConfigurations
{
    public class AuditEntityConfiguration : EntityTypeConfigurationBase<AuditEntity, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<AuditEntity> builder)
        {
            builder.HasIndex(m => m.OperationId);
            builder.HasOne(m => m.Operation).WithMany(n => n.AuditEntities).HasForeignKey(m => m.OperationId);
        }
    }
}