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

        [TestMethod]
        public void TestGuidGen()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }
        }

        [TestMethod]
        public void TestNull()
        {
            decimal? test = null;
            test = 0.000m;
            Assert.IsTrue((test ?? 0) == 0);
        }
    }
}