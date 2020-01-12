﻿// -----------------------------------------------------------------------
//  <copyright file="DataAuthCache.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-07-04 18:25</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.EntityInfos;
using ESoftor.Permission.Security;
using ESoftor.Web.Identity.Entity;
using ESoftor.Web.Security.Entities;

using System;

namespace ESoftor.Web.Security
{
    /// <summary>
    /// 数据权限缓存
    /// </summary>
    public class DataAuthCache : DataAuthCacheBase<EntityRole, Role, EntityInfo, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="DataAuthCacheBase{TEntityRole, TRole, TEntityInfo, TRoleKey}"/>类型的新实例
        /// </summary>
        public DataAuthCache(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }
    }
}