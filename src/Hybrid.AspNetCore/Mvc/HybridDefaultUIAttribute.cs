using System;

namespace Hybrid.AspNetCore.Mvc
{
    internal class HybridDefaultUIAttribute : Attribute
    {
        public HybridDefaultUIAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }

        public Type Template { get; }
    }
}
