using Hybrid.Finders;
using Hybrid.Reflection;

using Quartz;

using System;
using System.Linq;

namespace Hybrid.Quartz
{
    /// <summary>
    /// 作业处理器类型查找器
    /// </summary>
    public class JobTypeFinder : FinderBase<Type>, IJobTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="JobTypeFinder"/>类型的新实例
        /// </summary>
        public JobTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type baseType = typeof(IJob);
            return _allAssemblyFinder.FindAll(true).SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom(baseType)).Distinct().ToArray();
        }
    }
}