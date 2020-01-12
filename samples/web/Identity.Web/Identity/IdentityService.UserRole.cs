﻿using ESoftor.Collections;
using ESoftor.Data;
using ESoftor.Permission.Identity;
using ESoftor.Web.Identity.Dto;
using ESoftor.Web.Identity.Entity;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ESoftor.Web.Identity
{
    public partial class IdentityService
    {
        /// <summary>
        /// 获取 用户角色信息查询数据集
        /// </summary>
        public IQueryable<UserRole> UserRoles
        {
            get { return _userRoleRepository.QueryAsNoTracking(); }
        }

        /// <summary>
        /// 检查用户角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户角色信息编号</param>
        /// <returns>用户角色信息是否存在</returns>
        public Task<bool> CheckUserRoleExists(Expression<Func<UserRole, bool>> predicate, Guid id = default(Guid))
        {
            return _userRoleRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="dtos">用户角色信息集合</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateUserRoles(params UserRoleInputDto[] dtos)
        {
            Check.Validate<UserRoleInputDto, Guid>(dtos, nameof(dtos));

            List<string> userNames = new List<string>();
            OperationResult result = await _userRoleRepository.UpdateAsync(dtos,
                (dto, entity) =>
                {
                    string userName = _userRoleRepository.QueryAsNoTracking(m => m.UserId == entity.UserId).Select(m => m.User.UserName).FirstOrDefault();
                    userNames.AddIfNotNull(userName);
                    return Task.FromResult(0);
                });
            //todo
            //if (result.Successed && userNames.Count > 0)
            //{
            //    OnlineUserCacheRemoveEventData eventData = new OnlineUserCacheRemoveEventData() { UserNames = userNames.ToArray() };
            //    _eventBus.Publish(eventData);
            //}
            return result;
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ids">用户角色信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteUserRoles(Guid[] ids)
        {
            List<string> userNames = new List<string>();
            OperationResult result = await _userRoleRepository.DeleteAsync(ids,
                (entity) =>
                {
                    string userName = _userRoleRepository.QueryAsNoTracking(m => m.UserId == entity.UserId).Select(m => m.User.UserName).FirstOrDefault();
                    userNames.AddIfNotNull(userName);
                    return Task.FromResult(0);
                });
            //todo
            //if (result.Successed && userNames.Count > 0)
            //{
            //    OnlineUserCacheRemoveEventData eventData = new OnlineUserCacheRemoveEventData() { UserNames = userNames.ToArray() };
            //    _eventBus.Publish(eventData);
            //}

            return result;
        }

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> SetUserRoles(Guid userId, Guid[] roleIds)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"编号为“{userId}”的用户不存在");
            }
            IList<string> roleNames = _roleManager.Roles.Where(m => roleIds.Contains(m.Id)).Select(m => m.Name).ToList();
            IList<string> existRoleNames = await _userManager.GetRolesAsync(user);
            string[] addRoleNames = roleNames.Except(existRoleNames).ToArray();
            string[] removeRoleNames = existRoleNames.Except(roleNames).ToArray();

            if (!addRoleNames.Union(removeRoleNames).Any())
            {
                return OperationResult.NoChanged;
            }

            try
            {
                IdentityResult result = await _userManager.AddToRolesAsync(user, addRoleNames);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                result = await _userManager.RemoveFromRolesAsync(user, removeRoleNames);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                await _userManager.UpdateSecurityStampAsync(user);

                //todo更新用户缓存使角色生效
                //OnlineUserCacheRemoveEventData eventData = new OnlineUserCacheRemoveEventData() { UserNames = new[] { user.UserName } };
                //_eventBus.Publish(eventData);
            }
            catch (InvalidOperationException ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message);
            }
            if (addRoleNames.Length > 0 && removeRoleNames.Length == 0)
            {
                return new OperationResult(OperationResultType.Success, $"用户“{user.UserName}”添加角色“{addRoleNames.ExpandAndToString()}”操作成功");
            }
            if (addRoleNames.Length == 0 && removeRoleNames.Length > 0)
            {
                return new OperationResult(OperationResultType.Success, $"用户“{user.UserName}”移除角色“{removeRoleNames.ExpandAndToString()}”操作成功");
            }
            return new OperationResult(OperationResultType.Success,
                $"用户“{user.UserName}”添加角色“{addRoleNames.ExpandAndToString()}”，移除角色“{removeRoleNames.ExpandAndToString()}”操作成功");
        }
    }
}
