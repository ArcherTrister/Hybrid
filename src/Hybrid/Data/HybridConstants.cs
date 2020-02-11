// -----------------------------------------------------------------------
//  <copyright file="HybridConstants.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-04-29 3:52</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Data
{
    /// <summary>
    /// 框架中使用到的一些常量
    /// </summary>
    public class HybridConstants
    {
        /// <summary>
        /// Hybrid组件本地化资源名称
        /// </summary>
        public const string LocalizationSourceName = "Hybrid";

        /// <summary>
        /// 当前MVC功能键名
        /// </summary>
        public const string CurrentMvcFunctionKey = "HYBRID_MVC_FUNCTION_CURRENT";

        /// <summary>
        /// 验证码缓存键名前缀
        /// </summary>
        public const string VerifyCodeKeyPrefix = "HYBRID_VERIFY_CODE";

        /// <summary>
        /// Cookie名称
        /// </summary>
        public const string HybridCookieName = "_hybrid";

        /// <summary>
        /// 存放调度名Cookie名
        /// </summary>
        public const string SchedulerCookieName = "_Hybrid.Scheduler";

        /// <summary>
        /// 存放Culture Cookie名
        /// </summary>
        public const string CultureCookieName = "_Hybrid.Culture";

        /// <summary>
        /// 调度名称
        /// </summary>
        public const string DefaultSchedulerName = "HybridScheduler";

        /// <summary>
        /// Quartz表前缀
        /// </summary>
        public const string QuartzDefaultTablePrefix = "QRTZ_";

        /// <summary>
        /// 本地标识服务器访问令牌身份验证的常量
        /// </summary>
        public static class LocalApi
        {
            /// <summary>
            /// 使用AddLocalApi帮助程序时的身份验证方案
            /// </summary>
            public const string AuthenticationScheme = "IdentityServerAccessToken";

            /// <summary>
            /// 使用AddLocalApiAuthentication帮助程序时的API作用域名称
            /// </summary>
            public const string ScopeName = "IdentityServerApi";

            /// <summary>
            /// 使用AddLocalApiAuthentication帮助程序时的授权策略名称
            /// </summary>
            public const string PolicyName = "IdentityServerAccessToken";
        }

        public static class CustomController
        {
            /// <summary>
            /// 验证控制器列表
            /// </summary>
            public static string[] ValidEndings = new[] { "Controller`1", "Controller`2" };
        }
    }
}