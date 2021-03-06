﻿// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-05-14 17:37</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 自动事务提交过滤器，在<see cref="ActionFilterAttribute.OnResultExecuted"/>方法中执行<see cref="IUnitOfWork.Commit()"/>进行事务提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UnitOfWorkAttribute : ServiceFilterAttribute
    {
        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkAttribute"/>类型的新实例
        /// </summary>
        public UnitOfWorkAttribute()
            : base(typeof(UnitOfWorkFilterImpl))
        { }
    }
}