using FlyingFish.Mobile.Ruqi.Dtos;

using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.AspNetCore.UI;
using Hybrid.Core.ModuleInfos;
using Hybrid.Data;
using Hybrid.Web.Identity.Entity;
using Hybrid.Zero.IdentityServer4;

using IdentityModel.Client;

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Web.Ruqi.Dtos;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Newtonsoft.Json;

using System;
using System.ComponentModel;
using System.Globalization;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IEventService _events;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="signInManager"></param>
        /// <param name="events"></param>
        public AppAccountController(
            IHttpClientFactory httpClientFactory,
            SignInManager<User> signInManager,
            IEventService events)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _events = events;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginRequest">登录请求模型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AjaxResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            AjaxResult<LoginResponse> ajaxResponse = new AjaxResult<LoginResponse>();
            HttpClient signInClient = _httpClientFactory.CreateClient("Identityserver4Client");

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
                //ajaxResponse.Error = new ErrorInfo(tokenResponse.Exception.Message, tokenResponse.Error);
                ajaxResponse.Content = tokenResponse.Error;
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
                //ajaxResponse.Error = new ErrorInfo(userInfoResponse.Exception.Message, userInfoResponse.Error);
                ajaxResponse.Content = userInfoResponse.Error;
                return ajaxResponse;
            }
            else
            {
                CustomUserInfoResponse customUserInfoResponse = JsonConvert.DeserializeObject<CustomUserInfoResponse>(userInfoResponse.Raw);//, new JsonSerializerSettings { ContractResolver = new UnderScoreCaseToCamelCasePropertyNamesContractResolver() }
                CustomTokenResponse customTokenResponse = new CustomTokenResponse { AccessToken = tokenResponse.AccessToken, TokenType = tokenResponse.TokenType, ExpiresIn = tokenResponse.ExpiresIn, IdentityToken = tokenResponse.IdentityToken, RefreshToken = tokenResponse.RefreshToken };
                ajaxResponse.Result = new LoginResponse { TokenResponse = customTokenResponse, UserInfoResponse = customUserInfoResponse };
                return ajaxResponse;
            }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        public async Task<AjaxResult<CustomTokenResponse>> RefreshToken(RefreshTokensRequest refreshTokensRequest)
        {
            AjaxResult<CustomTokenResponse> ajaxResponse = new AjaxResult<CustomTokenResponse>("刷新成功！");
            HttpClient refreshTokenClient = _httpClientFactory.CreateClient("Identityserver4Client");

            var refreshTokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = refreshTokenClient.BaseAddress + "connect/token",
                ClientId = "mobileAppClient",
                ClientSecret = "mobile app secrect",
                RefreshToken = refreshTokensRequest.RefreshToken
            });
            if (refreshTokenResponse.IsError)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Content = refreshTokenResponse.Error;
                return ajaxResponse;
            }
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(refreshTokenResponse.ExpiresIn);
            var tokens = new[]
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = identityToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = refreshTokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = refreshTokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name = "expires_at",
                    Value = expiresAt.ToString("o", CultureInfo.InvariantCulture)
                }
            };
            var authenticationInfo = await HttpContext.AuthenticateAsync(HybridConstants.LocalApi.AuthenticationScheme);
            authenticationInfo.Properties.StoreTokens(tokens);
            //await HttpContext.SignInAsync(HybridConstants.LocalApi.AuthenticationScheme, authenticationInfo.Principal, authenticationInfo.Properties);
            await HttpContext.SignInAsync(authenticationInfo.Principal, authenticationInfo.Properties);
            ajaxResponse.Result = new CustomTokenResponse { AccessToken = refreshTokenResponse.AccessToken, RefreshToken = refreshTokenResponse.RefreshToken, ExpiresIn = refreshTokenResponse.ExpiresIn, IdentityToken = refreshTokenResponse.IdentityToken, TokenType = refreshTokenResponse.TokenType };
            return ajaxResponse;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<AjaxResult<string>> Logout(LogoutRequest logoutRequest)
        {
            AjaxResult<string> ajaxResponse = new AjaxResult<string>("退出成功！");
            HttpClient signOutClient = _httpClientFactory.CreateClient("Identityserver4Client");

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            string accessToken = await HttpContext.GetTokenAsync(HybridConstants.LocalApi.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var accessTokenRevocationResponse = await signOutClient.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = signOutClient.BaseAddress + "connect/revocation",
                    ClientId = "mobileAppClient",
                    ClientSecret = "mobile app secrect",
                    TokenTypeHint = OpenIdConnectParameterNames.AccessToken,
                    Token = accessToken
                });
                if (accessTokenRevocationResponse.IsError)
                {
                    ajaxResponse.Success = false;
                    ajaxResponse.Content = accessTokenRevocationResponse.Error;
                    return ajaxResponse;
                }
            }
            var refreshTokenRevocationResponse = await signOutClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = signOutClient.BaseAddress + "connect/revocation",
                ClientId = "mobileAppClient",
                ClientSecret = "mobile app secrect",
                TokenTypeHint = OpenIdConnectParameterNames.RefreshToken,
                Token = logoutRequest.RefreshToken
            });
            if (refreshTokenRevocationResponse.IsError)
            {
                ajaxResponse.Success = false;
                ajaxResponse.Error = new ErrorInfo { Message = refreshTokenRevocationResponse.Error };
                return ajaxResponse;
            }

            await HttpContext.SignOutAsync();

            return ajaxResponse;
        }
    }
}