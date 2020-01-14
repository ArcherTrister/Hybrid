using System;

namespace ESoftor.Zero.UI
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
