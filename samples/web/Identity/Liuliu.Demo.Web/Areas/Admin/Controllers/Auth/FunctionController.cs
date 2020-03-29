﻿// -----------------------------------------------------------------------
//  <copyright file="FunctionController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Liuliu.Demo.Authorization;
using Liuliu.Demo.Authorization.Dtos;
using Liuliu.Demo.Common.Dtos;

using Microsoft.AspNetCore.Mvc;

using Hybrid.AspNetCore.Mvc;
using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.Functions;
using Hybrid.Authorization.Modules;
using Hybrid.Caching;
using Hybrid.Data;
using Hybrid.Entity;
using Hybrid.Filter;


namespace Liuliu.Demo.Web.Areas.Admin.Controllers
{
    [ModuleInfo(Order = 2, Position = "Auth", PositionName = "权限授权模块")]
    [Description("管理-功能信息")]
    public class FunctionController : AdminApiController
    {
        private readonly FunctionAuthManager _functionAuthManager;
        private readonly ICacheService _cacheService;
        private readonly IFilterService _filterService;

        public FunctionController(FunctionAuthManager functionAuthManager,
            ICacheService cacheService,
            IFilterService filterService)
        {
            _functionAuthManager = functionAuthManager;
            _cacheService = cacheService;
            _filterService = filterService;
        }

        /// <summary>
        /// 读取功能信息
        /// </summary>
        /// <returns>功能信息集合</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<FunctionOutputDto> Read(PageRequest request)
        {
            IFunction function = this.GetExecuteFunction();
            request.AddDefaultSortCondition(
                new SortCondition("Area"),
                new SortCondition("Controller"),
                new SortCondition("IsController", ListSortDirection.Descending));

            Expression<Func<Function, bool>> predicate = _filterService.GetExpression<Function>(request.FilterGroup);
            PageResult<FunctionOutputDto> page = _cacheService.ToPageCache<Function, FunctionOutputDto>(_functionAuthManager.Functions, predicate, request.PageCondition, function);
            return page.ToPageData();
        }

        /// <summary>
        /// 读取功能[模块]树数据
        /// </summary>
        /// <param name="moduleId">模块编号</param>
        /// <returns>功能[模块]树数据</returns>
        [HttpGet]
        [Description("读取功能[模块]树数据")]
        public TreeNode[] ReadTreeNode(int moduleId)
        {
            Check.GreaterThan(moduleId, nameof(moduleId), 0);
            Guid[] checkFuncIds = _functionAuthManager.ModuleFunctions.Where(m => m.ModuleId == moduleId).Select(m => m.FunctionId).ToArray();

            var groups = _functionAuthManager.Functions.Unlocked()
                .Where(m => m.Area == null || m.Area == "Admin")
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.Area,
                    m.Controller,
                    m.Action,
                    m.IsController,
                    m.AccessType
                }).ToList().GroupBy(m => m.Area).OrderBy(m => m.Key).ToList();

            TreeNode root = new TreeNode { Id = Guid.NewGuid().ToString("N"), Name = "系统", HasChildren = true };
            foreach (var group1 in groups)
            {
                TreeNode areaNode = new TreeNode { Id = null, Name = group1.Key == null ? "网站" : group1.Key == "Admin" ? "管理" : "未知模块" };
                root.Items.Add(areaNode);
                var group2S = group1.GroupBy(m => m.Controller).OrderBy(m => m.Key).ToList();
                foreach (var group2 in group2S)
                {
                    areaNode.HasChildren = true;
                    var controller = group2.First(m => m.Action == null);
                    TreeNode controllerNode =
                        new TreeNode() { Id = Guid.NewGuid().ToString("N"), Name = $"{controller.Name}[{controller.Controller}]" };
                    areaNode.Items.Add(controllerNode);
                    foreach (var action in group2.Where(m => m.Action != null).OrderBy(m => m.Action))
                    {
                        controllerNode.HasChildren = true;
                        TreeNode actionNode = new TreeNode()
                        {
                            Id = action.Id.ToString("N"),
                            Name = action.Area == null
                                ? $"{action.Name}[{action.Controller}/{action.Action}]"
                                : $"{action.Name}[{action.Area}/{action.Controller}/{action.Action}]",
                            Data = action.AccessType,
                            IsChecked = checkFuncIds.Contains(action.Id)
                        };
                        controllerNode.Items.Add(actionNode);
                    }
                }
            }
            return new[] { root };
        }

        /// <summary>
        /// 更新功能信息
        /// </summary>
        /// <param name="dtos">功能信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("更新")]
        public async Task<AjaxResult> Update(FunctionInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));

            OperationResult result = await _functionAuthManager.UpdateFunctions(dtos);
            return result.ToAjaxResult();
        }
    }
}