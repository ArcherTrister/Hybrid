using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Hybrid.Data;
using Hybrid.Localization;
using Hybrid.Localization.Sources;
using Hybrid.Security;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Hybrid.Extensions;

namespace Hybrid.AspNetCore.Mvc.Views
{
    /// <summary>
    /// Base class for all views in Hybrid.AspNetCore system.
    /// </summary>
    /// <typeparam name="TModel">Type of the View Model</typeparam>
    public abstract class BaseRazorPage<TModel> : RazorPage<TModel>
    {
        /// <summary>
        /// Gets the root path of the application.
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                string appPath = Context.Request.PathBase.Value;
                if (appPath == null)
                {
                    return "/";
                }

                appPath = appPath.EnsureEndsWith('/');

                return appPath;
            }
        }

        protected CultureInfo LocalCultureInfo
        {
            get
            {
                string value = Context.Request.Cookies[HybridConstants.CultureCookieName];
                if (value.IsNullOrWhiteSpace())
                {
                    return CultureInfo.CurrentUICulture;
                }
                ProviderCultureResult providerCultureResult = CookieRequestCultureProvider.ParseCookieValue(value);
                return new CultureInfo(providerCultureResult.UICultures.FirstOrDefault().Value);
            }
        }

        protected string LocalSchedulerName
        {
            get
            {
                string value = Context.Request.Cookies[HybridConstants.SchedulerCookieName];
                if (value.IsNullOrWhiteSpace())
                {
                    return null;
                }
                return Crypto.DesDecrypt(value);
            }
        }

        public Stopwatch GenerationTime { get; }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        [RazorInject]
        public ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this controller.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName
        {
            get => _localizationSource.Name;
            set => _localizationSource = LocalizationHelper.GetSource(value);
        }

        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseRazorPage()
        {
            _localizationSource = NullLocalizationSource.Instance;
            GenerationTime = Stopwatch.StartNew();
        }

        /// <summary>
        /// 获取初始化时间
        /// </summary>
        /// <returns></returns>
        protected virtual Stopwatch GetGenerationTime()
        {
            return GenerationTime;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return _localizationSource.GetString(name, LocalCultureInfo);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, params object[] args)
        {
            return _localizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return _localizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return _localizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// Gets localized string from given source for given key name and current language.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, params object[] args)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, args);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and specified culture information.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string from given source  for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, culture, args);
        }
    }
}
