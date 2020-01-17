using System.Collections.Generic;

namespace Hybrid.Quartz.Models
{
    public sealed class NavBars
    {
        public string Title { get; set; }

        public List<MenuItem> Routers { get; set; }
    }
}