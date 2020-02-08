// -----------------------------------------------------------------------
//  <copyright file="AjaxResponse.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace
namespace Hybrid.AspNetCore.Mvc.Models
{
    /// <summary>
    /// This class is used to create standard responses for AJAX requests.
    /// </summary>
    [Serializable]
    public class AjaxResponse<TResult> : AjaxResponseBase
    {
        /// <summary>
        /// The actual result object of AJAX request.
        /// It is set if <see cref="AjaxResponseBase.Success"/> is true.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// 获取或设置 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        public AjaxResponse()
        {
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public AjaxResponse(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        /// <param name="result">The actual result object of AJAX request</param>
        public AjaxResponse(bool success, TResult result)
        {
            Result = result;
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        /// <param name="content">消息内容</param>
        public AjaxResponse(bool success, string content)
        {
            Content = content;
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        /// <param name="content">消息内容</param>
        public AjaxResponse(TResult result, string content)
        {
            Result = result;
            Content = content;
            Success = true;
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResponse"/>类型的新实例
        /// </summary>
        public AjaxResponse(string content, bool success, TResult result)
        {
            Result = result;
            Content = content;
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(TResult result, ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Result = result;
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }

    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// </summary>
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        public AjaxResponse()
            : base()
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public AjaxResponse(bool success)
            : base(success)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object</param>
        public AjaxResponse(object result)
            : base(result)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        /// <param name="result">The actual result object</param>
        public AjaxResponse(bool success, object result)
            : base(success, result)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        /// <param name="result">消息内容</param>
        public AjaxResponse(bool success, string content)
            : base(success, content)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="result">The actual result object</param>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(object result, ErrorInfo error, bool unAuthorizedRequest = false)
            : base(result, error, unAuthorizedRequest)
        {
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        /// <param name="content">消息内容</param>
        public AjaxResponse(object result, string content)
            : base(result, content)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResponse"/>类型的新实例
        /// </summary>
        public AjaxResponse(string content, bool success, object result)
            : base(content, success, result)
        {
        }
    }
}