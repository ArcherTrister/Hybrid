using System;
using System.Collections.Generic;

namespace Hybrid.Quartz
{
    /// <summary>
    /// Represents all the options you can use to configure the system.
    /// </summary>
    public class QuartzOptions
    {
        public QuartzOptions()
        {
            Version = "V1.0.0";
            Extensions = new List<IQuartzOptionsExtension>();
        }

        /// <summary>
        /// The default version of the message, configured to isolate data in the same instance. The length must not exceed 20
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// quartz options extension aggregate
        /// </summary>
        internal IList<IQuartzOptionsExtension> Extensions { get; }

        /// <summary>
        /// Registers an extension that will be executed when building services.
        /// </summary>
        /// <param name="extension"></param>
        public void RegisterExtension(IQuartzOptionsExtension extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            Extensions.Add(extension);
        }
    }
}