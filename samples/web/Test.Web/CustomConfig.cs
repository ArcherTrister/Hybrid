using System.ComponentModel.DataAnnotations;

namespace Test.Web
{
    public class CustomConfig
    {
        [Required(ErrorMessage = "Custom Setting1 Error Message")]
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
        public string Setting3 { get; set; }
    }
}
