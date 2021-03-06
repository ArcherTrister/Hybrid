﻿// -----------------------------------------------------------------------
//  <copyright file="RoleEntityController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-05 14:45</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization;
using Hybrid.Authorization.Modules;
using Hybrid.Data;
using Hybrid.Entity;
using Hybrid.Filter;

using Liuliu.Demo.Authorization;
using Liuliu.Demo.Authorization.Dtos;
using Liuliu.Demo.Authorization.Entities;
using Liuliu.Demo.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Liuliu.Demo.Web.Areas.Admin.Controllers
{
    [ModuleInfo(Order = 6, Position = "Auth", PositionName = "权限授权模块")]
    [Description("管理-角色数据权限")]
    public class RoleEntityController : AdminApiController
    {
        private readonly DataAuthManager _dataAuthManager;
        private readonly IFilterService _filterService;

        public RoleEntityController(DataAuthManager dataAuthManager,
            IFilterService filterService)
        {
            _dataAuthManager = dataAuthManager;
            _filterService = filterService;
        }

        /// <summary>
        /// 读取角色数据权限列表信息
        /// </summary>
        /// <param name="request">页请求信息</param>
        /// <returns>角色数据权限列表分页信息</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("ReadProperties", Controller = "EntityInfo")]
        [Description("读取")]
        public PageData<EntityRoleOutputDto> Read(PageRequest request)
        {
            Expression<Func<EntityRole, bool>> predicate = _filterService.GetExpression<EntityRole>(request.FilterGroup);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("RoleId"),
                    new SortCondition("EntityId"),
                    new SortCondition("Operation")
                };
            }
            RoleManager<Role> roleManager = HttpContext.RequestServices.GetService<RoleManager<Role>>();
            Func<EntityRole, bool> updateFunc = _filterService.GetDataFilterExpression<EntityRole>(null, DataAuthOperation.Update).Compile();
            Func<EntityRole, bool> deleteFunc = _filterService.GetDataFilterExpression<EntityRole>(null, DataAuthOperation.Delete).Compile();
            var page = _dataAuthManager.EntityRoles.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    D = m,
                    RoleName = roleManager.Roles.First(n => n.Id == m.RoleId).Name,
                    EntityName = m.EntityInfo.Name,
                    EntityType = m.EntityInfo.TypeName,
                }).ToPageResult(data => data.Select(m => new EntityRoleOutputDto(m.D)
                {
                    RoleName = m.RoleName,
                    EntityName = m.EntityName,
                    EntityType = m.EntityType,
                    Updatable = updateFunc(m.D),
                    Deletable = deleteFunc(m.D)
                }).ToArray());
            return page.ToPageData();
        }

        /// <summary>
        /// 新增角色数据权限信息
        /// </summary>
        /// <param name="dtos">角色数据权限信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [DependOnFunction("ReadNode", Controller = "Role")]
        [DependOnFunction("ReadNode", Controller = "EntityInfo")]
        [UnitOfWork]
        [Description("新增")]
        public async Task<AjaxResult> Create(params EntityRoleInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));

            OperationResult result = await _dataAuthManager.CreateEntityRoles(dtos);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 更新角色数据权限信息
        /// </summary>
        /// <param name="dtos">角色数据权限信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [DependOnFunction("ReadNode", Controller = "Role")]
        [DependOnFunction("ReadNode", Controller = "EntityInfo")]
        [UnitOfWork]
        [Description("更新")]
        public async Task<AjaxResult> Update(params EntityRoleInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));
            OperationResult result = await _dataAuthManager.UpdateEntityRoles(dtos);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 删除角色数据权限信息
        /// </summary>
        /// <param name="ids">角色数据权限信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("删除")]
        public async Task<AjaxResult> Delete(params Guid[] ids)
        {
            Check.NotNull(ids, nameof(ids));

            OperationResult result = await _dataAuthManager.DeleteEntityRoles(ids);
            return result.ToAjaxResult();
        }
    }
}