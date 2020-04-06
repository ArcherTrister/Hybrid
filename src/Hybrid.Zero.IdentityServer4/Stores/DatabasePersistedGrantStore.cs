using Hybrid.Collections;
using Hybrid.Entity;
using Hybrid.Extensions;
using Hybrid.Mapping;

using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Stores;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Stores
{
    public class DatabasePersistedGrantStore : IPersistedGrantStore
    {
        private readonly IPersistedGrantDbContext persistedGrantDbContext;

        public DatabasePersistedGrantStore(IServiceProvider serviceProvider)
        {
            persistedGrantDbContext = serviceProvider.GetRequiredService<IPersistedGrantDbContext>();
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var items = persistedGrantDbContext.PersistedGrants.WhereIf(!subjectId.IsNullOrWhiteSpace(), p => p.SubjectId.Equals(subjectId)).Select(p => p.MapTo<PersistedGrant>()).AsEnumerable();
            return await Task.FromResult(items);
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            return await persistedGrantDbContext.PersistedGrants.Where(p => p.Key.Equals(key)).Select(p => p.MapTo<PersistedGrant>()).FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<PersistedGrant>> GetSearchAllAsync(string search)
        //{
        //    var persistedGrantByUsers = (from pe in persistedGrantDbContext.PersistedGrants.AsQueryable()
        //                                 join us in userRepository.QueryAsNoTracking() on pe.SubjectId equals us.Id.ToString() into per
        //                                 from identity in per.DefaultIfEmpty()
        //                                 select new
        //                                 {
        //                                     SubjectId = pe.SubjectId,
        //                                     SubjectName = identity == null ? string.Empty : identity.UserName
        //                                 }).GroupBy(x => x.SubjectId).Select(g => g.First());


        //    var items = persistedGrantByUsers.WhereIf(!search.IsNullOrWhiteSpace(), x => x.SubjectId.Contains(search) || x.SubjectName.Contains(search)).Select(p=>p.MapTo<PersistedGrant>()).AsEnumerable();
        //    return await Task.FromResult(items);
        //}

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            var items = persistedGrantDbContext.PersistedGrants.Where(p => p.SubjectId.Equals(subjectId) && p.ClientId.Equals(clientId)).AsEnumerable();
            persistedGrantDbContext.PersistedGrants.RemoveRange(items);
            await persistedGrantDbContext.SaveChangesAsync();
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            var items = persistedGrantDbContext.PersistedGrants.Where(p => p.SubjectId.Equals(subjectId) && p.ClientId.Equals(clientId) && p.Type.Equals(type)).AsEnumerable();
            persistedGrantDbContext.PersistedGrants.RemoveRange(items);
            await persistedGrantDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(string key)
        {
            var item = await persistedGrantDbContext.PersistedGrants.FirstOrDefaultAsync(p => p.Key.Equals(key));
            persistedGrantDbContext.PersistedGrants.Remove(item);
            await persistedGrantDbContext.SaveChangesAsync();
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            await persistedGrantDbContext.PersistedGrants.AddAsync(grant.MapTo<global::IdentityServer4.EntityFramework.Entities.PersistedGrant>());
            await persistedGrantDbContext.SaveChangesAsync();
        }
    }
    //public class DatabasePersistedGrantStore<TUser, TUserKey> : IPersistedGrantStore
    //    where  TUser:UserBase<TUserKey>
    //    where TUserKey : IEquatable<TUserKey>
    //{
    //    private readonly IPersistedGrantDbContext persistedGrantDbContext;
    //    private readonly IRepository<TUser, TUserKey> userRepository;

    //    public DatabasePersistedGrantStore(IServiceProvider serviceProvider)
    //    {
    //        persistedGrantDbContext = serviceProvider.GetRequiredService<IPersistedGrantDbContext>();
    //        userRepository = serviceProvider.GetRequiredService<IRepository<TUser, TUserKey>>();
    //    }

    //    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
    //    {
    //        var items = persistedGrantDbContext.PersistedGrants.WhereIf(!subjectId.IsNullOrWhiteSpace(), p => p.SubjectId.Equals(subjectId)).Select(p=>p.MapTo<PersistedGrant>()).AsEnumerable();
    //        return await Task.FromResult(items);
    //    }

    //    public async Task<PersistedGrant> GetAsync(string key)
    //    {
    //       return await persistedGrantDbContext.PersistedGrants.Where(p => p.Key.Equals(key)).Select(p => p.MapTo<PersistedGrant>()).FirstOrDefaultAsync();
    //    }

    //    //public async Task<IEnumerable<PersistedGrant>> GetSearchAllAsync(string search)
    //    //{
    //    //    var persistedGrantByUsers = (from pe in persistedGrantDbContext.PersistedGrants.AsQueryable()
    //    //                                 join us in userRepository.QueryAsNoTracking() on pe.SubjectId equals us.Id.ToString() into per
    //    //                                 from identity in per.DefaultIfEmpty()
    //    //                                 select new
    //    //                                 {
    //    //                                     SubjectId = pe.SubjectId,
    //    //                                     SubjectName = identity == null ? string.Empty : identity.UserName
    //    //                                 }).GroupBy(x => x.SubjectId).Select(g => g.First());


    //    //    var items = persistedGrantByUsers.WhereIf(!search.IsNullOrWhiteSpace(), x => x.SubjectId.Contains(search) || x.SubjectName.Contains(search)).Select(p=>p.MapTo<PersistedGrant>()).AsEnumerable();
    //    //    return await Task.FromResult(items);
    //    //}

    //    public async Task RemoveAllAsync(string subjectId, string clientId)
    //    {
    //        var items = persistedGrantDbContext.PersistedGrants.Where(p => p.SubjectId.Equals(subjectId) && p.ClientId.Equals(clientId)).AsEnumerable();
    //        persistedGrantDbContext.PersistedGrants.RemoveRange(items);
    //        await persistedGrantDbContext.SaveChangesAsync();
    //    }

    //    public async Task RemoveAllAsync(string subjectId, string clientId, string type)
    //    {
    //        var items = persistedGrantDbContext.PersistedGrants.Where(p => p.SubjectId.Equals(subjectId) && p.ClientId.Equals(clientId)&&p.Type.Equals(type)).AsEnumerable();
    //        persistedGrantDbContext.PersistedGrants.RemoveRange(items);
    //        await persistedGrantDbContext.SaveChangesAsync();
    //    }

    //    public async Task RemoveAsync(string key)
    //    {
    //        var item = await persistedGrantDbContext.PersistedGrants.FirstOrDefaultAsync(p=>p.Key.Equals(key));
    //        persistedGrantDbContext.PersistedGrants.Remove(item);
    //        await persistedGrantDbContext.SaveChangesAsync();
    //    }

    //    public async Task StoreAsync(PersistedGrant grant)
    //    {
    //        await persistedGrantDbContext.PersistedGrants.AddAsync(grant.MapTo<global::IdentityServer4.EntityFramework.Entities.PersistedGrant>());
    //        await persistedGrantDbContext.SaveChangesAsync();
    //    }
    //}
}
