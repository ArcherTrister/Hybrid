namespace Hybrid.AspNetCore.Mvc.Models
{
    public class HybridErrorViewModel
    {
        public HybridErrorViewModel()
        {
        }

        public HybridErrorViewModel(string error)
        {
            Error = new ErrorInfos { Error = error };
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorInfos Error { get; set; }
    }
}