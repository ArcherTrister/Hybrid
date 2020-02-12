using System;

namespace Hybrid.AspNetCore.Mvc.Models
{
    public sealed class ErrorInfos
    {
        public ErrorInfos() { }

        //
        // 摘要:
        //     The display mode passed from the authorization request.
        public string DisplayMode { get; set; }
        //
        // 摘要:
        //     The UI locales passed from the authorization request.
        public string UiLocales { get; set; }
        //
        // 摘要:
        //     Gets or sets the error code.
        public string Error { get; set; }
        //
        // 摘要:
        //     Gets or sets the error description.
        public string ErrorDescription { get; set; }
        //
        // 摘要:
        //     The per-request identifier. This can be used to display to the end user and can
        //     be used in diagnostics.
        public string RequestId { get; set; }
        //
        // 摘要:
        //     The redirect URI.
        public string RedirectUri { get; set; }
        //
        // 摘要:
        //     The response mode.
        public string ResponseMode { get; set; }
        //
        // 摘要:
        //     The client id making the request (if available).
        public string ClientId { get; set; }
    }
}
