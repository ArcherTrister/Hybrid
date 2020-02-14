using Hybrid.EventBuses;

namespace Hybrid.Zero.Identity.Events
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
}
