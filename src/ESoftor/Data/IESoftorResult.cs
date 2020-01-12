// -----------------------------------------------------------------------
//  <copyright file="IESoftorResult.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2015-08-03 18:20</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Data
{
    /// <summary>
    /// ESoftor操作结果
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public interface IESoftorResult<TResultType> : IESoftorResult<TResultType, object>
    { }

    /// <summary>
    /// ESoftor操作结果
    /// </summary>
    public interface IESoftorResult<TResultType, TData>
    {
        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        TData Data { get; set; }
    }
}