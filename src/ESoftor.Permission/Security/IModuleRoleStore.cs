﻿// -----------------------------------------------------------------------
//  <copyright file="IModuleRoleStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ESoftor.Permission.Security
{
    /// <summary>
    /// 定义模块角色信息存储
    /// </summary>
    public interface IModuleRoleStore<TModuleRole, in TRoleKey, TModuleKey>
    {
        #region 模块角色信息业务

        /// <summary>
        /// 获取 模块角色信息查询数据集
        /// </summary>
        IQueryable<TModuleRole> ModuleRoles { get; }

        /// <summary>
        /// 检查模块角色信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块角色信息编号</param>
        /// <returns>模块角色信息是否存在</returns>
        Task<bool> CheckModuleRoleExists(Expression<Func<TModuleRole, bool>> predicate, Guid id = default(Guid));

        /// <summary>
        /// 设置角色的可访问模块
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="moduleIds">要赋予的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetRoleModules(TRoleKey roleId, TModuleKey[] moduleIds);

        /// <summary>
        /// 获取角色可访问模块编号
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>模块编号集合</returns>
        TModuleKey[] GetRoleModuleIds(TRoleKey roleId);

        #endregion 模块角色信息业务
    }
}