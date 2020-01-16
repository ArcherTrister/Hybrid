// -----------------------------------------------------------------------
//  <copyright file="LogMessageEntry.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 21:10</last-date>
// -----------------------------------------------------------------------

using System;

//power by https://github.com/andrewlock/NetEscapades.Extensions.Logging
namespace Hybrid.Logging.RollingFile.Internal
{
    /// <summary>
    /// 日志消息项
    /// </summary>
    public class LogMessageEntry
    {
        /// <summary>
        /// 获取或设置 时间戳
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// 获取或设置 消息
        /// </summary>
        public string Message { get; set; }
    }
}