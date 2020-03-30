// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Hybrid.Extensions;

using IdentityModel;

using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Stores.Serialization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

using static IdentityModel.OidcConstants;

namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// Custom token creation service
    /// </summary>
    public class CustomTokenCreationService : ITokenCreationService
    {
        /// <summary>
        /// The PersistedGrantStore.
        /// </summary>
        protected readonly IPersistedGrantStore Store;

        /// <summary>
        /// The PersistentGrantSerializer;
        /// </summary>
        protected readonly IPersistentGrantSerializer Serializer;

        /// <summary>
        /// The key service
        /// </summary>
        protected readonly IKeyMaterialService Keys;

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        ///  The clock
        /// </summary>
        protected readonly ISystemClock Clock;

        /// <summary>
        /// The options
        /// </summary>
        protected readonly IdentityServerOptions Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTokenCreationService"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="clock">The options.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public CustomTokenCreationService(
            IPersistedGrantStore store,
            IPersistentGrantSerializer serializer,
            ISystemClock clock,
            IKeyMaterialService keys,
            IdentityServerOptions options,
            ILogger<DefaultTokenCreationService> logger)
        {
            Store = store;
            Serializer = serializer;
            Clock = clock;
            Keys = keys;
            Options = options;
            Logger = logger;
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A protected and serialized security token
        /// </returns>
        public virtual async Task<string> CreateTokenAsync(Token token)
        {
            var header = await CreateHeaderAsync(token);
            var payload = await CreatePayloadAsync(token);
            return await CreateJwtAsync(new JwtSecurityToken(header, payload), token);
        }

        /// <summary>
        /// Creates the JWT header
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The JWT header</returns>
        protected virtual async Task<JwtHeader> CreateHeaderAsync(Token token)
        {
            //var credential = await Keys.GetSigningCredentialsAsync(token.AllowedSigningAlgorithms);
            var credential = await Keys.GetSigningCredentialsAsync();
            if (credential == null)
            {
                throw new InvalidOperationException("No signing credential is configured. Can't create JWT token");
            }

            var header = new JwtHeader(credential);

            // emit x5t claim for backwards compatibility with v4 of MS JWT library
            if (credential.Key is X509SecurityKey x509key)
            {
                var cert = x509key.Certificate;
                if (Clock.UtcNow.UtcDateTime > cert.NotAfter)
                {
                    Logger.LogWarning("Certificate {subjectName} has expired on {expiration}", cert.Subject, cert.NotAfter.ToString(CultureInfo.InvariantCulture));
                }

                header["x5t"] = Base64Url.Encode(cert.GetCertHash());
            }

            if (token.Type == TokenTypes.AccessToken)
            {
                if (Options.AccessTokenJwtType.IsPresent())
                {
                    header["typ"] = Options.AccessTokenJwtType;
                }
            }

            return header;
        }

        /// <summary>
        /// Creates the JWT payload
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The JWT payload</returns>
        protected virtual Task<JwtPayload> CreatePayloadAsync(Token token)
        {
            var payload = token.CreateJwtPayload(Clock, Logger);
            return Task.FromResult(payload);
        }

        /// <summary>
        /// Applies the signature to the JWT
        /// </summary>
        /// <param name="jwt">The JWT object.</param>
        /// <param name="token">The token.</param>
        /// <returns>The signed JWT</returns>
        protected virtual async Task<string> CreateJwtAsync(JwtSecurityToken jwt, Token token)
        {
            var handler = new JwtSecurityTokenHandler();
            string accessToken = handler.WriteToken(jwt);
            //return Task.FromResult(handler.WriteToken(jwt));
            await StoreItemAsync(accessToken, token, token.ClientId, token.SubjectId, token.CreationTime, token.Lifetime);
            return accessToken;
        }

        private const string KeySeparator = ":";

        //private const string GrantType = "jwt_token";
        private const string GrantType = IdentityServerConstants.PersistedGrantTypes.ReferenceToken;

        /// <summary>
        /// Gets the hashed key.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected virtual string GetHashedKey(string value)
        {
            return (value + KeySeparator + GrantType).Sha256();
        }

        /// <summary>
        /// Stores the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="subjectId">The subject identifier.</param>
        /// <param name="created">The created.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns></returns>
        protected virtual Task StoreItemAsync(string key, Token item, string clientId, string subjectId, DateTime created, int lifetime)
        {
            return StoreItemAsync(key, item, clientId, subjectId, created, created.AddSeconds(lifetime));
        }

        /// <summary>
        /// Stores the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="subjectId">The subject identifier.</param>
        /// <param name="created">The created.</param>
        /// <param name="expiration">The expiration.</param>
        /// <returns></returns>
        protected virtual async Task StoreItemAsync(string key, Token item, string clientId, string subjectId, DateTime created, DateTime? expiration)
        {
            key = GetHashedKey(key);

            var json = Serializer.Serialize(item);

            var grant = new PersistedGrant
            {
                Key = key,
                Type = GrantType,
                ClientId = clientId,
                SubjectId = subjectId,
                CreationTime = created,
                Expiration = expiration,
                Data = json
            };

            await Store.StoreAsync(grant);
        }
    }
}