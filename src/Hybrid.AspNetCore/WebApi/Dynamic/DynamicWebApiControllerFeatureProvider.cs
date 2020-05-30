using Hybrid.AspNetCore.WebApi.Dynamic.Attributes;
using Hybrid.Reflection;

using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Reflection;

namespace Hybrid.AspNetCore.WebApi.Dynamic
{
    public class DynamicWebApiControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            //var type = typeInfo.AsType();

            //if (!typeof(IDynamicWebApi).IsAssignableFrom(type) ||
            //    !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            //{
            //    return false;
            //}

            //if (typeInfo.GetSingleAttributeOrDefaultByFullSearch<NonDynamicWebApiAttribute>() != null)
            //{
            //    return false;
            //}

            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                isController = typeInfo.HasAttribute<DynamicWebApiAttribute>();
                if (isController)
                    Console.WriteLine();
                isController = typeInfo.AsType().HasAttribute<DynamicWebApiAttribute>();
                if (isController)
                    Console.WriteLine();
            }

            //if (typeInfo.HasAttribute<NoneDynamicWebApiAttribute>())
            //    return false;

            //if (typeInfo.HasAttribute<DynamicWebApiAttribute>())
            //    return true;

            return isController;
        }
    }
}