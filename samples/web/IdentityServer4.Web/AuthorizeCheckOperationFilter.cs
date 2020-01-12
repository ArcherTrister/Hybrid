﻿// -----------------------------------------------------------------------
//  <copyright file="AuthorizeCheckOperationFilter.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Web
{
    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            // Check for authorize attribute
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            var requiredScopes = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>();

            hasAuthorize = hasAuthorize || requiredScopes.Any();
            if (!hasAuthorize) return;

            operation.Responses.Add("401", new Response { Description = "Unauthorized" });
            operation.Responses.Add("403", new Response { Description = "Forbidden" });

            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                                        {
                                                {"Bearer", new List<string>()},
                                              {"IdentityServer", requiredScopes.Select(attr => attr.Policy).Distinct().ToList()}
                                        };
            operation.Security.Add(oAuthRequirements);
        }
    }
}