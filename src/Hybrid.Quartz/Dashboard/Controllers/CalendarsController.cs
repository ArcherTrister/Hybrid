using Hybrid.Quartz.Dashboard.Models.Dtos;

using Microsoft.AspNetCore.Mvc;

using Quartz;

using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 日历
    /// </summary>
    public sealed class CalendarsController : QuartzBaseController
    {
        public IActionResult Index(string sortOrder, string searchString, int page = 1, int pageSize = 5)
        {
            return View();
        }

        public async Task<IActionResult> Details(string schedulerName, string calendarName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            ICalendar calendar = await scheduler.GetCalendar(calendarName).ConfigureAwait(false);

            return View(CalendarDetailDto.Create(calendar, calendarName));
        }
    }
}