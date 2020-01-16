using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Views.Shared.Components.TopBarSchedulerSwitch
{
    public class TopBarSchedulerSwitchViewComponent : QuartzViewComponent
    {
        private readonly ISchedulerManager _schedulerManager;

        public TopBarSchedulerSwitchViewComponent(ISchedulerManager schedulerManager)
        {
            _schedulerManager = schedulerManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TopBarSchedulerSwitchViewModel
            {
                CurrentSchedulerHeader = await _schedulerManager.GetCurrentScheduler(LocalSchedulerName),
                SchedulerHeaders = await _schedulerManager.GetSchedulers()
            };

            return View(model);
        }
    }
}