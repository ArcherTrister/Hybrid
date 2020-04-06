using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Swagger
{
    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        //public void Apply(Operation operation, OperationFilterContext context)
        //{
        //    // Check for authorize attribute
        //    var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        //    var requiredScopes = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>();

        //    hasAuthorize = hasAuthorize || requiredScopes.Any();
        //    if (!hasAuthorize) return;

        //    operation.Responses.Add("401", new Response { Description = "Unauthorized" });
        //    operation.Responses.Add("403", new Response { Description = "Forbidden" });

        //    if (operation.Security == null)
        //        operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
        //    var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
        //                                {
        //                                        {"Bearer", new List<string>()},
        //                                      {"IdentityServer", requiredScopes.Select(attr => attr.Policy).Distinct().ToList()}
        //                                };
        //    operation.Security.Add(oAuthRequirements);
        //}

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check for authorize attribute
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            var requiredScopes = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>();

            hasAuthorize = hasAuthorize || requiredScopes.Any();
            if (!hasAuthorize) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "IdentityServer" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                },
                new OpenApiSecurityRequirement
                {
                    [ oAuthScheme ] = requiredScopes.Select(attr => attr.Policy).Distinct().ToList()
                }
            };
        }
    }
}
