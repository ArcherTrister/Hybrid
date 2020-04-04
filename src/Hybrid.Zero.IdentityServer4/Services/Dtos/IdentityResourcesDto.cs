using System.Collections.Generic;

namespace Hybrid.Zero.IdentityServer4.Services.Dtos
{
    public class IdentityResourcesDto
    {
        public IdentityResourcesDto()
        {
            IdentityResources = new List<IdentityResourceDto>();
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<IdentityResourceDto> IdentityResources { get; set; }
    }
}