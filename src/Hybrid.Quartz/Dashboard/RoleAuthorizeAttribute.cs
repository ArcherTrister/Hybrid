//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//using System;
//using System.Threading.Tasks;

//namespace Hybrid.Quartz.Dashboard
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
//    public class RoleAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
//    {
//        public RoleAuthorizeAttribute(string roleName)
//        {
//            RoleName = roleName;
//        }

//        public string RoleName { get; set; }

//        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//        {
//            if (!context.HttpContext.User.HasClaim(c => c.Value.Equals(RoleName, StringComparison.OrdinalIgnoreCase)))
//            {
//                context.Result = new ForbidResult();
//            }
//            await Task.CompletedTask;
//        }
//    }
//}