// -----------------------------------------------------------------------
//  <copyright file="DatabaseType.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-21 1:11</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Domain.EntityFramework
{
    /// <summary>
    /// 表示数据库类型，如SqlServer，Sqlite等
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// SqlServer数据库类型
        /// </summary>
        SqlServer,

        /// <summary>
        /// Sqlite数据库类型
        /// </summary>
        Sqlite,

        /// <summary>
        /// MySql数据库类型
        /// </summary>
        MySql,

        /// <summary>
        /// PostgreSql数据库类型
        /// </summary>
        PostgreSql,

        /// <summary>
        /// Oracle数据库类型
        /// </summary>
        Oracle
    }
}