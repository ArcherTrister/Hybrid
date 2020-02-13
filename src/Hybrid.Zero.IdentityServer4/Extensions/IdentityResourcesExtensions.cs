using Hybrid.Security.Claims;

using IdentityServer4.Models;

namespace Hybrid.Zero.IdentityServer4.Extensions
{
    /// <summary>
    /// Identity扩展资源
    /// </summary>
    public static class IdentityResourcesExtensions
    {
        /// <summary>
        /// Identity扩展资源名称
        /// </summary>
        public const string HybridCustomName = "HybridCustomIdentity";

        /// <summary>
        /// Identity扩展资源
        /// </summary>
        public class HybridCustomResource : IdentityResource
        {
            public HybridCustomResource()
            {
                Name = HybridCustomName;
                DisplayName = "Custom IdentityResource";
                Description = "Hybrid Custom IdentityResource";
                Emphasize = true;
                UserClaims = new[]
                {
                    HybridClaimTypes.TrueName,
                    HybridClaimTypes.IdCard,
                    HybridClaimTypes.IdCardVerified,
                    HybridClaimTypes.Gender,
                    HybridClaimTypes.NickName,
                    HybridClaimTypes.AvatarUrl,
                    HybridClaimTypes.Email,
                    HybridClaimTypes.PhoneNumber
                    //HybridClaimTypes.TenantId
                };
            }
        }
    }
}