// -----------------------------------------------------------------------
//  <copyright file="PageUnitOfWorkFilter.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Uow;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    public class PageUnitOfWorkFilter : IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            if (context.Exception != null && !context.ExceptionHandled)
            {
                unitOfWorkManager?.Rollback();
            }
            else
            {
                unitOfWorkManager?.Commit();
            }
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}