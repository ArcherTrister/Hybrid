using System;

namespace Hybrid.AspNetCore.WebApi.Dynamic.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class NoneDynamicWebApiAttribute : Attribute
    {
    }
}