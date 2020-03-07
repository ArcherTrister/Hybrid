// -----------------------------------------------------------------------
//  <copyright file="DataAuthCacheRefreshEventData.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.EventBuses;

using System.Collections.Generic;

namespace Hybrid.Zero.Security.Events
{
    /// <summary>
    /// 数据权限缓存刷新事件数据
    /// </summary>
    public class DataAuthCacheRefreshEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 要更新的数据权限缓存项集合
        /// </summary>
        public IList<DataAuthCacheItem> SetItems { get; } = new List<DataAuthCacheItem>();

        /// <summary>
        /// 获取或设置 要移除的数据权限缓存项信息
        /// </summary>
        public IList<DataAuthCacheItem> RemoveItems { get; } = new List<DataAuthCacheItem>();

        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasData()
        {
            return SetItems.Count > 0 || RemoveItems.Count > 0;
        }
    }
}