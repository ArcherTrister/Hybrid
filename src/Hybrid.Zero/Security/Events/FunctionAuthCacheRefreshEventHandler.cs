// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthCacheRefreshEventHandler.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.EventBuses;
using Hybrid.Security;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Zero.Security.Events
{
    /// <summary>
    /// 功能权限缓存刷新事件处理器
    /// </summary>
    public class FunctionAuthCacheRefreshEventHandler : EventHandlerBase<FunctionAuthCacheRefreshEventData>
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="FunctionAuthCacheRefreshEventHandler"/>类型的新实例
        /// </summary>
        public FunctionAuthCacheRefreshEventHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(FunctionAuthCacheRefreshEventData eventData)
        {
            if (!_provider.InHttpRequest())
            {
                return;
            }
            IFunctionAuthCache cache = _provider.GetService<IFunctionAuthCache>();
            if (eventData.FunctionIds.Length > 0)
            {
                cache.RemoveFunctionCaches(eventData.FunctionIds);
                foreach (Guid functionId in eventData.FunctionIds)
                {
                    cache.GetFunctionRoles(functionId);
                }
            }
            if (eventData.UserNames.Length > 0)
            {
                cache.RemoveUserCaches(eventData.UserNames);
                foreach (string userName in eventData.UserNames)
                {
                    cache.GetUserFunctions(userName);
                }
            }
        }
    }
}