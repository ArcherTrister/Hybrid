// -----------------------------------------------------------------------
//  <copyright file="MvcController.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace ESoftor.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Hybrid system.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class MvcController : Controller
    {
        //public Stopwatch GenerationTime { get; }

        ///// <summary>
        ///// Reference to the localization manager.
        ///// </summary>
        //public ILocalizationManager LocalizationManager { protected get; set; }

        ///// <summary>
        ///// Gets/sets name of the localization source that is used in this application service.
        ///// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        ///// </summary>
        //protected string LocalizationSourceName { get; set; }

        ///// <summary>
        ///// Gets localization source.
        ///// It's valid if <see cref="LocalizationSourceName"/> is set.
        ///// </summary>
        //protected ILocalizationSource LocalizationSource
        //{
        //    get
        //    {
        //        if (LocalizationSourceName == null)
        //        {
        //            throw new Exception("Must set LocalizationSourceName before, in order to get LocalizationSource");
        //        }

        //        if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
        //        {
        //            _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
        //        }

        //        return _localizationSource;
        //    }
        //}

        //protected CultureInfo LocalCultureInfo
        //{
        //    get
        //    {
        //        string value = HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
        //        if (value.IsNullOrWhiteSpace())
        //        {
        //            return CultureInfo.CurrentUICulture;
        //        }
        //        ProviderCultureResult providerCultureResult = CookieRequestCultureProvider.ParseCookieValue(value);
        //        return new CultureInfo(providerCultureResult.UICultures.FirstOrDefault().Value);
        //    }
        //}

        //protected string LocalSchedulerName
        //{
        //    get
        //    {
        //        string value = HttpContext.Request.Cookies[HybridConsts.SchedulerCookieName];
        //        if (value.IsNullOrWhiteSpace())
        //        {
        //            return null;
        //        }
        //        return Crypto.DesDecrypt(value);
        //    }
        //}

        //private ILocalizationSource _localizationSource;

        ///// <summary>
        ///// Reference to the logger to write logs.
        ///// </summary>
        //internal ILog Logger { get; set; }

        ///// <summary>
        ///// Constructor.
        ///// </summary>
        //protected PageControllerBase()
        //{
        //    Logger = LogProvider.For<PageControllerBase>();
        //    LocalizationManager = NullLocalizationManager.Instance;
        //}

        ///// <summary>
        ///// 获取初始化时间
        ///// </summary>
        ///// <returns></returns>
        //protected virtual Stopwatch GetGenerationTime()
        //{
        //    return default;
        //}

        ///// <summary>
        ///// Gets localized string for given key name and current language.
        ///// </summary>
        ///// <param name="name">Key name</param>
        ///// <returns>Localized string</returns>
        //protected virtual string L(string name)
        //{
        //    return LocalizationSource.GetString(name);
        //}

        ///// <summary>
        ///// Gets localized string for given key name and current language with formatting strings.
        ///// </summary>
        ///// <param name="name">Key name</param>
        ///// <param name="args">Format arguments</param>
        ///// <returns>Localized string</returns>
        //protected string L(string name, params object[] args)
        //{
        //    return LocalizationSource.GetString(name, args);
        //}

        ///// <summary>
        ///// Gets localized string for given key name and specified culture information.
        ///// </summary>
        ///// <param name="name">Key name</param>
        ///// <param name="culture">culture information</param>
        ///// <returns>Localized string</returns>
        //protected virtual string L(string name, CultureInfo culture)
        //{
        //    return LocalizationSource.GetString(name, culture);
        //}

        ///// <summary>
        ///// Gets localized string for given key name and current language with formatting strings.
        ///// </summary>
        ///// <param name="name">Key name</param>
        ///// <param name="culture">culture information</param>
        ///// <param name="args">Format arguments</param>
        ///// <returns>Localized string</returns>
        //protected string L(string name, CultureInfo culture, params object[] args)
        //{
        //    return LocalizationSource.GetString(name, culture, args);
        //}
    }
}