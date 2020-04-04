using Hybrid.Data;
using Hybrid.Security.Claims;
using Hybrid.Zero.IdentityServer4.Extensions;

using IdentityServer4;
using IdentityServer4.Models;

using System.Collections.Generic;

namespace LeXun.Demo.Web.Startups
{
    public static class IdentityServer4Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                //new IdentityResources.Email(),
                //new IdentityResources.Phone(),
                new IdentityResourcesExtensions.HybridCustomResource()
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new ApiResource[]
            {
                new ApiResource(HybridConstants.LocalApi.ScopeName, "My API #1",
                    new List<string>
                    {
                        HybridClaimTypes.UserIdTypeName,
                        HybridClaimTypes.ClientId,
                        HybridClaimTypes.EmailVerified,
                        HybridClaimTypes.PhoneNumberVerified,
                        HybridClaimTypes.PostalCode,
                        HybridClaimTypes.Role,
                        HybridClaimTypes.UserId,
                        HybridClaimTypes.UserName,
                        HybridClaimTypes.TrueName,
                        HybridClaimTypes.IdCard,
                        HybridClaimTypes.IdCardVerified,
                        HybridClaimTypes.Gender,
                        HybridClaimTypes.NickName,
                        HybridClaimTypes.PhoneNumber,
                        HybridClaimTypes.Avatar,
                        HybridClaimTypes.Email,
                        //HybridClaimTypes.TenantId
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

                    AllowedScopes = { HybridConstants.LocalApi.ScopeName }
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
                        HybridConstants.LocalApi.ScopeName,
                        IdentityResourcesExtensions.HybridCustomName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                // mobile app client, password grant
                new Client
                {
                    ClientId = "mobileAppClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("mobile app secrect".Sha256())
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        HybridConstants.LocalApi.ScopeName,
                        IdentityResourcesExtensions.HybridCustomName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                //swagger client
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
                    AllowedScopes = { HybridConstants.LocalApi.ScopeName }//指定客户端请求的api作用域。 如果为空，则客户端无法访问
                },
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { HybridConstants.LocalApi.ScopeName }
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
                    AllowedScopes = {
                        HybridConstants.LocalApi.ScopeName,
                        IdentityResourcesExtensions.HybridCustomName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                //// SPA client using code flow + pkce
                //new Client
                //{
                //    ClientId = "spa",
                //    ClientName = "SPA Client",
                //    ClientUri = "http://identityserver.io",

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,

                //    RedirectUris =
                //    {
                //        "http://localhost:5002/index.html",
                //        "http://localhost:5002/callback.html",
                //        "http://localhost:5002/silent.html",
                //        "http://localhost:5002/popup.html",
                //    },

                //    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                //    AllowedCorsOrigins = { "http://localhost:5002" },

                //    AllowedScopes = { "openid", "profile", HybridConsts.LocalApi.ScopeName }
                //}
            };
    }
}