// -----------------------------------------------------------------------
//  <copyright file="FunctionCacheRefreshEventHandler.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.Authorization.Functions;
using Hybrid.EventBuses;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Zero.Authorization.Events
{
    /// <summary>
    /// 功能信息缓存刷新事件处理器
    /// </summary>
    public class FunctionCacheRefreshEventHandler : EventHandlerBase<FunctionCacheRefreshEventData>
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="FunctionCacheRefreshEventHandler"/>类型的新实例
        /// </summary>
        public FunctionCacheRefreshEventHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(FunctionCacheRefreshEventData eventData)
        {
            if (!_provider.InHttpRequest())
            {
                return;
            }
            IFunctionHandler functionHandler = _provider.GetService<IFunctionHandler>();
            functionHandler.RefreshCache();
        }
    }
}