using Hybrid.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
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
    }
}
