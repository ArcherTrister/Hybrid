﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult)
            {
                //ResultLogEntity logEntity = new ResultLogEntity()
                //{
                //    Result = JsonHelper.SerializeObject(((ObjectResult)context.Result).Value)
                //};

                //LogstashHelper.Log(logEntity);
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
