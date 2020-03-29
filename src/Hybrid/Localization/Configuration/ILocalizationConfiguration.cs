using Hybrid.Domain.Entities;

using System.Collections.Generic;

namespace Hybrid.Localization.Configuration
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    public interface ILocalizationConfiguration : IEnabled
    {
        /// <summary>
        /// Used to set languages available for this application.
        /// </summary>
        HashSet<LanguageInfo> Languages { get; }

        /// <summary>
        /// List of localization sources.
        /// </summary>
        ILocalizationSourceList Sources { get; }

        /// <summary>
        /// If this is set to true, the given text (name) is returned
        /// if not found in the localization source. That prevent exceptions if
        /// given name is not defined in the localization sources.
        /// Also writes a warning log.
        /// Default: true.
        /// </summary>
        bool ReturnGivenTextIfNotFound { get; set; }

        /// <summary>
        /// It returns the given text by wrapping with [ and ] chars
        /// if not found in the localization source.
        /// This is considered only if <see cref="ReturnGivenTextIfNotFound"/> is true.
        /// Default: true.
        /// </summary>
        bool WrapGivenTextIfNotFound { get; set; }

        /// <summary>
        /// It returns the given text by converting string from 'PascalCase' to a 'Sentense case'
        /// if not found in the localization source.
        /// This is considered only if <see cref="ReturnGivenTextIfNotFound"/> is true.
        /// Default: true.
        /// </summary>
        bool HumanizeTextIfNotFound { get; set; }

        /// <summary>
        /// Write (or not write) a warning log if given text can not found in the localization source.
        /// Default: true.
        /// </summary>
        bool LogWarnMessageIfNotFound { get; set; }
    }
}