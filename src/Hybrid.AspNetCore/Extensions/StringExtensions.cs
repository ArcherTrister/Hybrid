using Hybrid.Data;
using Hybrid.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace Hybrid.AspNetCore.Extensions
{
    public static class StringExtensions
    {
        public static bool IsLocalUrl(this string url, [NotNull] HttpRequest request)
        {
            //Check.NotNull(request, nameof(request));
            Check.NotNull(url, nameof(url));

            return IsRelativeLocalUrl(url) || url.StartsWith(GetLocalUrlRoot(request));
        }

        private static string GetLocalUrlRoot(HttpRequest request)
        {
            return request.Scheme + "://" + request.Host;
        }

        private static bool IsRelativeLocalUrl(string url)
        {
            //This code is copied from System.Web.WebPages.RequestExtensions class.

            if (url.IsNullOrEmpty())
                return false;
            if (url[0] == 47 && (url.Length == 1 || url[1] != 47 && url[1] != 92))
                return true;
            if (url.Length > 1 && url[0] == 126)
                return url[1] == 47;
            return false;
        }
    }
}