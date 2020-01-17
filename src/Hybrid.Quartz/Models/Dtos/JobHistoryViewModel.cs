using Hybrid.Application.Services.Dtos;

namespace Hybrid.Quartz.Models.Dtos
{
    public class JobHistoryViewModel
    {
        public JobHistoryViewModel(PagedResultDto<JobHistoryEntryDto> entries, string errorMessage)
        {
            HistoryEntries = entries;
            ErrorMessage = errorMessage;
        }

        public PagedResultDto<JobHistoryEntryDto> HistoryEntries { get; }
        public string ErrorMessage { get; }
    }
}