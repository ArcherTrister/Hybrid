// -----------------------------------------------------------------------
//  <copyright file="IModuleUserStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Dependency;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.Zero.Security
{
    /// <summary>
    /// 模块用户信息存储
    /// </summary>
    [IgnoreDependency]
    public interface IModuleUserStore<TModuleUser, in TUserKey, TModuleKey>
    {
        #region 模块用户信息业务

        /// <summary>
        /// 获取 模块用户信息查询数据集
        /// </summary>
        IQueryable<TModuleUser> ModuleUsers { get; }

        /// <summary>
        /// 检查模块用户信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块用户信息编号</param>
        /// <returns>模块用户信息是否存在</returns>
        Task<bool> CheckModuleUserExists(Expression<Func<TModuleUser, bool>> predicate, Guid id = default(Guid));

        /// <summary>
        /// 设置用户的可访问模块
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="moduleIds">要赋给用户的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetUserModules(TUserKey userId, TModuleKey[] moduleIds);

        /// <summary>
        /// 获取用户自己的可访问模块编号
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>模块编号集合</returns>
        TModuleKey[] GetUserSelfModuleIds(TUserKey userId);

        /// <summary>
        /// 获取用户及其拥有角色可访问模块编号
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>模块编号集合</returns>
        TModuleKey[] GetUserWithRoleModuleIds(TUserKey userId);

        #endregion 模块用户信息业务
    }
}