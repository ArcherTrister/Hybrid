// -----------------------------------------------------------------------
//  <copyright file="UserRoleController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:49</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization;
using Hybrid.Authorization.Modules;
using Hybrid.Data;
using Hybrid.Entity;
using Hybrid.Filter;

using LeXun.Demo.Identity;
using LeXun.Demo.Identity.Dtos;
using LeXun.Demo.Identity.Entities;

using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeXun.Demo.Web.Areas.Admin.Controllers
{
    [ModuleInfo(Order = 3, Position = "Identity", PositionName = "身份认证模块")]
    [Description("管理-用户角色信息")]
    public class UserRoleController : AdminApiController
    {
        private readonly IIdentityContract _identityContract;
        private readonly IFilterService _filterService;

        public UserRoleController(IIdentityContract identityContract,
            IFilterService filterService)
        {
            _identityContract = identityContract;
            _filterService = filterService;
        }

        /// <summary>
        /// 读取用户角色信息
        /// </summary>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<UserRoleOutputDto> Read(PageRequest request)
        {
            Expression<Func<UserRole, bool>> predicate = _filterService.GetExpression<UserRole>(request.FilterGroup);
            Func<UserRole, bool> updateFunc = _filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Update).Compile();
            Func<UserRole, bool> deleteFunc = _filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Delete).Compile();

            PageResult<UserRoleOutputDto> page = _identityContract.UserRoles.ToPage(predicate, request.PageCondition, m => new
            {
                D = m,
                UserName = m.User.UserName,
                RoleName = m.Role.Name,
            }).ToPageResult(data => data.Select(m => new UserRoleOutputDto(m.D)
            {
                UserName = m.UserName,
                RoleName = m.RoleName,
                Updatable = updateFunc(m.D),
                Deletable = deleteFunc(m.D)
            }).ToArray());
            return page.ToPageData();
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="dtos">用户角色信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("更新")]
        public async Task<AjaxResult> Update(UserRoleInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));

            OperationResult result = await _identityContract.UpdateUserRoles(dtos);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ids">要删除的用户角色编号</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("删除")]
        public async Task<AjaxResult> Delete(Guid[] ids)
        {
            Check.NotNull(ids, nameof(ids));

            OperationResult result = await _identityContract.DeleteUserRoles(ids);
            return result.ToAjaxResult();
        }
    }
}