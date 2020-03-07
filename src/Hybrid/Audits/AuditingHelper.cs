// -----------------------------------------------------------------------
//  <copyright file="AuditingHelper.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

using Hybrid.Audits.Configuration;
using Hybrid.Authorization.Functions;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;

namespace Hybrid.Audits
{
    public static class AuditingHelper
    {
        public static bool ShouldSaveAudit(IAuditingConfiguration configuration, ClaimsPrincipal principal, IFunction function, MethodInfo methodInfo)
        {
            if (!configuration.IsEnabled)
            {
                return false;
            }

            if (!configuration.IsEnabledForAnonymousUsers && (!(principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity identity)))
            {
                return false;
            }

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
                {
                    return true;
                }

                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))
                {
                    return false;
                }
            }
            return function.AuditOperationEnabled;
        }

        public static string Serialize(object obj, List<Type> ignoredTypes)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(ignoredTypes)
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}