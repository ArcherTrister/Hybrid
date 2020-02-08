// -----------------------------------------------------------------------
//  <copyright file="OperationResultExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Data;
using Hybrid.Extensions;

using System;

namespace Hybrid.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="OperationResult"/>扩展方法
    /// </summary>
    public static class OperationResultExtensions
    {
        /// <summary>
        /// 将业务操作结果转ajax操作结果，并处理强类型的 <see cref="OperationResult{TResultType}"/>
        /// </summary>
        public static AjaxResult ToAjaxResult<T>(this OperationResult<T> result, Func<T, object> dataFunc = null)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            bool type = result.ResultType.IsSuccess();
            object data = dataFunc == null ? result.Data : dataFunc(result.Data);
            return new AjaxResult(content, type, data);
        }

        /// <summary>
        /// 将业务操作结果转ajax操作结果，可确定是否包含Data
        /// </summary>
        /// <param name="result">业务操作结果</param>
        /// <param name="containsData">是否包含Data，默认不包含</param>
        /// <returns></returns>
        public static AjaxResult ToAjaxResult(this OperationResult result, bool containsData = false)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            bool type = result.ResultType.IsSuccess();
            return containsData ? new AjaxResult(content, type, result.Data) : new AjaxResult(type, content);
        }

        /// <summary>
        /// 将业务操作结果转ajax操作结果，会将 object 类型的 <see cref="OperationResult{TResultType}"/> 转换为强类型 T，再通过 dataFunc 进行进一步处理
        /// </summary>
        public static AjaxResult ToAjaxResult<T>(this OperationResult result, Func<T, object> dataFunc)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            bool type = result.ResultType.IsSuccess();
            object data = null;
            if (result.Data != null)
            {
                if (dataFunc != null && result.Data is T resultData)
                {
                    data = dataFunc(resultData);
                }
            }
            return new AjaxResult(content, type, data);
        }

        /// <summary>
        /// 判断业务结果类型是否是Success结果
        /// </summary>
        public static bool IsSuccess(this OperationResultType resultType)
        {
            if (resultType.Equals(OperationResultType.Success))
            {
                return true;
            }
            return false;
        }
    }
}