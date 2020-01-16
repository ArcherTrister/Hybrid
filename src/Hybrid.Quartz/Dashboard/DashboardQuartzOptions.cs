using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard
{
    public class DashboardQuartzOptions
    {
        /// <summary>
        /// 路由名称. Set to <see cref="RouteName" /> in the options.
        /// </summary>
        public string RouteName { get; set; } = "HybridQuartzAreaRoute";

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
        internal string AuthScheme { get; set; } = "HybridQuartzCookie";

        ///// <summary>
        ///// 错误跳转地址
        ///// </summary>
        //internal string ErrorPath { get; set; } = "/Quartz/Home/Error";

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