using Hybrid.Quartz.Dashboard.Models.Dtos;

using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard.Views.Shared.Components.TopBarSchedulerSwitch
{
    public class TopBarSchedulerSwitchViewModel
    {
        public SchedulerHeaderDto CurrentSchedulerHeader { get; set; }

        public IReadOnlyList<SchedulerHeaderDto> SchedulerHeaders { get; set; }
    }
}