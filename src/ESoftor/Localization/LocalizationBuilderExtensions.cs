//using Hybrid;
//using Hybrid.Configuration;
//using Hybrid.Localization;
//using Hybrid.Localization.Configuration;
//using Hybrid.Localization.Dictionaries;
//using Hybrid.Localization.Dictionaries.Json;
//using Hybrid.Localization.Dictionaries.Xml;

//using Microsoft.AspNetCore.Builder;

//using System;
//using System.Linq;

//// ReSharper disable once CheckNamespace
//namespace Microsoft.Extensions.DependencyInjection
//{
//    public static class LocalizationBuilderExtensions
//    {
//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="app"></param>
//        /// <param name="options"></param>
//        /// <example>
//        /// app.UseI18n(options => {
//        ///    options.LocalizationSourceName = "Hybrid";
//        ///    options.Localizations.Add(new LocalizationFile { Assembly = typeof(DisposeAction).GetAssembly(), RootNamespace = "Hybrid.Localization.Sources.XmlSource", LocalizationFileType = LocalizationFileType.XmlEmbeddedFile });
//        /// });
//        /// </example>
//        /// <returns></returns>
//        public static IApplicationBuilder UseI18N(this IApplicationBuilder app)
//        {
//            if (app == null)
//            {
//                throw new ArgumentNullException(nameof(app));
//            }

//            LocalizationIocManager.ServiceProvider = app.ApplicationServices;

//            IServiceProvider provider = app.ApplicationServices;

//            var Configuration = provider.GetService<IHybridStartupConfiguration>();

//app.UseI18N(langOptions =>
//{
//    //langOptions.LocalizationSourceName = QuartzConsts.LocalizationSourceName;
//    //langOptions.Localizations.Add(new LocalizationFile
//    //{
//    //    Assembly = typeof(QuartzOptions).GetAssembly(),
//    //    RootNamespace = "Hybrid.Quartz.Dashboard.Localization.Sources.XmlSource",
//    //    LocalizationFileType = LocalizationFileType.XmlEmbeddedFile
//    //});
//    langOptions.LocalizationSourceName = QuartzConsts.LocalizationSourceName;
//    langOptions.Localizations.Add(new LocalizationFile
//    {
//        Assembly = typeof(QuartzOptions).GetAssembly(),
//        RootNamespace = "Hybrid.Quartz.Dashboard.Localization.Sources.JsonSource",
//        LocalizationFileType = LocalizationFileType.JsonEmbeddedFile
//    });
//});
//            foreach (LocalizationFile item in Configuration.Localization.Sources)
//            {
//                switch (item.LocalizationFileType)
//                {
//                    case LocalizationFileType.JsonEmbeddedFile:
//                        localizationConfiguration.Sources.Add(
//                            new DictionaryBasedLocalizationSource(
//                                localizationOptions.LocalizationSourceName,
//                                new JsonEmbeddedFileLocalizationDictionaryProvider(
//                                    item.Assembly, item.RootNamespace
//                                )));
//                        break;

//                    case LocalizationFileType.JsonFile:
//                        Check.NotNullOrEmpty(item.DirectoryPath, nameof(item.DirectoryPath));
//                        localizationConfiguration.Sources.Add(
//                            new DictionaryBasedLocalizationSource(
//                                localizationOptions.LocalizationSourceName,
//                                new JsonFileLocalizationDictionaryProvider(
//                                    item.DirectoryPath
//                                )));
//                        break;

//                    case LocalizationFileType.XmlFile:
//                        Check.NotNullOrEmpty(item.DirectoryPath, nameof(item.DirectoryPath));
//                        localizationConfiguration.Sources.Add(
//                            new DictionaryBasedLocalizationSource(
//                                localizationOptions.LocalizationSourceName,
//                                new XmlFileLocalizationDictionaryProvider(
//                                    item.DirectoryPath
//                                )));
//                        break;

//                    case LocalizationFileType.XmlEmbeddedFile:
//                        localizationConfiguration.Sources.Add(
//                            new DictionaryBasedLocalizationSource(
//                                localizationOptions.LocalizationSourceName,
//                                new XmlEmbeddedFileLocalizationDictionaryProvider(
//                                    item.Assembly, item.RootNamespace
//                                )));
//                        break;
//                }
//            }

//            var localizationManager = provider.GetService<ILocalizationManager>();

//            localizationManager.Initialize();

//            return app;
//        }
//    }
//}