using IdentityServer4.Models;
using IdentityServer4.Stores;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Stores
{
    public interface ICustomPersistedGrantStore: IPersistedGrantStore
    {
        Task<IEnumerable<PersistedGrant>> GetSearchAllAsync(string search);
    }
}