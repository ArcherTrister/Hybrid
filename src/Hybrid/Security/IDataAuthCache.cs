﻿// -----------------------------------------------------------------------
//  <copyright file="IDataAuthCache.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 17:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.Filter;

namespace Hybrid.Security
{
    /// <summary>
    /// 定义数据权限缓存的功能
    /// </summary>
    public interface IDataAuthCache
    {
        /// <summary>
        /// 创建数据权限缓存
        /// </summary>
        void BuildCaches();

        /// <summary>
        /// 设置指定数据权限的缓存
        /// </summary>
        /// <param name="item">要更新的数据权限缓存项</param>
        void SetCache(DataAuthCacheItem item);

        /// <summary>
        /// 移除指定角色名与实体类型的缓存项
        /// </summary>
        /// <param name="item">要移除的数据权限缓存项信息</param>
        void RemoveCache(DataAuthCacheItem item);

        /// <summary>
        /// 获取指定角色名与实体类型的数据权限过滤规则
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="entityTypeFullName">实体类型名称</param>
        /// <returns>数据过滤条件组</returns>
        /// <param name="operation">数据权限操作</param>
        FilterGroup GetFilterGroup(string roleName, string entityTypeFullName, DataAuthOperation operation);
    }
}