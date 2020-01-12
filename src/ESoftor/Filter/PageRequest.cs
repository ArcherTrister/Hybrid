// -----------------------------------------------------------------------
//  <copyright file="PageRequest.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-21 21:40</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;

namespace ESoftor.Filter
{
    /// <summary>
    /// 分布查询请求
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 初始化一个<see cref="PageRequest"/>类型的新实例
        /// </summary>
        public PageRequest()
        {
            PageCondition = new PageCondition(1, 20);
            FilterGroup = new FilterGroup();
        }

        /// <summary>
        /// 获取或设置 分页条件信息
        /// </summary>
        public PageCondition PageCondition { get; set; }

        /// <summary>
        /// 获取或设置 查询条件组
        /// </summary>
        public FilterGroup FilterGroup { get; set; }

        /// <summary>
        /// 添加默认排序条件，只有排序为空时有效
        /// </summary>
        public void AddDefaultSortCondition(params SortCondition[] sortConditions)
        {
            Check.NotNullOrEmpty(sortConditions, nameof(sortConditions));
            if (PageCondition.SortConditions.Length == 0)
            {
                PageCondition.SortConditions = sortConditions;
            }
        }
    }
}