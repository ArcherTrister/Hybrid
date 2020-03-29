using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Hybrid.Data;
using Hybrid.Exceptions;
using Hybrid.Localization;
using Hybrid.Localization.Sources;
using Hybrid.Security;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Hybrid.Extensions;

namespace Hybrid.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Hybrid system.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class MvcController : Controller
    {
        public Stopwatch GenerationTime { get; }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new HybridException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }

        protected CultureInfo LocalCultureInfo
        {
            get
            {
                string value = HttpContext.Request.Cookies[HybridConstants.CultureCookieName];
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
                string value = HttpContext.Request.Cookies[HybridConstants.SchedulerCookieName];
                if (value.IsNullOrWhiteSpace())
                {
                    return null;
                }
                return Crypto.DesDecrypt(value);
            }
        }

        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected MvcController()
        {
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// 获取初始化时间
        /// </summary>
        /// <returns></returns>
        protected virtual Stopwatch GetGenerationTime()
        {
            return default;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
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
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}
