using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Test.Web.Conventions
{
    internal class ControllerModelConvention : IControllerModelConvention
    {
        public ControllerModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ControllerModel controller)
        {
            if (!serviceType.IsAssignableFrom(controller.ControllerType)) return;

            var attrs = serviceType.GetCustomAttributes();
            var controllerAttrs = new List<object>();

            foreach (var att in attrs)
            {
                if (att is RouteAttribute routeAttr)
                {
                    var template = routeAttr.Template;
                    controllerAttrs.Add(new Router(template));
                }
            }

            if (controllerAttrs.Any())
            {
                controller.Selectors.Clear();
                ModelConventionHelper.AddRange(controller.Selectors, ModelConventionHelper.CreateSelectors(controllerAttrs));
            }
        }
    }
}