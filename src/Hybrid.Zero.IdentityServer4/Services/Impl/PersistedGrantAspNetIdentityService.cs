using Hybrid.Collections;
using Hybrid.Entity;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Identity.Entities;
using Hybrid.Mapping;
using Hybrid.Zero.IdentityServer4.Services.Dtos;

using IdentityServer4.Models;
using IdentityServer4.Stores;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Services.Impl
{
    /// <summary>
    /// Default persisted grant service
    /// </summary>
    public class PersistedGrantAspNetIdentityService<TUser, TUserKey> : IPersistedGrantAspNetIdentityService
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly ILogger _logger;
        private readonly IPersistedGrantStore _store;
        //private readonly IPersistentGrantSerializer _serializer;
        private readonly IRepository<TUser, TUserKey> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedGrantAspNetIdentityService<TUser, TUserKey>"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="logger">The logger.</param>
        public PersistedGrantAspNetIdentityService(IPersistedGrantStore store,
            //IPersistentGrantSerializer serializer,
            IRepository<TUser, TUserKey> userRepository,
            ILogger<PersistedGrantAspNetIdentityService<TUser, TUserKey>> logger)
        {
            _store = store;
            _userRepository = userRepository;
            //_serializer = serializer;
            _logger = logger;
        }

        public async Task DeletePersistedGrantAsync(string key)
        {
            await _store.RemoveAsync(key);
        }

        public async Task DeletePersistedGrantsAsync(string userId)
        {
            var list = await _store.GetAllAsync(new PersistedGrantFilter { SessionId = userId });
            var clientIds = list.Select(p=>p.ClientId);
            foreach (var clientId in clientIds)
            {
                await _store.RemoveAllAsync(new PersistedGrantFilter { ClientId = clientId, SessionId = userId });
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
                return entity.MapTo<PersistedGrantDto>();
            }
        }

        public async Task<PersistedGrantsDto> GetPersistedGrantsByUserAsync(string subjectId, int page = 1, int pageSize = 10)
        {
            PersistedGrantsDto persistedGrantsDto = new PersistedGrantsDto() { SubjectId=subjectId, PageSize = pageSize };
            var list = await _store.GetAllAsync(new PersistedGrantFilter { SubjectId = subjectId});
            persistedGrantsDto.TotalCount = list.Count();
            persistedGrantsDto.PersistedGrants = list.Select(p => p.MapTo<PersistedGrantDto>()).ToList();
            return await Task.FromResult(persistedGrantsDto);
        }

        public async Task<PersistedGrantsDto> GetPersistedGrantsByUsersAsync(string search, int page = 1, int pageSize = 10)
        {
            PersistedGrantsDto persistedGrantsDto = new PersistedGrantsDto() { SubjectId = search, PageSize = pageSize };
            var list = await _store.GetAllAsync(new PersistedGrantFilter { SubjectId = search});

            var persistedGrantByUsers = (from pe in list
                                         join us in _userRepository.QueryAsNoTracking() on pe.SubjectId equals us.Id.ToString() into per
                                         from identity in per.DefaultIfEmpty()
                                         select new
                                         {
                                             SubjectId = pe.SubjectId,
                                             SubjectName = identity == null ? string.Empty : identity.UserName
                                         }).GroupBy(x => x.SubjectId).Select(g => g.First());


            var items = persistedGrantByUsers.WhereIf(x => x.SubjectId.Contains(search) || x.SubjectName.Contains(search), !search.IsNullOrWhiteSpace())
                .Select(p=>new PersistedGrant { 
                         SubjectId = p.SubjectId,
                });

            persistedGrantsDto.TotalCount = items.Count();
            persistedGrantsDto.PersistedGrants = items.Select(p => p.MapTo<PersistedGrantDto>()).ToList();
            return await Task.FromResult(persistedGrantsDto);
        }
    }
}
