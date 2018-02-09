using System;

namespace Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RangeFloatAttribute : Attribute
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public RangeFloatAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}