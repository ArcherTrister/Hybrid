// -----------------------------------------------------------------------
//  <copyright file="BinAssemblyFinder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-15 23:33</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Filter
{
    /// <summary>
    /// 指定可用于表数据存在性检查类型的值
    /// </summary>
    public enum CheckExistsType
    {
        /// <summary>
        ///   插入数据时重复性检查
        /// </summary>
        Insert,

        /// <summary>
        ///   编辑数据时重复性检查
        /// </summary>
        Update
    }
}