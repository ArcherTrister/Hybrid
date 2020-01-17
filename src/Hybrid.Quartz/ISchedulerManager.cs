using Hybrid.Quartz.Models.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hybrid.Quartz
{
    public interface ISchedulerManager
    {
        Task<SchedulerHeaderDto> GetCurrentScheduler(string schedulerName);

        Task<IReadOnlyList<SchedulerHeaderDto>> GetSchedulers();
    }
}