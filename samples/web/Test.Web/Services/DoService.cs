// -----------------------------------------------------------------------
//  <copyright file="DoService" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:37:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Services
{
    public class DoService<T> : IDoService<T>
    {
        public void SayHello()
        {
            Console.WriteLine(typeof(T).ToString());
        }
    }
}
