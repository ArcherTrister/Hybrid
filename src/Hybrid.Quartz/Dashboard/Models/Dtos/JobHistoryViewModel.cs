using Hybrid.AspNetCore.Mvc.Models;

namespace Hybrid.Quartz.Dashboard.Models.Dtos
{
    public class JobHistoryViewModel
    {
        public JobHistoryViewModel(PageResult<JobHistoryEntryDto> entries, string errorMessage)
        {
            HistoryEntries = entries;
            ErrorMessage = errorMessage;
        }

        public PageResult<JobHistoryEntryDto> HistoryEntries { get; }
        public string ErrorMessage { get; }
    }
}