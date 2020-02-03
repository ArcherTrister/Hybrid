using FlyingFish.Mobile.Ruqi.Dtos;

using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.Core.ModuleInfos;
using Hybrid.Data;
using Hybrid.Web.Identity.Entity;
using Hybrid.Zero.IdentityServer4;

using IdentityModel.Client;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;

using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlyingFish.Mobile.Controllers
{
    /// <summary>
    /// App登录
    /// </summary>
    [Description("网站-App登录")]
    [ModuleInfo(Order = 1)]
    public sealed class AppAccountController : LocalApiController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly SignInManager<User> _signInManager;
        //private readonly IEventService _events;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="signInManager"></param>
        /// <param name="events"></param>
        public AppAccountController(IHttpClientFactory httpClientFactory)
        //,
        //SignInManager<User> signInManager,
        //IEventService events)
        {
            _httpClientFactory = httpClientFactory;
            //_signInManager = signInManager;
            //_events = events;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginRequest">登录请求模型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AjaxResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            AjaxResponse<LoginResponse> ajaxResponse = new AjaxResponse<LoginResponse>(true);
            HttpClient signInClient = _httpClientFactory.CreateClient("SignInOrOutClient");

            // request access token
            var tokenResponse = await signInClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = signInClient.BaseAddress + "connect/token",
                ClientId = "mobileAppClient",
                ClientSecret = "mobile app secrect",
                Scope = "IdentityServerApi openid profile HybridCustom offline_access",
                UserName = loginRequest.UserName,
                Password = loginRequest.Password
            });

            if (tokenResponse.IsError)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Details = tokenResponse.Error, Message = tokenResponse.Exception.Message };
                return ajaxResponse;
            }

            var userInfoResponse = await signInClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = signInClient.BaseAddress + "connect/userinfo",
                Token = tokenResponse.AccessToken
            });
            if (userInfoResponse.IsError)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Details = userInfoResponse.Error, Message = userInfoResponse.Exception.Message };
                return ajaxResponse;
            }
            else
            {
                CustomUserInfoResponse customUserInfoResponse = JsonConvert.DeserializeObject<CustomUserInfoResponse>(userInfoResponse.Raw);
                CustomTokenResponse customTokenResponse = new CustomTokenResponse { AccessToken = tokenResponse.AccessToken, TokenType = tokenResponse.TokenType, ExpiresIn = tokenResponse.ExpiresIn, IdentityToken = tokenResponse.IdentityToken, RefreshToken = tokenResponse.RefreshToken };
                ajaxResponse.Result = new LoginResponse { TokenResponse = customTokenResponse, UserInfoResponse = customUserInfoResponse };
                return ajaxResponse;
            }
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<AjaxResponse<string>> Logout()
        {
            AjaxResponse<string> ajaxResponse = new AjaxResponse<string>("退出成功！");

            //if (User?.Identity.IsAuthenticated == true)
            //{
            //    // delete local authentication cookie
            //    await _signInManager.SignOutAsync();

            //    // raise the logout event
            //    await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            //}
            HttpClient signOutClient = _httpClientFactory.CreateClient("SignInOrOutClient");
            string accessToken = await HttpContext.GetTokenAsync(HybridConstants.LocalApi.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var accessTokenRevocationResponse = await signOutClient.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = signOutClient.BaseAddress + "connect/revocation",
                    ClientId = "mobileAppClient",
                    ClientSecret = "mobile app secrect",
                    Token = accessToken
                });
                if (accessTokenRevocationResponse.IsError)
                {
                    ajaxResponse.Success = false;
                    ajaxResponse.Error = new ErrorInfo { Message = accessTokenRevocationResponse.Error };
                    return ajaxResponse;
                }
            }

            //string refreshToken = await HttpContext.GetTokenAsync(HybridConstants.LocalApi.AuthenticationScheme, OpenIdConnectParameterNames.RefreshToken);

            //if (!string.IsNullOrWhiteSpace(refreshToken))
            //{
            //    var refreshTokenRevocationResponse = await signOutClient.RevokeTokenAsync(new TokenRevocationRequest
            //    {
            //        Address = "",
            //        ClientId = "",
            //        ClientSecret = "",
            //        Token = refreshToken
            //    });
            //    if (refreshTokenRevocationResponse.IsError)
            //    {
            //        ajaxResponse.Success = false;
            //        ajaxResponse.Error = new ErrorInfo { Message = refreshTokenRevocationResponse.Error };
            //        return ajaxResponse;
            //    }
            //}
            return ajaxResponse;
        }
    }
}