using Hybrid.Quartz.Dashboard.Models;
using Hybrid.Quartz.Plugins.History;

using System;
using System.Collections.Generic;
using System.Linq;
using Hybrid.Extensions;

namespace Hybrid.Quartz.Extensions
{
    internal static class HistogramExtension
    {
        public static Histogram ToHistogram(this IEnumerable<ExecutionHistoryEntry> entries, bool detailed = false)
        {
            if (entries == null || entries.Any() == false)
                return null;

            var hst = new Histogram();
            foreach (var entry in entries)
            {
                TimeSpan? duration = null;
                string cssClass = "";
                string state = "Finished";

                if (entry.FinishedTimeUtc != null)
                    duration = entry.FinishedTimeUtc - entry.ActualFireTimeUtc;

                if (entry.Vetoed == false && entry.FinishedTimeUtc == null) // still running
                {
                    duration = DateTime.UtcNow - entry.ActualFireTimeUtc;
                    cssClass = "running";
                    state = "Running";
                }

                if (entry.Vetoed)
                    state = "Vetoed";

                string durationHtml = "", delayHtml = "", errorHtml = "", detailsHtml = "";

                if (!string.IsNullOrEmpty(entry.ErrorMessage))
                {
                    state = "Failed";
                    cssClass = "failed";
                    errorHtml = $"<p class=\"text-left text-capitalize\">Error: <b>{entry.ErrorMessage}</b></p>";
                }

                if (duration != null)
                    durationHtml = $"<p class=\"text-left text-capitalize\">Duration: <b>{duration.ToNiceFormat()}</b></p>";

                if (entry.ScheduledFireTimeUtc != null)
                    delayHtml = $"<p class=\"text-left text-capitalize\">Delay: <b>{(entry.ActualFireTimeUtc - entry.ScheduledFireTimeUtc).ToNiceFormat()}</b></p>";

                if (detailed)
                    detailsHtml = $"<p class=\"text-left text-capitalize\">Job: <b>{entry.JobName}</b></p>" +
                                  $"<p class=\"text-left text-capitalize\">Trigger: <b>{entry.TriggerName}</b></p>";

                hst.AddBar(duration?.TotalSeconds ?? 1,
                    //$"<div class=\"panel panel-default\" style=\"width: 300px;height: 150px;\"><div class=\"panel-body\">" +
                    $"{detailsHtml}" +
                    $"<p class=\"text-left text-capitalize\">Fired: <b>{entry.ActualFireTimeUtc.ToDefaultFormat()} UTC</b></p>" +
                    $"{durationHtml}{delayHtml}" +
                    $"<p class=\"text-left text-capitalize\">State: <b>{state}</b></p>" +
                    $"{errorHtml}",
                    //+$"</div></div>",
                    cssClass);
            }

            return hst;
        }
    }
}