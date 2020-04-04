using Hybrid.Exceptions;
using Hybrid.Zero.IdentityServer4.Services.Dtos;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Services.Impl
{
    /// <summary>
    /// Default persisted grant service
    /// </summary>
    public class PersistedGrantService : IPersistedGrantService
    {
        private readonly ILogger _logger;
        private readonly IPersistedGrantStore _store;
        //private readonly IPersistentGrantSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedGrantService"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="logger">The logger.</param>
        public PersistedGrantService(IPersistedGrantStore store,
            //IPersistentGrantSerializer serializer,
            ILogger<PersistedGrantService> logger)
        {
            _store = store;
            //_serializer = serializer;
            _logger = logger;
        }

        public async Task DeletePersistedGrantAsync(string key)
        {
            await _store.RemoveAsync(key);
        }

        public async Task DeletePersistedGrantsAsync(string userId)
        {
            var list = await _store.GetAllAsync(userId);
            var clientIds = list.Select(p=>p.ClientId);
            foreach (var cilentId in clientIds)
            {
                await _store.RemoveAllAsync(userId, cilentId);
            }
        }

        public async Task<PersistedGrantDto> GetPersistedGrantAsync(string key)
        {
            var entity = await _store.GetAsync(key);
            if (entity == null)
            {
                throw new HybridException($"Persisted Grant with id {key} doesn't exist");
            }
            else {
                //TODO:ToMap
                //return entity.ToMap();
                return new PersistedGrantDto();
            }
        }

        public Task<PersistedGrantsDto> GetPersistedGrantsByUserAsync(string subjectId, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PersistedGrantsDto> GetPersistedGrantsByUsersAsync(string search, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
