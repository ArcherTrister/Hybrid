using Quartz;
using Quartz.Util;

namespace Hybrid.Quartz.Dashboard.Models.Dtos
{
    public class SchedulerJobStoreDto
    {
        public SchedulerJobStoreDto(SchedulerMetaData metaData)
        {
            Type = metaData.JobStoreType.AssemblyQualifiedNameWithoutVersion();
            Clustered = metaData.JobStoreClustered;
            Persistent = metaData.JobStoreSupportsPersistence;
        }

        public string Type { get; private set; }
        public bool Clustered { get; private set; }
        public bool Persistent { get; private set; }
    }
}