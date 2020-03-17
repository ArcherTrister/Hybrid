using Hybrid.Domain.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace Conmon.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var isTrue = (false && false);
            Console.WriteLine(isTrue);
            isTrue = (true && true);
            Console.WriteLine(isTrue);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var A = typeof(ISoftDelete);
            var B = A.GetProperties()[0];
            Console.WriteLine(nameof(ISoftDelete));
        }

        [TestMethod]
        public void TestThreadPriority()
        {
            Console.WriteLine(System.Threading.ThreadPriority.Normal);
        }
    }
}