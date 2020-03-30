﻿// -----------------------------------------------------------------------
//  <copyright file="OnlineUserCacheRemoveEventHandler.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-09 16:07</last-date>
// -----------------------------------------------------------------------

using Hybrid.EventBuses;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Identity.Events
{
    /// <summary>
    /// 在线用户信息缓存移除事件数据
    /// </summary>
    public class OnlineUserCacheRemoveEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string[] UserNames { get; set; } = new string[0];
    }

    /// <summary>
    /// 在线用户信息缓存移除事件处理器
    /// </summary>
    public class OnlineUserCacheRemoveEventHandler : EventHandlerBase<OnlineUserCacheRemoveEventData>
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="OnlineUserCacheRemoveEventHandler"/>类型的新实例
        /// </summary>
        public OnlineUserCacheRemoveEventHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(OnlineUserCacheRemoveEventData eventData)
        {
            IOnlineUserProvider onlineUserProvider = _provider.GetService<IOnlineUserProvider>();
            onlineUserProvider.Remove(eventData.UserNames);
        }
    }
}