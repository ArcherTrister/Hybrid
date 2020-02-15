using Hybrid.EventBuses;
using Hybrid.Web.Identity.Entities;

using Microsoft.AspNetCore.Http;

namespace IdentityServer4.Web.Identity.Events
{
    /// <summary>
    /// 注册事件数据
    /// </summary>
    public class RegisterEventData : EventDataBase
    {
        /// <summary>
        /// 注册用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string RequestScheme { get; set; }

        /// <summary>
        ///
        /// </summary>
        public HostString RequestHost { get; set; }
    }
}