using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard
{
    public class DashboardMiddeware
    {
        private RequestDelegate _next;
        private DashboardQuartzOptions _options;

        private List<string> OperateUrls = new List<string>
        {
            #region Calendars

            //添加日历
            "api/Quartz/AddCalendar",
            //删除日历
            "api/Quartz/DeleteCalendar",
            //更新日历
            "api/Quartz/UpdateCalendar",

            #endregion Calendars

            #region Jobs

            //暂停任务
            "api/Quartz/PauseJob",
            //暂停任务
            "api/Quartz/PauseJobs",
            //恢复任务
            "api/Quartz/ResumeJob",
            //恢复任务
            "api/Quartz/ResumeJobs",
            //触发任务
            "api/Quartz/TriggerJob",
            //删除任务
            "api/Quartz/DeleteJob",
            //中断任务
            "api/Quartz/InterruptJob",
            //添加任务
            "api/Quartz/AddJob",
            //触发任务
            "api/Quartz/TriggerJob",

            #endregion Jobs

            #region Schedulers

            //启动调度程序
            "api/Quartz/Start",
            //准备调度程序
            "api/Quartz/Standby",
            //关闭调度程序
            "api/Quartz/Shutdown",
            //清除调度程序
            "api/Quartz/Clear",

            #endregion Schedulers



            #region Triggers

            //暂停触发器
            "api/Quartz/PauseTrigger",
            //暂停触发器
            "api/Quartz/PauseTriggers",
            //恢复触发器
            "api/Quartz/ResumeTrigger",
            //恢复触发器
            "api/Quartz/ResumeTriggers"

            #endregion Triggers
    };

        public DashboardMiddeware(RequestDelegate next, DashboardQuartzOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            string routeUrl = context.Request.Path;
            await _next.Invoke(context);
            if (routeUrl.StartsWith(_options.PathMatch, StringComparison.OrdinalIgnoreCase) && !routeUrl.StartsWith(_options.LoginPath, StringComparison.OrdinalIgnoreCase))
            {
                if (_options.AuthType.Equals(AuthTypes.Operate))
                {
                    if (OperateUrls.Where(p => p.Equals(routeUrl, StringComparison.OrdinalIgnoreCase)).Any())
                    {
                        //Quartz Dashboard授权验证
                        if (_options.Authorizations.Any(auth => !auth.Authorize(context, _options)))
                        {
                            context.Response.Redirect(_options.LoginPath);

                            // todo: 方案二 返回401
                            //bool? isAuthenticated = context.User?.Identity?.IsAuthenticated;
                            //context.Response.StatusCode = isAuthenticated == true
                            //    ? (int)HttpStatusCode.Forbidden
                            //    : (int)HttpStatusCode.Unauthorized;
                        }
                    }
                }
                if (_options.AuthType.Equals(AuthTypes.All))
                {
                    //Quartz Dashboard授权验证
                    if (_options.Authorizations.Any(auth => !auth.Authorize(context, _options)))
                    {
                        context.Response.Redirect(_options.LoginPath);

                        // todo: 方案二 返回401
                        //bool? isAuthenticated = context.User?.Identity?.IsAuthenticated;
                        //context.Response.StatusCode = isAuthenticated == true
                        //    ? (int)HttpStatusCode.Forbidden
                        //    : (int)HttpStatusCode.Unauthorized;
                    }
                }
            }
        }
    }
}