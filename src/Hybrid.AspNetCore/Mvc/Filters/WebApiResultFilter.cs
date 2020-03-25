using Hybrid.AspNetCore.UI;
using Hybrid.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    public class WebApiResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //根据实际需求进行具体实现
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new AjaxResult { Success = false, ResultType = AjaxResultType.NoFound, Content = "未找到资源", Result = null });
                }
                else
                {
                    context.Result = new ObjectResult(new AjaxResult { Success = true, ResultType = AjaxResultType.Success, Content = "成功！", Result = objectResult.Value });
                }
            }
        }
    }
}