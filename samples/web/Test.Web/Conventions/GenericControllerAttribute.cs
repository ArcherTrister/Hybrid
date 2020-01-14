using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;

namespace Test.Web.Conventions
{
    public class GenericControllerAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            Type entityType = controller.ControllerType.GetGenericArguments()[0];

            controller.ControllerName = entityType.Name;
        }
    }
}