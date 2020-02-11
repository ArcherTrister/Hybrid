using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Validation;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// custom token validator
    /// </summary>
    public class CustomTokenValidator : ICustomTokenValidator
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// The referenceTokenStore.
        /// </summary>
        protected readonly IReferenceTokenStore Store;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTokenValidator" /> class.
        /// </summary>
        /// <param name="store">The referenceToken store.</param>
        /// <param name="logger">The logger.</param>
        public CustomTokenValidator(IReferenceTokenStore store, ILogger<CustomTokenValidator> logger)
        {
            Store = store;
            Logger = logger;
        }

        /// <summary>
        /// Custom validation logic for access tokens.
        /// </summary>
        /// <param name="result">The validation result so far.</param>
        /// <returns>
        /// The validation result
        /// </returns>
        public async Task<TokenValidationResult> ValidateAccessTokenAsync(TokenValidationResult result)
        {
            if (!string.IsNullOrWhiteSpace(result.Jwt) && !result.IsError)
            {
                Token token = await Store.GetReferenceTokenAsync(result.Jwt);
                if (token == null)
                {
                    result.IsError = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Custom validation logic for identity tokens.
        /// </summary>
        /// <param name="result">The validation result so far.</param>
        /// <returns>
        /// The validation result
        /// </returns>
        public virtual Task<TokenValidationResult> ValidateIdentityTokenAsync(TokenValidationResult result)
        {
            return Task.FromResult(result);
        }
    }
}