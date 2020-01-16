// -----------------------------------------------------------------------
//  <copyright file="IMapper.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-25 1:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hybrid.Mapping
{
    /// <summary>
    /// 定义对象映射功能
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        TTarget MapTo<TTarget>(object source);

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        TTarget MapTo<TSource, TTarget>(TSource source, TTarget target);

        /// <summary>
        /// 将数据源映射为指定输出DTO的集合
        /// </summary>
        /// <typeparam name="TOutputDto">输出DTO类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="membersToExpand">成员展开</param>
        /// <returns>输出DTO的结果集</returns>
        IQueryable<TOutputDto> ToOutput<TOutputDto>(IQueryable source, params Expression<Func<TOutputDto, object>>[] membersToExpand);
    }
}