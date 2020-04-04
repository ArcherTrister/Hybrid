using Hybrid.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

using Quartz;

using System;
using System.Threading.Tasks;

namespace LeXun.Demo.Web.Controllers
{
    public class JobTestController : MvcController
    {
        internal class SimpleJob : IJob
        {
            public virtual Task Execute(IJobExecutionContext context)
            {
                //this.languageConfiguration.GetString("", Language.SimplifiedChinese);
                JobKey jobKey = context.JobDetail.Key;
                Console.WriteLine($"SimpleJob says: {jobKey} executing at {DateTime.Now:r}");
                Task.Delay(20000);
                return Task.CompletedTask;
            }
        }

        private readonly IScheduler _sched;

        public JobTestController(IScheduler sched)
        {
            _sched = sched;
        }

        public async Task<IActionResult> Index()
        {
            var j1 = new JobKey("job1");
            if (!await _sched.CheckExists(j1))
            {
                IJobDetail job1 = JobBuilder.Create<SimpleJob>().WithIdentity("job1").Build();
                ITrigger trigger1 = TriggerBuilder.Create().WithIdentity("trigger1").WithCronSchedule("*/10 * * * * ?").Build();

                _sched.ScheduleJob(job1, trigger1).Wait();
            }

            var a1 = await _sched.GetCurrentlyExecutingJobs();

            await _sched.Interrupt(j1);

            //var a2 = await _sched.GetCurrentlyExecutingJobs();

            return View();
        }
    }
}
