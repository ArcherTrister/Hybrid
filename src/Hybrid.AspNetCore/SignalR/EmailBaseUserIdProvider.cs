using Hybrid.Security.Claims;

using Microsoft.AspNetCore.SignalR;

namespace Hybrid.AspNetCore.SignalR
{
    /// <summary>
    /// 基于Email的用户标识提供者
    /// </summary>
    public class EmailBaseUserIdProvider : IUserIdProvider
    {
        /// <summary>Gets the user ID for the specified connection.</summary>
        /// <param name="connection">The connection to get the user ID for.</param>
        /// <returns>The user ID for the specified connection.</returns>
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(HybridClaimTypes.Email)?.Value;
        }
    }
}