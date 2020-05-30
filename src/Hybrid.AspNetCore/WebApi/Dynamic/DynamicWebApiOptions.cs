
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hybrid.AspNetCore.WebApi.Dynamic
{
    public class DynamicWebApiOptions
    {
        public DynamicWebApiOptions()
        {
            RemoveControllerPostfixes = new List<string>() { "AppService", "ApplicationService", "Service" };
            RemoveActionPostfixes = new List<string>() { "Async" };
            DefaultHttpVerb = "POST";
            DefaultApiPrefix = "api";
            IsUseRestFul = false;
            AssemblyDynamicWebApiOptions = new Dictionary<Assembly, AssemblyDynamicWebApiOptions>();
        }

        /// <summary>
        /// API HTTP Verb.
        /// <para></para>
        /// Default value is "POST".
        /// </summary>
        public string DefaultHttpVerb { get; set; }

        /// <summary>
        /// 默认区域名称
        /// </summary>
        public string DefaultAreaName { get; set; }

        /// <summary>
        /// Routing prefix for all APIs
        /// <para></para>
        /// Default value is "api".
        /// </summary>
        public string DefaultApiPrefix { get; set; }

        /// <summary>
        /// 是否使用RestFul
        /// </summary>
        public bool IsUseRestFul { get; set; }

        /// <summary>
        /// Remove the dynamic API class(Controller) name postfix.
        /// <para></para>
        /// Default value is {"AppService", "ApplicationService"}.
        /// </summary>
        public List<string> RemoveControllerPostfixes { get; set; }

        /// <summary>
        /// Remove the dynamic API class's method(Action) postfix.
        /// <para></para>
        /// Default value is {"Async"}.
        /// </summary>
        public List<string> RemoveActionPostfixes { get; set; }

        /// <summary>
        /// The method that processing the name of the action.
        /// </summary>
        public Func<string, string> GetRestFulActionName { get; set; }

        /// <summary>
        /// Specifies the dynamic webapi options for the assembly.
        /// </summary>
        public Dictionary<Assembly, AssemblyDynamicWebApiOptions> AssemblyDynamicWebApiOptions { get; }

        /// <summary>
        /// Verify that all configurations are valid
        /// </summary>
        public void Valid()
        {
            if (string.IsNullOrEmpty(DefaultHttpVerb))
            {
                throw new ArgumentException($"{nameof(DefaultHttpVerb)} can not be empty.");
            }

            if (string.IsNullOrEmpty(DefaultAreaName))
            {
                DefaultAreaName = string.Empty;
            }

            if (string.IsNullOrEmpty(DefaultApiPrefix))
            {
                DefaultApiPrefix = string.Empty;
            }

            if (RemoveControllerPostfixes == null)
            {
                throw new ArgumentException($"{nameof(RemoveControllerPostfixes)} can not be null.");
            }
        }

        /// <summary>
        /// Add the dynamic webapi options for the assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="apiPreFix"></param>
        /// <param name="httpVerb"></param>
        public void AddAssemblyOptions(Assembly assembly, string apiPreFix = null, string httpVerb = null)
        {
            if (assembly == null)
            {
                throw new ArgumentException($"{nameof(assembly)} can not be null.");
            }

            this.AssemblyDynamicWebApiOptions[assembly] = new AssemblyDynamicWebApiOptions(apiPreFix, httpVerb);
        }
    }

    /// <summary>
    /// Specifies the dynamic webapi options for the assembly.
    /// </summary>
    public class AssemblyDynamicWebApiOptions
    {
        /// <summary>
        /// Routing prefix for all APIs
        /// <para></para>
        /// Default value is null.
        /// </summary>
        public string ApiPrefix { get; }

        /// <summary>
        /// API HTTP Verb.
        /// <para></para>
        /// Default value is null.
        /// </summary>
        public string HttpVerb { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiPrefix">Routing prefix for all APIs</param>
        /// <param name="httpVerb">API HTTP Verb.</param>
        public AssemblyDynamicWebApiOptions(string apiPrefix = null, string httpVerb = null)
        {
            ApiPrefix = apiPrefix;
            HttpVerb = httpVerb;
        }
    }
}