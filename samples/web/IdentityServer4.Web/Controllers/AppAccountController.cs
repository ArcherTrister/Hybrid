using FlyingFish.Mobile.Ruqi.Dtos;

using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.AspNetCore.Mvc.Models;
using Hybrid.AspNetCore.Services;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization.ModuleInfos;
using Hybrid.Data;
using Hybrid.Domain.Repositories;
using Hybrid.EventBuses;
using Hybrid.Extensions;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.IdentityServer4;

using IdentityModel.Client;

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Web.Identity.Events;
using IdentityServer4.Web.Ruqi.Dtos;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Newtonsoft.Json;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Text;
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
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceProvider"></param>
        public AppAccountController(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IRepository<UserDetail, Guid> UserDetailRepository => _serviceProvider.GetRequiredService<IRepository<UserDetail, Guid>>();
        protected SignInManager<User> SignInManager => _serviceProvider.GetRequiredService<SignInManager<User>>();
        protected UserManager<User> UserManager => _serviceProvider.GetRequiredService<UserManager<User>>();
        protected IVerifyCodeService VerifyCodeService => _serviceProvider.GetRequiredService<IVerifyCodeService>();
        protected IEventBus EventBus => _serviceProvider.GetRequiredService<IEventBus>();
        protected IEventService EventService => _serviceProvider.GetRequiredService<IEventService>();
        protected IHttpClientFactory HttpClientFactory => _serviceProvider.GetRequiredService<IHttpClientFactory>();

        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="registerRequest">注册请求模型</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [UnitOfWork]
        [ModuleInfo]
        //[DependOnFunction("CheckUserNameExists")]
        //[DependOnFunction("CheckEmailExists")]
        //[DependOnFunction("CheckNickNameExists")]
        [Description("用户注册")]
        [AllowAnonymous]
        public async Task<AjaxResult<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            AjaxResult<RegisterResponse> ajaxResult = new AjaxResult<RegisterResponse>("用户注册成功");
            //if (!_verifyCodeService.CheckCode(registerRequest.VerifyCode, registerRequest.VerifyCodeId))
            //{
            //    ajaxResult.Success = false;
            //    ajaxResult.Content = "验证码错误，请刷新重试";
            //}
            User user = new User()
            {
                UserName = registerRequest.UserName,
                NickName = registerRequest.NickName ?? $"User_{new Random().NextLetterAndNumberString(8)}", //随机用户昵称
                Email = registerRequest.Email
            };

            IdentityResult result = await UserManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + "|");
                }
                ajaxResult.Content = sb.ToString();// "注册失败";
                ajaxResult.Success = false;
                return ajaxResult;
            }
            string registerIp = HttpContext.GetClientIp();
            UserDetail detail = new UserDetail() { RegisterIp = registerIp, UserId = user.Id };
            int count = await UserDetailRepository.InsertAsync(detail);

            //触发注册成功事件
            if (count > 0)
            {
                RegisterResponse registerResponse = new RegisterResponse
                {
                    Email = user.Email,
                    NickName = user.NickName,
                    UserName = user.UserName,
                    RegisterIp = registerIp
                };
                RegisterEventData eventData = new RegisterEventData() { User = user, RequestScheme = Request.Scheme, RequestHost = Request.Host };
                EventBus.Publish(eventData);
                ajaxResult.Result = registerResponse;
                return ajaxResult;
            }
            ajaxResult.Content = "保存用户IP失败";
            ajaxResult.Success = false;
            return ajaxResult;
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
            AjaxResult<LoginResponse> ajaxResponse = new AjaxResult<LoginResponse>("登陆成功");
            HttpClient signInClient = HttpClientFactory.CreateClient("Identityserver4Client");

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
        [HttpPost]
        public async Task<AjaxResult<CustomTokenResponse>> RefreshToken(RefreshTokensRequest refreshTokensRequest)
        {
            AjaxResult<CustomTokenResponse> ajaxResponse = new AjaxResult<CustomTokenResponse>("刷新Token成功");
            HttpClient refreshTokenClient = HttpClientFactory.CreateClient("Identityserver4Client");

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
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(refreshTokenResponse.ExpiresIn);
            var tokens = new[]
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = idToken
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
            var authenticationInfo = await HttpContext.AuthenticateAsync(HybridConsts.LocalApi.AuthenticationScheme);
            authenticationInfo.Properties.StoreTokens(tokens);
            //// 登录
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //    authenticationInfo.Principal, authenticationInfo.Properties);
            //await HttpContext.SignInAsync(HybridConsts.LocalApi.AuthenticationScheme, authenticationInfo.Principal, authenticationInfo.Properties);
            await HttpContext.SignInAsync(authenticationInfo.Principal, authenticationInfo.Properties);
            ajaxResponse.Result = new CustomTokenResponse { AccessToken = refreshTokenResponse.AccessToken, RefreshToken = refreshTokenResponse.RefreshToken, ExpiresIn = refreshTokenResponse.ExpiresIn, IdentityToken = refreshTokenResponse.IdentityToken, TokenType = refreshTokenResponse.TokenType };
            return ajaxResponse;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> Logout(LogoutRequest logoutRequest)
        {
            AjaxResult<string> ajaxResponse = new AjaxResult<string>("退出成功");
            HttpClient signOutClient = HttpClientFactory.CreateClient("Identityserver4Client");

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await SignInManager.SignOutAsync();

                // raise the logout event
                await EventService.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            await HttpContext.SignOutAsync();

            string accessToken = await HttpContext.GetTokenAsync(HybridConsts.LocalApi.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);

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

            return ajaxResponse;
        }
    }
}