// -----------------------------------------------------------------------
//  <copyright file="IListResult.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Hybrid.Application.Services.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to return a list of items to clients.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}