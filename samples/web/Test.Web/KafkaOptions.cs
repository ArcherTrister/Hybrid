using System.ComponentModel.DataAnnotations;

namespace Test.Web
{
    public class KafkaOptions : IEnabled
    {
        [Required]
        public string BootstrapServers { get; set; }

        [Required]
        public string GroupId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}