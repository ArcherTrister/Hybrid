// -----------------------------------------------------------------------
//  <copyright file="ApplicationService.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

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