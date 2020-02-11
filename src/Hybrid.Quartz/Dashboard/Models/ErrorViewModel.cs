namespace Hybrid.Quartz.Dashboard.Models
{
    public class QuartzErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}