using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard
{
    public class DashboardQuartzOptions
    {
        /// <summary>
        /// The path for the Back To Site link. Set to <see langword="null" /> in order to hide the Back To Site link.
        /// </summary>
        public string RouteName { get; set; } = "QuartzAreaRoute";

        //public Func<DashboardContext, bool> IsReadOnlyFunc { get; set; }

        /// <summary>
        /// 返回应用路径地址
        /// </summary>
        public string AppPath { get; set; } = "/";

        /// <summary>
        /// 登录路径地址
        /// </summary>
        public string LoginPath { get; set; } = "/Quartz/Account/Login";

        /// <summary>
        /// Quartz 地址
        /// </summary>
        internal string PathMatch { get; set; } = "/Quartz";

        /// <summary>
        /// Quartz AuthScheme
        /// </summary>
        internal string AuthScheme { get; set; } = "QuartzCookie";

        /// <summary>
        /// 错误跳转地址
        /// </summary>
        internal string ErrorPath { get; set; } = "/Quartz/Home/Error";

        /// <summary>
        /// swagger login账号,未指定则不启用
        /// </summary>
        public List<QuartzAuthUser> QuartzAuthUsers { get; set; } = new List<QuartzAuthUser>();

        /// <summary>
        /// 操作授权校验过滤器
        /// </summary>
        public IEnumerable<IDashboardAuthorizationFilter> Authorizations { get; set; } = new[] { new LoginAuthorizationFilter() }; // LoginAuthorizationFilter DefaultAuthorizationFilter

        /// <summary>
        /// 验证类型
        /// </summary>
        public AuthTypes AuthType { get; set; } = AuthTypes.Operate;
    }
}