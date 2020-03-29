// -----------------------------------------------------------------------
//  <copyright file="HybridResult.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2015 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2015-08-03 18:29</last-date>
// -----------------------------------------------------------------------

using System;

using Hybrid.Extensions;


namespace Hybrid.Data
{
    /// <summary>
    /// Hybrid结果基类
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public abstract class HybridResult<TResultType> : HybridResult<TResultType, object>, IHybridResult<TResultType>
    {
        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType}"/>类型的新实例
        /// </summary>
        protected HybridResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type)
            : this(type, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type, string message, object data)
            : base(type, message, data)
        { }
    }


    /// <summary>
    /// Hybrid结果基类
    /// </summary>
    /// <typeparam name="TResultType">结果类型</typeparam>
    /// <typeparam name="TData">结果数据类型</typeparam>
    public abstract class HybridResult<TResultType, TData> : IHybridResult<TResultType, TData>
    {
        /// <summary>
        /// 内部消息
        /// </summary>
        protected string _message;

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected HybridResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type)
            : this(type, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type, string message)
            : this(type, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="HybridResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected HybridResult(TResultType type, string message, TData data)
        {
            if (message == null && typeof(TResultType).IsEnum)
            {
                message = (type as Enum)?.ToDescription();
            }
            ResultType = type;
            _message = message;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        public TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public virtual string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        public TData Data { get; set; }
    }
}