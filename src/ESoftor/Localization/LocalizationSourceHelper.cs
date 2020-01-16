using ESoftor.Extensions;
using Hybrid.Localization.Configuration;

using Microsoft.Extensions.Logging;

using System;
using System.Globalization;

namespace Hybrid.Localization
{
    public static class LocalizationSourceHelper
    {
        internal static string ReturnGivenNameOrThrowException(
            ILocalizationConfiguration configuration,
            string sourceName,
            string name,
            CultureInfo culture,
            ILogger logger = null
            )
        {
            string exceptionMessage = $"Can not find '{name}' in localization source '{sourceName}'!";

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new ArgumentNullException(exceptionMessage);
            }

            if (configuration.LogWarnMessageIfNotFound)
            {
                //(logger ?? LogProvider.For<LocalizationSourceHelper>()).Warn(exceptionMessage);
                //(logger ?? LogHelper.Logger).Warn(exceptionMessage);
            }

            string notFoundText = configuration.HumanizeTextIfNotFound
                ? name.ToSentenceCase(culture)
                : name;

            return configuration.WrapGivenTextIfNotFound
                ? $"[{notFoundText}]"
                : notFoundText;
        }
    }
}