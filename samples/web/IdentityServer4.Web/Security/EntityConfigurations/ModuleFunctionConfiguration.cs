// -----------------------------------------------------------------------
//  <copyright file="ModuleFunctionConfiguration.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Functions;
using ESoftor.EntityFrameworkCore;
using ESoftor.Web.Security.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace ESoftor.Web.EntityConfiguration.Security
{
    /// <summary>
    /// 模块功能信息映射配置类
    /// </summary>
    public class ModuleFunctionConfiguration : EntityTypeConfigurationBase<ModuleFunction, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<ModuleFunction> builder)
        {
            builder.HasIndex(m => new { m.ModuleId, m.FunctionId }).HasName("ModuleFunctionIndex").IsUnique();

            builder.HasOne<Module>(mf => mf.Module).WithMany().HasForeignKey(m => m.ModuleId);
            builder.HasOne<Function>(mf => mf.Function).WithMany().HasForeignKey(m => m.FunctionId);
        }
    }
}