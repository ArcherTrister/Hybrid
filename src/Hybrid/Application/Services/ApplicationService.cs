using System.Collections.Generic;

namespace Hybrid.Application.Services
{
    public abstract class ApplicationService : IApplicationService, IAvoidDuplicateCrossCuttingConcerns
    {
        /// <summary>
        /// Gets the applied cross cutting concerns.
        /// </summary>
        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();
    }
}
