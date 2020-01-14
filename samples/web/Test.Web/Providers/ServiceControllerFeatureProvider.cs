using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Test.Web.Providers
{
    internal class ServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private const string ControllerTypeNameSuffix = "Controller";
        private readonly IEnumerable<Type> ServiceTypes;

        public ServiceControllerFeatureProvider(IEnumerable<Type> ServiceTypes)
        {
            this.ServiceTypes = ServiceTypes;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in Reflection.CurrentAssembiles.SelectMany(o => o.DefinedTypes))
            {
                if (IsController(type) || ServiceTypes.Any(o => type.IsClass && o.IsAssignableFrom(type)) && !feature.Controllers.Contains(type))
                {
                    feature.Controllers.Add(type);
                }
            }
        }

        protected bool IsController(TypeInfo typeInfo)
        {
            //...
            return true;
        }
    }
}