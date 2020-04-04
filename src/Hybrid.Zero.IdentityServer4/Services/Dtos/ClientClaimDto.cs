using System.ComponentModel.DataAnnotations;

namespace Hybrid.Zero.IdentityServer4.Services.Dtos
{
	public class ClientClaimDto
	{
		public int Id { get; set; }

		[Required]
		public string Type { get; set; }

		[Required]
		public string Value { get; set; }
	}
}
