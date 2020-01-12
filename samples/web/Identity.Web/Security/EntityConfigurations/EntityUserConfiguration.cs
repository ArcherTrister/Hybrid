// -----------------------------------------------------------------------
//  <copyright file="EntityUserConfiguration.cs" company="ESoftor��Դ�Ŷ�">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>������</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.EntityInfos;
using ESoftor.EntityFrameworkCore;
using ESoftor.Web.Identity.Entity;
using ESoftor.Web.Security.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;


namespace ESoftor.Web.EntityConfiguration.Security
{
    public class EntityUserConfiguration : EntityTypeConfigurationBase<EntityUser, Guid>
    {
        /// <summary>
        /// ��д��ʵ��ʵ�����͸������Ե����ݿ�����
        /// </summary>
        /// <param name="builder">ʵ�����ʹ�����</param>
        public override void Configure(EntityTypeBuilder<EntityUser> builder)
        {
            builder.HasIndex(m => new { m.EntityId, m.UserId }).HasName("EntityUserIndex");

            builder.HasOne<EntityInfo>(eu => eu.EntityInfo).WithMany().HasForeignKey(m => m.EntityId);
            builder.HasOne<User>(eu => eu.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}