// -----------------------------------------------------------------------
//  <copyright file="AuditOperationEntry.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using System;
using System.Collections.Generic;

namespace Hybrid.Audits
{
    /// <summary>
    /// 审计操作信息
    /// </summary>
    public class AuditOperationEntry
    {
        /// <summary>
        /// 初始化一个<see cref="AuditOperationEntry"/>类型的新实例
        /// </summary>
        public AuditOperationEntry()
        {
            EntityEntries = new List<AuditEntityEntry>();
        }

        /// <summary>
        /// 获取或设置 执行的功能名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 获取或设置 当前用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置 当前用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 当前用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 当前访问IP
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 获取或设置 当前访问UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Optional custom data that can be filled and used.
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 获取或设置 操作结果类型
        /// </summary>
        public AjaxResultType ResultType { get; set; } = AjaxResultType.Success;

        /// <summary>
        /// 获取或设置 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置 信息添加时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 结束时间
        /// </summary>
        public DateTime EndedTime { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Return values.
        /// </summary>
        public string ReturnValue { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 获取或设置 执行耗时，单位毫秒
        /// </summary>
        public int Elapsed { get; set; }

        /// <summary>
        /// Exception object, if an exception occurred during execution of the method.
        /// </summary>
        public Exception Exception { get; set; }

        //public override string ToString()
        //{
        //    var loggedUserId = string.IsNullOrEmpty(UserId)
        //                           ? "an anonymous user"
        //                           : "user " + UserId;

        //    var exceptionOrSuccessMessage = Exception != null
        //        ? "exception: " + Exception.Message
        //        : "succeed";

        //    return $"AUDIT LOG: {ServiceName}.{MethodName} is executed by {loggedUserId} in {ExecutionDuration} ms from {ClientIpAddress} IP address with {exceptionOrSuccessMessage}.";
        //}

        /// <summary>
        /// 获取或设置 审计数据信息集合
        /// </summary>
        public ICollection<AuditEntityEntry> EntityEntries { get; set; }
    }
}