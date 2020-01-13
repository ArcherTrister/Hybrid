// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthCacheRefreshEventData.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.EventBuses;

using System;

namespace ESoftor.Zero.Security.Events
{
    /// <summary>
    /// 功能权限缓存刷新事件源
    /// </summary>
    public class FunctionAuthCacheRefreshEventData : EventDataBase
    {
        /// <summary>
        /// 初始化一个<see cref="FunctionAuthCacheRefreshEventData"/>类型的新实例
        /// </summary>
        public FunctionAuthCacheRefreshEventData()
        {
            FunctionIds = new Guid[0];
            UserNames = new string[0];
        }

        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        public Guid[] FunctionIds { get; set; }

        /// <summary>
        /// 获取或设置 用户名集合
        /// </summary>
        public string[] UserNames { get; set; }
    }
}