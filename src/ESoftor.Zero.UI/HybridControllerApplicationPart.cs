using Microsoft.AspNetCore.Mvc.ApplicationParts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESoftor.Zero.UI
{
    internal class HybridControllerApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public IEnumerable<TypeInfo> Types { get; }

        public override string Name => "GenericController";

        public HybridControllerApplicationPart(IEnumerable<TypeInfo> typeInfos, Type entityType)
        {
            Types = typeInfos.Select(p => p.GetCustomAttribute<HybridDefaultUIAttribute>())
                .Select(m => m.Template.MakeGenericType(entityType))
                .Select(t => t.GetTypeInfo())
                .ToArray();
        }
    }
}
