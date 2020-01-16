// -----------------------------------------------------------------------
//  <copyright file="FinderBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-17 2:47</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Finders
{
    /// <summary>
    /// 查找器基类
    /// </summary>
    public abstract class FinderBase<TItem> : IFinder<TItem>
    {
        private readonly object _lockObj = new object();

        /// <summary>
        /// 项缓存
        /// </summary>
        protected readonly List<TItem> ItemsCache = new List<TItem>();

        /// <summary>
        /// 是否已查找过
        /// </summary>
        protected bool Found = false;

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="fromCache">是否来自缓存</param>
        /// <returns></returns>
        public virtual TItem[] Find(Func<TItem, bool> predicate, bool fromCache = false)
        {
            return FindAll(fromCache).Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <param name="fromCache">是否来自缓存</param>
        /// <returns></returns>
        public virtual TItem[] FindAll(bool fromCache = false)
        {
            lock (_lockObj)
            {
                if (fromCache && Found)
                {
                    return ItemsCache.ToArray();
                }
                TItem[] items = FindAllItems();
                Found = true;
                ItemsCache.Clear();
                ItemsCache.AddRange(items);
                return items;
            }
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected abstract TItem[] FindAllItems();
    }
}