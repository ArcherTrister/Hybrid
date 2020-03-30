using Hybrid.Authorization.Functions;
using Hybrid.Core.Configuration;

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

            if (methodInfo.IsDefined(typeof(AuditIgnoreAttribute), true))
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

                if (classType.GetTypeInfo().IsDefined(typeof(AuditIgnoreAttribute), true))
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