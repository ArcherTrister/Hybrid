using System.Collections.Generic;

namespace Hybrid.Zero.IdentityServer4.Services.Dtos
{
    public class ClientsDto
    {
        public ClientsDto()
        {
            Clients = new List<ClientDto>();
        }

        public List<ClientDto> Clients { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}