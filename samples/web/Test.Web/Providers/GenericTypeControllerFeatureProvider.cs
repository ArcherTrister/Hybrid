//using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using Microsoft.AspNetCore.Mvc.Controllers;

//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//using WebApplication123.Controllers;

//namespace DynamicAndGenericControllersSample
//{
//    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
//    {
//        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
//        {
//            var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
//            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

//            foreach (var candidate in candidates)
//            {
//                feature.Controllers.Add(typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo());
//            }
//        }
//    }
//}