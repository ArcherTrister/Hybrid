using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.Application.Services.Dtos
{
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        int TotalCount { get; set; }
    }
}
