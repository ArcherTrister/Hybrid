// -----------------------------------------------------------------------
//  <copyright file="HybridApplicationModelConvention.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.DynamicWebApi;
using Hybrid.AspNetCore.DynamicWebApi.Attributes;
using Hybrid.Collections;
using Hybrid.Data;
using Hybrid.Extensions;
using Hybrid.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Linq;
using System.Reflection;

namespace Hybrid.AspNetCore.Mvc
{
    internal class HybridApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var removeList = application.Controllers.Where(p =>
                HybridConstants.CustomController.ValidEndings.Any(x =>
                    p.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase))
                && !p.ControllerType.GenericTypeArguments.Any()).ToList();
            foreach (var item in removeList)
            {
                application.Controllers.Remove(item);
            }
            foreach (var controller in application.Controllers)
            {
                var type = controller.ControllerType.AsType();
                var dynamicWebApiAttr = type.GetTypeInfo().GetSingleAttributeOrDefaultByFullSearch<DynamicWebApiAttribute>();
                if (dynamicWebApiAttr != null)
                {
                    controller.ControllerName = controller.ControllerName.RemovePostFix(DynamicWebApiConsts.ControllerPostfixes.ToArray());
                    ConfigureArea(controller, dynamicWebApiAttr);
                    ConfigureDynamicWebApi(controller, dynamicWebApiAttr);
                }
            }
        }

        private void ConfigureArea(ControllerModel controller, DynamicWebApiAttribute attr)
        {
            if (!controller.RouteValues.ContainsKey("area"))
            {
                if (attr != null && !string.IsNullOrEmpty(attr.Area))
                {
                    controller.RouteValues["area"] = attr.Area;
                }
                else if (!string.IsNullOrEmpty(DynamicWebApiConsts.DefaultAreaName))
                {
                    controller.RouteValues["area"] = DynamicWebApiConsts.DefaultAreaName;
                }
            }
        }

        private void ConfigureDynamicWebApi(ControllerModel controller, DynamicWebApiAttribute controllerAttr)
        {
            ConfigureApiExplorer(controller);
            ConfigureSelector(controller, controllerAttr);
            ConfigureParameters(controller);
        }

        private void ConfigureParameters(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                foreach (var para in action.Parameters)
                {
                    if (para.BindingInfo != null)
                    {
                        continue;
                    }

                    if (!para.ParameterInfo.ParameterType.IsPrimitiveExtendedIncludingNullable())
                    {
                        if (CanUseFormBodyBinding(action, para))
                        {
                            para.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        }
                    }
                }
            }
        }

        private bool CanUseFormBodyBinding(ActionModel action, ParameterModel parameter)
        {
            if (DynamicWebApiConsts.FormBodyBindingIgnoredTypes.Any(t => t.IsAssignableFrom(parameter.ParameterInfo.ParameterType)))
            {
                return false;
            }

            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpMethodActionConstraint == null)
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #region ConfigureApiExplorer

        private void ConfigureApiExplorer(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                controller.ApiExplorer.IsVisible = true;
            }

            foreach (var action in controller.Actions)
            {
                ConfigureApiExplorer(action);
            }
        }

        private void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible == null)
            {
                action.ApiExplorer.IsVisible = true;
            }
        }

        #endregion ConfigureApiExplorer

        private void ConfigureSelector(ControllerModel controller, DynamicWebApiAttribute controllerAttr)
        {
            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                return;
            }

            var areaName = string.Empty;

            if (controllerAttr != null)
            {
                areaName = controllerAttr.Area;
            }

            foreach (var action in controller.Actions)
            {
                ConfigureSelector(areaName, controller.ControllerName, action);
            }
        }

        private void ConfigureSelector(string areaName, string controllerName, ActionModel action)
        {
            var nonAttr = action.ActionMethod.GetSingleAttributeOrDefault<NonDynamicWebApiAttribute>();

            if (nonAttr != null)
            {
                return;
            }

            if (action.Selectors.IsNullOrEmpty() || action.Selectors.Any(a => a.ActionConstraints.IsNullOrEmpty()))
            {
                AddAppServiceSelector(areaName, controllerName, action);
            }
            else
            {
                NormalizeSelectorRoutes(areaName, controllerName, action);
            }
        }

        private void AddAppServiceSelector(string areaName, string controllerName, ActionModel action)
        {
            if (DynamicWebApiConsts.IsUseRestFul)
            {
                action.ActionName = GetRestFulActionName(action.ActionName);
            }

            var verb = GetHttpVerb(action);

            var appServiceSelectorModel = action.Selectors[0];

            if (appServiceSelectorModel.AttributeRouteModel == null)
            {
                appServiceSelectorModel.AttributeRouteModel = CreateActionRouteModel(areaName, controllerName, action);
            }

            if (!appServiceSelectorModel.ActionConstraints.Any())
            {
                appServiceSelectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));
                switch (verb)
                {
                    case "GET":
                        appServiceSelectorModel.EndpointMetadata.Add(new HttpGetAttribute());
                        break;

                    case "POST":
                        appServiceSelectorModel.EndpointMetadata.Add(new HttpPostAttribute());
                        break;

                    case "PUT":
                        appServiceSelectorModel.EndpointMetadata.Add(new HttpPutAttribute());
                        break;

                    case "DELETE":
                        appServiceSelectorModel.EndpointMetadata.Add(new HttpDeleteAttribute());
                        break;

                    default:
                        throw new Exception($"Unsupported http verb: {verb}.");
                }
            }
        }

        /// <summary>
        /// Processing action name
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private static string GetRestFulActionName(string actionName)
        {
            // custom process action name
            var appConstsActionName = DynamicWebApiConsts.GetRestFulActionName?.Invoke(actionName);
            if (appConstsActionName != null)
            {
                return appConstsActionName;
            }

            // default process action name.

            // Remove Postfix
            actionName = actionName.RemovePostFix(DynamicWebApiConsts.ActionPostfixes.ToArray());

            // Remove Prefix
            var verbKey = actionName.GetPascalOrCamelCaseFirstWord().ToLower();
            if (DynamicWebApiConsts.HttpVerbs.ContainsKey(verbKey))
            {
                if (actionName.Length == verbKey.Length)
                {
                    return "";
                }
                else
                {
                    return actionName.Substring(verbKey.Length);
                }
            }
            else
            {
                return actionName;
            }
        }

        private static void NormalizeSelectorRoutes(string areaName, string controllerName, ActionModel action)
        {
            if (DynamicWebApiConsts.IsUseRestFul)
                action.ActionName = GetRestFulActionName(action.ActionName);
            foreach (var selector in action.Selectors)
            {
                selector.AttributeRouteModel = selector.AttributeRouteModel == null ?
                     CreateActionRouteModel(areaName, controllerName, action) :
                     AttributeRouteModel.CombineAttributeRouteModel(CreateActionRouteModel(areaName, controllerName, action), selector.AttributeRouteModel);
            }
        }

        private static string GetHttpVerb(ActionModel action)
        {
            var getValueSuccess = DynamicWebApiConsts.AssemblyDynamicWebApiOptions
                .TryGetValue(action.Controller.ControllerType.Assembly, out AssemblyDynamicWebApiOptions assemblyDynamicWebApiOptions);
            if (getValueSuccess && !string.IsNullOrWhiteSpace(assemblyDynamicWebApiOptions?.HttpVerb))
            {
                return assemblyDynamicWebApiOptions.HttpVerb;
            }

            var verbKey = action.ActionName.GetPascalOrCamelCaseFirstWord().ToLower();

            var verb = DynamicWebApiConsts.HttpVerbs.ContainsKey(verbKey) ? DynamicWebApiConsts.HttpVerbs[verbKey] : DynamicWebApiConsts.DefaultHttpVerb;
            return verb;
        }

        private static string GetApiPreFix(ActionModel action)
        {
            var getValueSuccess = DynamicWebApiConsts.AssemblyDynamicWebApiOptions
                .TryGetValue(action.Controller.ControllerType.Assembly, out AssemblyDynamicWebApiOptions assemblyDynamicWebApiOptions);
            if (getValueSuccess && !string.IsNullOrWhiteSpace(assemblyDynamicWebApiOptions?.ApiPrefix))
            {
                return assemblyDynamicWebApiOptions.ApiPrefix;
            }

            return DynamicWebApiConsts.DefaultApiPreFix;
        }

        private static AttributeRouteModel CreateActionRouteModel(string areaName, string controllerName, ActionModel action)
        {
            var apiPreFix = GetApiPreFix(action);
            var routeStr = $"{apiPreFix}/{areaName}/{controllerName}/{action.ActionName}".Replace("//", "/");
            return new AttributeRouteModel(new RouteAttribute(routeStr));
        }

    }
}