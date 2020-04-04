using System.Collections.Generic;

namespace Hybrid.Zero.IdentityServer4.Services.Dtos
{
    public class ApiResourcesDto
    {
        public ApiResourcesDto()
        {
            ApiResources = new List<ApiResourceDto>();
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<ApiResourceDto> ApiResources { get; set; }
    }
}