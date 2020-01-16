using Hybrid.Quartz.Dashboard.Models.Dtos;

using Microsoft.AspNetCore.Mvc;

using Quartz;

using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    public class TriggersController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string schedulerName, string triggerGroup, string triggerName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            ITrigger trigger = await scheduler.GetTrigger(new TriggerKey(triggerName, triggerGroup)).ConfigureAwait(false);
            ICalendar calendar = trigger.CalendarName != null
                ? await scheduler.GetCalendar(trigger.CalendarName).ConfigureAwait(false)
                : null;

            if (trigger == null)
                return View(null);
            return View(TriggerDetailDto.Create(trigger, calendar));
        }
    }
}