using Hybrid.EventBuses;
using Hybrid.RealTime;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Zero.Identity.Events
{
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