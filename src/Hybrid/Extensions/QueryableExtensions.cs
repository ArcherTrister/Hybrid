﻿// -----------------------------------------------------------------------
//  <copyright file="QueryableExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.Domain.Entities;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Filter;
using Hybrid.Mapping;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Hybrid.Extensions
{
    /// <summary>
    /// IQueryable集合扩展方法
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段与排序方式进行排序
        /// </summary>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <typeparam name="T">动态类型</typeparam>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition<T> sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");
            return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            source.CheckNotNull("source");
            propertyName.CheckNotNullOrEmpty("propertyName");

            return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表字段排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, SortCondition sortCondition)
        {
            source.CheckNotNull("source");
            sortCondition.CheckNotNull("sortCondition");

            return source.ThenBy(sortCondition.SortField, sortCondition.ListSortDirection);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");
            selector.CheckNotNull("selector");

            return source.ToPage(predicate, pageCondition.PageIndex, pageCondition.PageSize, pageCondition.SortConditions, selector);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">分页数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            SortCondition[] sortConditions,
            Expression<Func<TEntity, TResult>> selector)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);
            selector.CheckNotNull("selector");

            TResult[] data = source.Where(predicate, pageIndex, pageSize, out int total, sortConditions).Select(selector).ToArray();
            return new PageResult<TResult>() { Total = total, Data = data };
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定输出DTO的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TOutputDto">输出DTO数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TOutputDto> ToPage<TEntity, TOutputDto>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition)
            where TOutputDto : IOutputDto
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");
            return source.ToPage<TEntity, TOutputDto>(predicate,
                pageCondition.PageIndex,
                pageCondition.PageSize,
                pageCondition.SortConditions);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询出指定输出DTO的分页信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TOutputDto">输出DTO数据类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns>分页结果信息</returns>
        public static PageResult<TOutputDto> ToPage<TEntity, TOutputDto>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            SortCondition[] sortConditions)
            where TOutputDto : IOutputDto
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);

            TOutputDto[] data = source.Where(predicate, pageIndex, pageSize, out int total, sortConditions).ToOutput<TEntity, TOutputDto>().ToArray();
            return new PageResult<TOutputDto>() { Total = total, Data = data };
        }

        /// <summary>
        /// 将数据源映射为指定<typeparamref name="TOutputDto"/>的集合，
        /// 并验证数据的<see cref="DataAuthOperation.Update"/>,<see cref="DataAuthOperation.Delete"/>数据权限状态
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="getKey">是否应用于获取缓存键时</param>
        public static IQueryable<TOutputDto> ToOutput<TEntity, TOutputDto>(this IQueryable<TEntity> source, bool getKey = false)
        {
            if (!typeof(TOutputDto).IsBaseOn<IDataAuthEnabled>() || getKey)
            {
                return MapperExtensions.ToOutput<TEntity, TOutputDto>(source);
            }

            List<TEntity> entities = source.ToList();
            List<TOutputDto> dtos = new List<TOutputDto>();
            Func<TEntity, bool> updateFunc = FilterHelper.GetDataFilterExpression<TEntity>(null, DataAuthOperation.Update).Compile();
            Func<TEntity, bool> deleteFunc = FilterHelper.GetDataFilterExpression<TEntity>(null, DataAuthOperation.Delete).Compile();
            foreach (TEntity entity in entities)
            {
                TOutputDto dto = entity.MapTo<TOutputDto>();
                IDataAuthEnabled dto2 = (IDataAuthEnabled)dto;
                dto2.Updatable = updateFunc(entity);
                dto2.Deletable = deleteFunc(entity);
                dtos.Add(dto);
            }
            return dtos.AsQueryable();
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            out int total)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");

            return source.Where(predicate, pageCondition.PageIndex, pageCondition.PageSize, out total, pageCondition.SortConditions);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            out int total,
            SortCondition[] sortConditions = null)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);

            total = source.Count(predicate);
            source = source.Where(predicate);
            if (sortConditions == null || sortConditions.Length == 0)
            {
                if (typeof(TEntity).IsEntityType())
                {
                    source = source.OrderBy("Id");
                }
                else if (typeof(TEntity).IsBaseOn<ICreatedTime>())
                {
                    source = source.OrderBy("CreatedTime");
                }
                else
                {
                    throw new HybridException($"类型“{typeof(TEntity)}”未添加默认排序方式");
                }
            }
            else
            {
                int count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                foreach (SortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, sortCondition.SortField, sortCondition.ListSortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, sortCondition.SortField, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;
            }
            return source != null
                ? source.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询未过期的子数据集，用于筛选实现了<see cref="IExpirable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Unexpired<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, IExpirable
        {
            DateTime now = DateTime.Now;
            Expression<Func<TEntity, bool>> predicate =
                m => (m.BeginTime == null || m.BeginTime.Value <= now) && (m.EndTime == null || m.EndTime.Value >= now);
            return source.Where(predicate);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>集合中查询未过期的子数据集，用于筛选实现了<see cref="IExpirable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Unexpired<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : IExpirable
        {
            DateTime now = DateTime.Now;
            bool Func(TEntity m) => (m.BeginTime == null || m.BeginTime.Value <= now) && (m.EndTime == null || m.EndTime.Value >= now);
            return source.Where(Func);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中查询已过期的子数据集，用于筛选实现了<see cref="IExpirable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Expired<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, IExpirable
        {
            DateTime now = DateTime.Now;
            Expression<Func<TEntity, bool>> predicate = m => m.EndTime != null && m.EndTime.Value < now;
            return source.Where(predicate);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>集合中查询已过期的子数据集，用于筛选实现了<see cref="IExpirable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Expired<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : IExpirable
        {
            DateTime now = DateTime.Now;
            bool Func(TEntity m) => m.EndTime != null && m.EndTime.Value < now;
            return source.Where(Func);
        }

        /*
        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>数据集中查询未逻辑删除的子数据集，用于筛选实现了<see cref="IRecyclable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Unrecycled<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, IRecyclable
        {
            return source.Where(m => !m.IsDeleted);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>数据集中查询未逻辑删除的子数据集，用于筛选实现了<see cref="IRecyclable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Unrecycled<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : IRecyclable
        {
            return source.Where(m => !m.IsDeleted);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>数据集中查询已逻辑删除的子数据集，用于筛选实现了<see cref="IRecyclable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Recycled<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, IRecyclable
        {
            return source.Where(m => m.IsDeleted);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>数据集中查询已逻辑删除的子数据集，用于筛选实现了<see cref="IRecyclable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Recycled<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : IRecyclable
        {
            return source.Where(m => m.IsDeleted);
        }
        */

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>数据集中查询未锁定的子数据集，用于筛选实现了<see cref="ILockable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Unlocked<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, ILockable
        {
            return source.Where(m => !m.IsLocked);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>数据集中查询未锁定的子数据集，用于筛选实现了<see cref="ILockable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Unlocked<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : ILockable
        {
            return source.Where(m => !m.IsLocked);
        }

        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>数据集中查询已锁定的子数据集，用于筛选实现了<see cref="ILockable"/>接口的数据集
        /// </summary>
        public static IQueryable<TEntity> Locked<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class, ILockable
        {
            return source.Where(m => m.IsLocked);
        }

        /// <summary>
        /// 从指定<see cref="IEnumerable{T}"/>数据集中查询已锁定的子数据集，用于筛选实现了<see cref="ILockable"/>接口的数据集
        /// </summary>
        public static IEnumerable<TEntity> Locked<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : ILockable
        {
            return source.Where(m => m.IsLocked);
        }

        /// <summary>
        /// 多属性排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition">eg:Id asc,Age desc</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> MultiOrderBy<T>(this IQueryable<T> query, string condition)
        {
            string[] conditions = condition.Split(',');

            if (conditions.Length == 0)
            {
                return (IOrderedQueryable<T>)query;
            }

            IOrderedQueryable<T> res = null;

            for (int i = 0; i < conditions.Length; i++)
            {
                string[] strings = conditions[i].Split(" ");
                var fieldName = strings[0];
                var direction = strings[1];

                var param = Expression.Parameter(typeof(T), "p");
                var prop = Expression.Property(param, fieldName);
                var exp = Expression.Lambda(prop, param);

                string method;

                if (i == 0)
                {
                    method = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
                }
                else
                {
                    method = direction.ToLower() == "asc" ? "ThenBy" : "ThenByDescending";
                }
                Type[] types = { query.ElementType, exp.Body.Type };
                var mce = i == 0 ? Expression.Call(typeof(Queryable), method, types, query.Expression, exp) : Expression.Call(typeof(Queryable), method, types, res.Expression, exp);

                if (conditions.Length == 1)
                {
                    return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(mce);
                }

                res = i == 0 ? (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(mce) : (IOrderedQueryable<T>)res.Provider.CreateQuery<T>(mce);
            }
            return res;
        }
    }
}