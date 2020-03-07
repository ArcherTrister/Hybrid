﻿// -----------------------------------------------------------------------
//  <copyright file="AuthorizationResult.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-10 19:45</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Extensions;

using System.Diagnostics;

namespace Hybrid.Authorization
{
    /// <summary>
    /// 权限检查结果
    /// </summary>
    [DebuggerDisplay("{ResultType}-{Message}")]
    public sealed class AuthorizationResult : HybridResult<AuthorizationStatus>
    {
        // TODO: 400 405 406

        #region NoApi

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status)
            : this(status, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status, string message)
            : this(status, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status, string message, object data)
            : base(status, message, data)
        { }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public override string Message
        {
            get { return _message ?? ResultType.ToDescription(); }
            set { _message = value; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsOk
        {
            get { return ResultType == AuthorizationStatus.OK; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsUnauthorized
        {
            get { return ResultType == AuthorizationStatus.Unauthorized; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsForbidden
        {
            get { return ResultType == AuthorizationStatus.Forbidden; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsNoFound
        {
            get { return ResultType == AuthorizationStatus.NoFound; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsLocked
        {
            get { return ResultType == AuthorizationStatus.Locked; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsError
        {
            get { return ResultType == AuthorizationStatus.Error; }
        }

        /// <summary>
        /// 获取 检查成功的结果
        /// </summary>
        public static AuthorizationResult OK { get; } = new AuthorizationResult(AuthorizationStatus.OK);

        #endregion NoApi

        #region Api

        //    /// <summary>
        //    /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        //    /// </summary>
        //    public AuthorizationResult(AuthorizationStatus status, bool isApi)
        //        : this(status, isApi, null)
        //    { }

        //    /// <summary>
        //    /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        //    /// </summary>
        //    public AuthorizationResult(AuthorizationStatus status, bool isApi, string message)
        //        : this(status, isApi, message, null)
        //    { }

        //    /// <summary>
        //    /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        //    /// </summary>
        //    public AuthorizationResult(AuthorizationStatus status, bool isApi, string message, object data)
        //        : base(status, message, data)
        //    {
        //        IsApi = isApi;
        //    }

        //    /// <summary>
        //    /// 获取或设置 是否Api
        //    /// </summary>
        //    public bool IsApi { get; set; } = false;

        //    /// <summary>
        //    /// 获取或设置 返回消息
        //    /// </summary>
        //    public override string Message
        //    {
        //        get { return _message ?? ResultType.ToDescription(); }
        //        set { _message = value; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsOk
        //    {
        //        get { return ResultType == AuthorizationStatus.OK; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsUnauthorized
        //    {
        //        get { return ResultType == AuthorizationStatus.Unauthorized; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsForbidden
        //    {
        //        get { return ResultType == AuthorizationStatus.Forbidden; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsNoFound
        //    {
        //        get { return ResultType == AuthorizationStatus.NoFound; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsLocked
        //    {
        //        get { return ResultType == AuthorizationStatus.Locked; }
        //    }

        //    /// <summary>
        //    /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        //    /// </summary>
        //    public bool IsError
        //    {
        //        get { return ResultType == AuthorizationStatus.Error; }
        //    }

        #endregion Api
    }
}