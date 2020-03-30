﻿// -----------------------------------------------------------------------
//  <copyright file="ExceptionExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2014-07-18 17:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Hybrid.Exceptions
{
    /// <summary>
    /// 异常操作扩展
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 格式化异常消息
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="isHideStackTrace">是否隐藏异常规模信息</param>
        /// <returns>格式化后的异常信息字符串</returns>
        public static string FormatMessage(this Exception e, bool isHideStackTrace = false)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string appString = string.Empty;
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "  ";
                }
                sb.AppendLine(string.Format("{0}异常消息：{1}", appString, e.Message));
                sb.AppendLine(string.Format("{0}异常类型：{1}", appString, e.GetType().FullName));
                sb.AppendLine(string.Format("{0}异常方法：{1}", appString, (e.TargetSite == null ? null : e.TargetSite.Name)));
                sb.AppendLine(string.Format("{0}异常源：{1}", appString, e.Source));
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(string.Format("{0}异常堆栈：{1}", appString, e.StackTrace));
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(string.Format("{0}内部异常：", appString));
                    count++;
                }
                e = e.InnerException;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将异常重新抛出
        /// </summary>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}