// -----------------------------------------------------------------------
//  <copyright file="LoggerExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-02 2:40</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using System;

namespace Hybrid.Logging
{
    /// <summary>
    /// <see cref="ILogger"/>扩展方法
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 记录异常日志信息，并返回异常以便抛出
        /// </summary>
        public static Exception LogExceptionMessage(this ILogger logger, Exception exception, string message = null)
        {
            logger.LogError(exception, message ?? exception.Message);
            return exception;
        }
    }
}