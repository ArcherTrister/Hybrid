// -----------------------------------------------------------------------
//  <copyright file="DataAuthCacheRefreshEventHandler.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.EventBuses;
using Hybrid.Security;

namespace Hybrid.Zero.Security.Events
{
    /// <summary>
    /// 数据权限缓存刷新事件处理器
    /// </summary>
    public class DataAuthCacheRefreshEventHandler : EventHandlerBase<DataAuthCacheRefreshEventData>
    {
        private readonly IDataAuthCache _authCache;

        /// <summary>
        /// 初始化一个<see cref="DataAuthCacheRefreshEventHandler"/>类型的新实例
        /// </summary>
        public DataAuthCacheRefreshEventHandler(IDataAuthCache authCache)
        {
            _authCache = authCache;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(DataAuthCacheRefreshEventData eventData)
        {
            //更新缓存项
            foreach (DataAuthCacheItem cacheItem in eventData.SetItems)
            {
                _authCache.SetCache(cacheItem);
            }
            //移除缓存项
            foreach (DataAuthCacheItem cacheItem in eventData.RemoveItems)
            {
                _authCache.RemoveCache(cacheItem);
            }
        }
    }
}