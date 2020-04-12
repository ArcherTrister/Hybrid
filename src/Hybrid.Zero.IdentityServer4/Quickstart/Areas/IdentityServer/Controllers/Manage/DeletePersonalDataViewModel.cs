using System.ComponentModel.DataAnnotations;

namespace Hybrid.Zero.IdentityServer4.Quickstart.Areas.IdentityServer
{
    public class DeletePersonalDataViewModel
    {
        public bool RequirePassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}