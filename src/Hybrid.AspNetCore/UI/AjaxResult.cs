// -----------------------------------------------------------------------
//  <copyright file="AjaxResult.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:38</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using System;

namespace Hybrid.AspNetCore.UI
{
    ///// <summary>
    ///// 表示Ajax操作结果
    ///// </summary>
    //public class AjaxResult
    //{
    //    ///// <summary>
    //    ///// 初始化一个<see cref="AjaxResult"/>类型的新实例
    //    ///// </summary>
    //    //public AjaxResult()
    //    //    : this(null)
    //    //{ }

    //    /// <summary>
    //    /// 初始化一个<see cref="AjaxResult"/>类型的新实例
    //    /// </summary>
    //    public AjaxResult(string content, AjaxResultType type = AjaxResultType.Success, object data = null)
    //        : this(content, data, type)
    //    { }

    //    /// <summary>
    //    /// 初始化一个<see cref="AjaxResult"/>类型的新实例
    //    /// </summary>
    //    public AjaxResult(string content, object data, AjaxResultType type = AjaxResultType.Success)
    //    {
    //        Type = type;
    //        Content = content;
    //        Data = data;
    //    }

    //    /// <summary>
    //    /// 获取或设置 Ajax操作结果类型
    //    /// </summary>
    //    public AjaxResultType Type { get; set; }

    //    /// <summary>
    //    /// 获取或设置 消息内容
    //    /// </summary>
    //    public string Content { get; set; }

    //    /// <summary>
    //    /// 获取或设置 返回数据
    //    /// </summary>
    //    public object Data { get; set; }

    //    /// <summary>
    //    /// 是否成功
    //    /// </summary>
    //    public bool Succeeded()
    //    {
    //        return Type == AjaxResultType.Success;
    //    }

    //    /// <summary>
    //    /// 是否错误
    //    /// </summary>
    //    public bool Error()
    //    {
    //        return Type == AjaxResultType.Error;
    //    }

    //    /// <summary>
    //    /// 成功的AjaxResult
    //    /// </summary>
    //    public static AjaxResult Success(object data = null)
    //    {
    //        return new AjaxResult("操作执行成功", AjaxResultType.Success, data);
    //    }
    //}

    /// <summary>
    /// 表示Ajax操作结果
    /// </summary>
    [Serializable]
    public class AjaxResult<TResult> : AjaxResponseBase
    {
        /// <summary>
        /// 获取或设置 Ajax操作结果类型
        /// </summary>
        public AjaxResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置 返回数据
        /// </summary>
        public TResult Result { get; set; }

        ///// <summary>
        ///// 是否成功
        ///// </summary>
        //public bool Succeeded()
        //{
        //    return Type == AjaxResultType.Success;
        //}

        ///// <summary>
        ///// 是否错误
        ///// </summary>
        //public bool Error()
        //{
        //    return Type == AjaxResultType.Error;
        //}

        ///// <summary>
        ///// 成功的AjaxResult
        ///// </summary>
        //public static AjaxResult Success(object data = null)
        //{
        //    return new AjaxResult("操作执行成功", AjaxResultType.Success, data);
        //}

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult()
        {
            Success = true;
            ResultType = AjaxResultType.Success;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content)
        {
            Success = true;
            ResultType = AjaxResultType.Success;
            Content = content;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType ajaxResultType, bool unAuthorizedRequest = false)
        {
            ResultType = ajaxResultType;
            Success = ResultType == AjaxResultType.Success;
            Content = content;
            UnAuthorizedRequest = unAuthorizedRequest;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType ajaxResultType, TResult data)
        {
            ResultType = ajaxResultType;
            Success = ResultType == AjaxResultType.Success;
            Content = content;
            Result = data;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, TResult data, AjaxResultType ajaxResultType = AjaxResultType.Success, bool unAuthorizedRequest = false)
        {
            ResultType = ajaxResultType;
            Success = ResultType == AjaxResultType.Success;
            Content = content;
            Result = data;
            UnAuthorizedRequest = unAuthorizedRequest;
        }
    }

    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// </summary>
    [Serializable]
    public class AjaxResult : AjaxResult<object>
    {
        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult()
        {
            Success = true;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content)
             : base(content)
        {
            Success = true;
            ResultType = AjaxResultType.Success;
            Content = content;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType ajaxResultType, bool unAuthorizedRequest = false)
            : base(content, ajaxResultType, unAuthorizedRequest)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType ajaxResultType, object data)
            : base(content, ajaxResultType, data)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult{TResult}"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, object data = null, AjaxResultType ajaxResultType = AjaxResultType.Success, bool unAuthorizedRequest = false)
            : base(content, ajaxResultType, unAuthorizedRequest)
        {
        }

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object.
        ///// <see cref="AjaxResponseBase.Success"/> is set as true.
        ///// </summary>
        //public AjaxResponse()
        //    : base()
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        ///// </summary>
        ///// <param name="success">Indicates success status of the result</param>
        //public AjaxResponse(bool success)
        //    : base(success)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as true.
        ///// </summary>
        ///// <param name="result">The actual result object</param>
        //public AjaxResponse(object result)
        //    : base(result)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as false.
        ///// </summary>
        ///// <param name="error">Error details</param>
        ///// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        //public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        //    : base(error, unAuthorizedRequest)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as true.
        ///// </summary>
        ///// <param name="success">Indicates success status of the result</param>
        ///// <param name="result">The actual result object</param>
        //public AjaxResponse(bool success, object result)
        //    : base(success, result)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as true.
        ///// </summary>
        ///// <param name="success">Indicates success status of the result</param>
        ///// <param name="result">消息内容</param>
        //public AjaxResponse(bool success, string content)
        //    : base(success, content)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as false.
        ///// </summary>
        ///// <param name="result">The actual result object</param>
        ///// <param name="error">Error details</param>
        ///// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        //public AjaxResponse(object result, ErrorInfo error, bool unAuthorizedRequest = false)
        //    : base(result, error, unAuthorizedRequest)
        //{
        //}

        ///// <summary>
        ///// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        ///// <see cref="AjaxResponseBase.Success"/> is set as true.
        ///// </summary>
        ///// <param name="result">The actual result object of AJAX request</param>
        ///// <param name="content">消息内容</param>
        //public AjaxResponse(object result, string content)
        //    : base(result, content)
        //{
        //}

        ///// <summary>
        ///// 初始化一个<see cref="AjaxResponse"/>类型的新实例
        ///// </summary>
        //public AjaxResponse(string content, bool success, object result)
        //    : base(content, success, result)
        //{
        //}
    }
}