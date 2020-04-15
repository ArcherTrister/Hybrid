using Hybrid.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Conmon.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod4()
        {
            int? a = 1;
            int? b = null;
            int aa = 1;
            int bb = 2;
            var result1 = a + b;
            var result2 = a + bb;
            var result3 = aa + b;
            var result4 = aa + bb;


            var allLayer = a ?? 0 + b ?? 0;


            Console.WriteLine(a + b);
            Console.WriteLine(aa + bb);
            Console.WriteLine(aa + b);
            Console.WriteLine(a + bb);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string value = null;
            string temp = value.Equals("true", StringComparison.OrdinalIgnoreCase) ? "√" : value.Equals("true", StringComparison.OrdinalIgnoreCase) ? "×" : value;
            Console.WriteLine(temp);
        }

        //
        [TestMethod]
        public void TestReflex()
        {
            Reflex reflex = new Reflex();
            reflex.BZ = "haha";
            reflex.Students.Add(new Student { Id = 1, Name = "s1" });
            reflex.Students.Add(new Student { Id = 2, Name = "s2" });
            reflex.Students.Add(new Student { Id = 3, Name = "s3" });

            //获取所有属性名称和属性类型         
            PropertyInfo[] infos = reflex.GetType().GetProperties();// typeof(T).GetProperties();
            foreach (PropertyInfo item in infos)
            {
                Console.WriteLine(string.Format("PropertyName:{0},type:{1}", item.Name, item.PropertyType.Name));
                object obj = item.GetValue(item.Name, null);
            }

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine(list[i].GetType().GetProperty("Username").GetValue(list[i], null));
            //    Console.WriteLine(list[i].GetType().GetProperty("Password").GetValue(list[i], null));
            //    object obj = list[i].GetType().GetProperty("student").GetValue(list[i], null);
            //    IList ll = obj as IList;

            //    foreach (var item in ll)
            //    {
            //        Console.WriteLine(string.Format("Name:{0},Age:{1}", item.GetType().GetProperty("Name").GetValue(item, null), item.GetType().GetProperty("Age").GetValue(item, null)));
            //    }
            //}


            Console.WriteLine();
        }

        public class Reflex
        {
            public string BZ { get; set; }

            public List<Student> Students { get; set; } = new List<Student>();
        }

        public class Student
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

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