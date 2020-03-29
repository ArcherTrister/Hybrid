// -----------------------------------------------------------------------
//  <copyright file="PageData.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-10-25 0:07</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;


namespace Hybrid.Filter
{
    /// <summary>
    /// 页数据，封装分页的行数据与总记录数
    /// </summary>
    /// <typeparam name="T">项数据类型</typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 初始化一个<see cref="PageData{T}"/>类型的新实例
        /// </summary>
        public PageData()
            : this(new List<T>(), 0)
        { }

        /// <summary>
        /// 初始化一个<see cref="PageData{T}"/>类型的新实例
        /// </summary>
        public PageData(IEnumerable<T>rows, int total)
        {
            Rows = rows;
            Total = total;
        }

        /// <summary>
        /// 获取或设置 行数据
        /// </summary>
        public IEnumerable<T> Rows { get; set; }

        /// <summary>
        /// 获取或设置 数据行数
        /// </summary>
        public int Total { get; set; }
    }
}