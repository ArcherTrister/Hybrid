﻿// -----------------------------------------------------------------------
//  <copyright file="AjaxResultType.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:36</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Data
{
    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum AjaxResultType
    {
        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success = 200,

        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info = 203,

        /// <summary>
        /// 错误请求
        /// </summary>
        RequestError = 400,

        /// <summary>
        /// 用户未登录
        /// </summary>
        UnAuth = 401,

        /// <summary>
        /// 已登录，但权限不足
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 资源未找到
        /// </summary>
        NoFound = 404,

        /// <summary>
        /// 方法禁用
        /// </summary>
        MethodDisabled = 405,

        /// <summary>
        /// 不支持
        /// </summary>
        NoSupport = 406,

        /// <summary>
        /// 资源被锁定
        /// </summary>
        Locked = 423,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error = 500,
    }
}