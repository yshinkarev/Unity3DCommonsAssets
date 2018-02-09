using System;

namespace Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class IgnoreToSave : Attribute
    {
    }
}