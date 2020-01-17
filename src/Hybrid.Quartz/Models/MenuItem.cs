using System;

namespace Hybrid.Quartz.Models
{
    public class MenuItem
    {
        public MenuItem(string areaName, string controllerName, string actionName)
        {
            AreaName = areaName;
            ControllerName = controllerName;
            ActionName = actionName;
            actionName = string.IsNullOrWhiteSpace(actionName) ? "" :
                actionName.Equals("Index", StringComparison.OrdinalIgnoreCase) ? "" : "/" + actionName;
            Url = $"/{areaName}/{controllerName}{actionName}";
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string AreaName { get; }

        public string ControllerName { get; }

        public string ActionName { get; }

        public string Url { get; }

        public string Metrics { get; set; } = "0";

        public string ClassName { get; set; } = "";
    }
}