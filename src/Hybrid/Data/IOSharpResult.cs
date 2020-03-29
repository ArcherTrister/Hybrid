// -----------------------------------------------------------------------
//  <copyright file="IHybridResult.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2015 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2015-08-03 18:20</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Data
{
    /// <summary>
    /// Hybrid操作结果
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public interface IHybridResult<TResultType> : IHybridResult<TResultType, object>
    { }


    /// <summary>
    /// Hybrid操作结果
    /// </summary>
    public interface IHybridResult<TResultType, TData>
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