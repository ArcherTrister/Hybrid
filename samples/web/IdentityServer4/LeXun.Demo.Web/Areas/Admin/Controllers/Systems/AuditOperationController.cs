// -----------------------------------------------------------------------
//  <copyright file="AuditOperationController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 14:47</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Modules;
using Hybrid.Entity;
using Hybrid.Filter;

using LeXun.Demo.Systems;
using LeXun.Demo.Systems.Dtos;
using LeXun.Demo.Systems.Entities;

using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace LeXun.Demo.Web.Areas.Admin.Controllers
{
    [ModuleInfo(Order = 2, Position = "Systems", PositionName = "系统管理模块")]
    [Description("管理-操作审计信息")]
    public class AuditOperationController : AdminApiController
    {
        private readonly IAuditContract _auditContract;
        private readonly IFilterService _filterService;

        public AuditOperationController(IAuditContract auditContract, IFilterService filterService)
        {
            _auditContract = auditContract;
            _filterService = filterService;
        }

        /// <summary>
        /// 读取操作审计信息
        /// </summary>
        /// <param name="request">页数据请求</param>
        /// <returns>操作审计信息的页数据</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<AuditOperationOutputDto> Read(PageRequest request)
        {
            Expression<Func<AuditOperation, bool>> predicate = _filterService.GetExpression<AuditOperation>(request.FilterGroup);
            request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
            var page = _auditContract.AuditOperations.ToPage<AuditOperation, AuditOperationOutputDto>(predicate, request.PageCondition);
            return page.ToPageData();
        }
    }
}