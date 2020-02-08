using IdentityServer4.Models;
using IdentityServer4.Services;
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
        /// The user service
        /// </summary>
        protected readonly IProfileService Profile;

        /// <summary>
        /// The client store
        /// </summary>
        protected readonly IClientStore Clients;

        /// <summary>
        /// Gets the reference token store.
        /// </summary>
        /// <value>
        /// The reference token store.
        /// </value>
        protected readonly IReferenceTokenStore ReferenceTokenStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTokenValidator" /> class.
        /// </summary>
        /// <param name="referenceTokenStore">The reference token store.</param>
        /// <param name="logger">The logger.</param>
        public CustomTokenValidator(IReferenceTokenStore referenceTokenStore, ILogger<CustomTokenValidator> logger)
        {
            ReferenceTokenStore = referenceTokenStore;
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
            Token token = await ReferenceTokenStore.GetReferenceTokenAsync(result.ReferenceTokenId);
            //if (token == null)
            //{
            //    result.IsError = true;
            //}
            await Task.CompletedTask;
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