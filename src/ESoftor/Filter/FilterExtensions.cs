// -----------------------------------------------------------------------
//  <copyright file="FilterExtensions.cs" company="com.esoftor">
//      Copyright ? 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-10 14:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace ESoftor.Filter
{
    /// <summary>
    /// ���ݹ�����չ����
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// ��������ת��Ϊ��ѯ���ʽ
        /// </summary>
        [Obsolete("ʹ�� IFilterService ������棬���ཫ��1.0�汾���Ƴ�")]
        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(this FilterGroup group)
        {
            return FilterHelper.GetExpression<TEntity>(group);
        }

        /// <summary>
        /// ������ת��Ϊ��ѯ���ʽ
        /// </summary>
        [Obsolete("ʹ�� IFilterService ������棬���ཫ��1.0�汾���Ƴ�")]
        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(this FilterRule rule)
        {
            return FilterHelper.GetExpression<TEntity>(rule);
        }
    }
}