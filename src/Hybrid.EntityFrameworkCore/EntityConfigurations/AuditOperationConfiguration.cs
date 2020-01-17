// -----------------------------------------------------------------------
//  <copyright file="AuditOperationConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities.Auditing;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Hybrid.EntityFrameworkCore.EntityConfigurations
{
    public class AuditOperationConfiguration : EntityTypeConfigurationBase<AuditOperation, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<AuditOperation> builder)
        { }
    }
}