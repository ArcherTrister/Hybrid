// -----------------------------------------------------------------------
//  <copyright file="IHasTotalCount.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Application.Services.Dtos
{
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        int TotalCount { get; set; }
    }
}