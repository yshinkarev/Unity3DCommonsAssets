using System;

namespace Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RangeIntAttribute : Attribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public RangeIntAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}