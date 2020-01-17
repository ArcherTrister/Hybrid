using Quartz;
using Quartz.Util;

namespace Hybrid.Quartz.Models.Dtos
{
    public class SchedulerThreadPoolDto
    {
        public SchedulerThreadPoolDto(SchedulerMetaData metaData)
        {
            Type = metaData.ThreadPoolType.AssemblyQualifiedNameWithoutVersion();
            Size = metaData.ThreadPoolSize;
        }

        public string Type { get; private set; }
        public int Size { get; private set; }
    }
}