﻿// -----------------------------------------------------------------------
//  <copyright file="DataAuthOperation.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-14 11:18</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace ESoftor.Security
{
    /// <summary>
    /// 数据权限操作
    /// </summary>
    public enum DataAuthOperation
    {
        /// <summary>
        /// 读取
        /// </summary>
        [Description("读取")]
        Read,

        /// <summary>
        /// 更新
        /// </summary>
        [Description("更新")]
        Update,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete
    }
}