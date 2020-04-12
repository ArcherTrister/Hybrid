using Hybrid.Collections;
using Hybrid.Entity;
using Hybrid.Extensions;

using IdentityServer4.Models;
using IdentityServer4.Stores;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Stores
{
    public class CustomInMemoryPersistedGrantStore : IPersistedGrantStore
    {
        private readonly ConcurrentDictionary<string, PersistedGrant> _repository = new ConcurrentDictionary<string, PersistedGrant>();

        /// <summary>
        /// Gets all grants for a given subject id.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            //var query =
            //    from item in _repository
            //    where item.Value.SubjectId == subjectId
            //    select item.Value;
            //var items = query.ToArray().AsEnumerable();
            //return Task.FromResult(items);
            var items = _repository.WhereIf(p => p.Value.SubjectId.Equals(subjectId), !subjectId.IsNullOrWhiteSpace()).Select(p=>p.Value);
            return await Task.FromResult(items);
        }

        /// <summary>
        /// Gets the grant.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<PersistedGrant> GetAsync(string key)
        {
            PersistedGrant token;
            if (_repository.TryGetValue(key, out token))
            {
                return await Task.FromResult(token);
            }

            return await Task.FromResult<PersistedGrant>(null);
        }

        ///// <summary>
        ///// Gets all grants for a given search.
        ///// </summary>
        ///// <param name="search">The search.</param>
        ///// <returns></returns>
        //public async Task<IEnumerable<PersistedGrant>> GetSearchAllAsync(string search)
        //{
        //    var persistedGrantByUsers = (from pe in _repository
        //                                 join us in userRepository.QueryAsNoTracking() on pe.Value.SubjectId equals us.Id.ToString() into per
        //                                 from identity in per.DefaultIfEmpty()
        //                                 select new
        //                                 {
        //                                     SubjectId = pe.Value.SubjectId,
        //                                     SubjectName = identity == null ? string.Empty : identity.UserName
        //                                 }).GroupBy(x => x.SubjectId).Select(g => g.First());


        //    var items = persistedGrantByUsers.WhereIf(x => x.SubjectId.Contains(search) || x.SubjectName.Contains(search), !search.IsNullOrWhiteSpace()).Select(p => p.MapTo<PersistedGrant>());
        //    return await Task.FromResult(items);
        //}

        /// <summary>
        /// Removes all grants for a given subject id and client id combination.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            var query =
                from item in _repository
                where item.Value.ClientId == clientId &&
                    item.Value.SubjectId == subjectId
                select item.Key;

            var keys = query.ToArray();
            foreach (var key in keys)
            {
                _repository.TryRemove(key, out _);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Removes all grants of a give type for a given subject id and client id combination.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            var query =
                from item in _repository
                where item.Value.SubjectId == subjectId &&
                    item.Value.ClientId == clientId &&
                    item.Value.Type == type
                select item.Key;

            var keys = query.ToArray();
            foreach (var key in keys)
            {
                _repository.TryRemove(key, out _);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Removes the grant by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            _repository.TryRemove(key, out _);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Stores the grant.
        /// </summary>
        /// <param name="grant">The grant.</param>
        /// <returns></returns>
        public async Task StoreAsync(PersistedGrant grant)
        {
            _repository[grant.Key] = grant;

            await Task.CompletedTask;
        }
    }
}
