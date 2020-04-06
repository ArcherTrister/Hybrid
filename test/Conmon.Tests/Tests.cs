using Hybrid.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Conmon.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> a = new List<string> { "1", "2", "3", "4" };
            List<string> b = new List<string> { "1", "2", "3" };
            var c = a.Except(b).ToList();

            var isTrue = (false && false);
            Console.WriteLine(isTrue);
            isTrue = (true && true);
            Console.WriteLine(isTrue);         
        }

        [TestMethod]
        public void TestMethod2()
        {
            var A = typeof(ISoftDeletable);
            var B = A.GetProperties()[0];
            Console.WriteLine(nameof(ISoftDeletable));
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

        [TestMethod]
        public void TestAny()
        {
            List<User> list = new List<User>();
            list.Add(new User { Id = 1 });
            Console.WriteLine(list.Any());
            Assert.IsTrue(list.Any());
        }

        public class User {
            public int Id { get; set; }
        }
    }
}