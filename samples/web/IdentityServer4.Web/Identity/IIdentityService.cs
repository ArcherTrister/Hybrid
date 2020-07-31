﻿// -----------------------------------------------------------------------
//  <copyright file="IIdentityContract.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Web.Identity.Dto;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Identity.Entities;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.Web.Identity
{
    /// <summary>
    /// 业务契约：身份认证模块
    /// </summary>
    public interface IIdentityService
    {
        #region 用户信息业务

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<User> Users { get; }

        #endregion 用户信息业务

        #region 角色信息业务

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        IQueryable<Role> Roles { get; }

        #endregion 角色信息业务

        #region 用户角色信息业务

        /// <summary>
        /// 获取 用户角色信息查询数据集
        /// </summary>
        IQueryable<UserRole> UserRoles { get; }

        /// <summary>
        /// 检查用户角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户角色信息编号</param>
        /// <returns>用户角色信息是否存在</returns>
        Task<bool> CheckUserRoleExists(Expression<Func<UserRole, bool>> predicate, Guid id = default(Guid));

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="dtos">用户角色信息集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateUserRoles(params UserRoleInputDto[] dtos);

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ids">用户角色信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUserRoles(Guid[] ids);

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetUserRoles(Guid userId, Guid[] roleIds);

        #endregion 用户角色信息业务

        #region 用户登录信息业务

        /// <summary>
        /// 获取 用户登录信息查询数据集
        /// </summary>
        IQueryable<UserLogin> UserLogins { get; }

        /// <summary>
        /// 删除实体信息信息
        /// </summary>
        /// <param name="ids">要删除的实体信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUserLogins(params Guid[] ids);

        #endregion 用户登录信息业务

        #region 身份认证

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="dto">注册信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> Register(RegisterDto dto);

        /// <summary>
        /// 使用账号登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> Login(LoginDto dto);

        /// <summary>
        /// 使用第三方用户信息进行OAuth2登录
        /// </summary>
        /// <param name="loginInfo">第三方用户信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> LoginOAuth2(UserLoginInfoEx loginInfo);

        /// <summary>
        /// 登录并绑定现有账号
        /// </summary>
        /// <param name="loginInfoEx">第三方登录信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> LoginBind(UserLoginInfoEx loginInfoEx);

        /// <summary>
        /// 一键创建新用户并登录
        /// </summary>
        /// <param name="cacheId">第三方登录信息缓存编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> LoginOneKey(string cacheId);

        /// <summary>
        /// 账号退出
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> Logout(Guid userId);

        #endregion 身份认证
    }
}