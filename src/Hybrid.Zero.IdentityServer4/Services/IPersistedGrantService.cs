using Hybrid.Zero.IdentityServer4.Services.Dtos;

using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Services
{
    public interface IPersistedGrantService
    {
        Task<PersistedGrantsDto> GetPersistedGrantsByUsersAsync(string search, int page = 1, int pageSize = 10);

        Task<PersistedGrantsDto> GetPersistedGrantsByUserAsync(string subjectId, int page = 1, int pageSize = 10);

        Task<PersistedGrantDto> GetPersistedGrantAsync(string key);

        Task DeletePersistedGrantAsync(string key);

        Task DeletePersistedGrantsAsync(string userId);
    }
}