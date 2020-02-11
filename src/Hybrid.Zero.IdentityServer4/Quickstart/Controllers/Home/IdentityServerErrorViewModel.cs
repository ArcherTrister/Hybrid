// -----------------------------------------------------------------------
//  <copyright file="ErrorViewModel.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using IdentityServer4.Models;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    public class IdentityServerErrorViewModel
    {
        public IdentityServerErrorViewModel()
        {
        }

        public IdentityServerErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }
}