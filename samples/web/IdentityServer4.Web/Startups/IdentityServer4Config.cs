﻿// -----------------------------------------------------------------------
//  <copyright file="IdentityServer4Config.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Security.Claims;

using IdentityServer4;
using IdentityServer4.Models;

using System.Collections.Generic;

namespace ESoftor.Web.Startups
{
    public static class IdentityServer4Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new ApiResource[]
            {
                new ApiResource(ESoftorConstants.LocalApi.ScopeName, "My API #1",
                    new List<string>
                    {
                        HybridClaimTypes.UserId,
                        HybridClaimTypes.TenantId,
                        HybridClaimTypes.UserIdTypeName,
                        HybridClaimTypes.UserName,
                        HybridClaimTypes.AvatarUrl,
                        HybridClaimTypes.ClientId,
                        HybridClaimTypes.Email,
                        HybridClaimTypes.EmailVerified,
                        HybridClaimTypes.Gender,
                        HybridClaimTypes.MobilePhone,
                        HybridClaimTypes.NickName,
                        HybridClaimTypes.PhoneNumberVerified,
                        HybridClaimTypes.PostalCode,
                        HybridClaimTypes.PreferredUserName,
                        HybridClaimTypes.Role,
                        HybridClaimTypes.Subject
                    }
                )
            };

        public static IEnumerable<Client> GetClients() =>
            new Client[]
            {
                                // console client credentials flow client
                new Client
                {
                    ClientId = "consoleClient",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { ESoftorConstants.LocalApi.ScopeName }
                },
                // wpf client, password grant
                new Client
                {
                    ClientId = "wpfClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("wpf secrect".Sha256())
                    },
                    AllowedScopes = {
                        ESoftorConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "swaggerClient",//客服端名称
                    ClientName = "Swagger UI for demo_api",//描述
                    AllowedGrantTypes = GrantTypes.Implicit,//指定允许的授权类型（AuthorizationCode，Implicit，Hybrid，ResourceOwner，ClientCredentials的合法组合）。
                    AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                    RedirectUris =
                    {
                        "http://localhost:5002/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes = { ESoftorConstants.LocalApi.ScopeName }//指定客户端请求的api作用域。 如果为空，则客户端无法访问
                },
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { ESoftorConstants.LocalApi.ScopeName }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", ESoftorConstants.LocalApi.ScopeName }
                },

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", ESoftorConstants.LocalApi.ScopeName }
                }
            };
    }
}