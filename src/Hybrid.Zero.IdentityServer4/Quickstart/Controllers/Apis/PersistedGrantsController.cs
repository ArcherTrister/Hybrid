using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.Zero.IdentityServer4.Quickstart.Helpers;
using Hybrid.Zero.IdentityServer4.Services;
using Hybrid.Zero.IdentityServer4.Services.Dtos;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Controllers.Apis
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PersistedGrantsController : LocalApiController
    {
        private readonly IPersistedGrantAspNetIdentityService _persistedGrantsService;

        public PersistedGrantsController(IPersistedGrantAspNetIdentityService persistedGrantsService)
        {
            _persistedGrantsService = persistedGrantsService;
        }

        [HttpGet("Subjects")]
        public async Task<ActionResult<PersistedGrantsDto>> Get(string searchText, int page = 1, int pageSize = 10)
        {
            var persistedGrantsDto = await _persistedGrantsService.GetPersistedGrantsByUsersAsync(searchText, page < 1 ? 1 : page, pageSize < 0 ? 10 : pageSize);

            return Ok(persistedGrantsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersistedGrantDto>> Get(string id)
        {
            var persistedGrantDto = await _persistedGrantsService.GetPersistedGrantAsync(UrlHelpers.QueryStringUnSafeHash(id));

            ParsePersistedGrantKey(persistedGrantDto);

            return Ok(persistedGrantDto);
        }

        [HttpGet("Subjects/{subjectId}")]
        public async Task<ActionResult<PersistedGrantsDto>> GetBySubject(string subjectId, int page = 1, int pageSize = 10)
        {
            var persistedGrantsDto = await _persistedGrantsService.GetPersistedGrantsByUserAsync(subjectId, page < 1 ? 1 : page, pageSize < 0 ? 10 : pageSize);

            ParsePersistedGrantKeys(persistedGrantsDto);

            return Ok(persistedGrantsDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _persistedGrantsService.DeletePersistedGrantAsync(UrlHelpers.QueryStringUnSafeHash(id));

            return Ok();
        }

        [HttpDelete("Subjects/{subjectId}")]
        public async Task<IActionResult> DeleteBySubject(string subjectId)
        {
            await _persistedGrantsService.DeletePersistedGrantsAsync(subjectId);

            return Ok();
        }

        private void ParsePersistedGrantKey(PersistedGrantDto persistedGrantDto)
        {
            if (!string.IsNullOrEmpty(persistedGrantDto.Key))
            {
                persistedGrantDto.Key = UrlHelpers.QueryStringSafeHash(persistedGrantDto.Key);
            }
        }

        private void ParsePersistedGrantKeys(PersistedGrantsDto persistedGrantApiDto)
        {
            persistedGrantApiDto.PersistedGrants.ForEach(ParsePersistedGrantKey);
        }
    }
}
