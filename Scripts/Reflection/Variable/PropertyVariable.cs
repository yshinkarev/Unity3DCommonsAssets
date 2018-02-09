using System;
using System.Reflection;

namespace Commons.Reflection.Variable
{
    public class PropertyVariable : SimpleVariable<PropertyInfo>
    {
        public PropertyVariable(PropertyInfo pi)
            : base(pi)
        {
        }

        public override void SetValue(object obj, object value)
        {
            Member.SetValue(obj, Convert.ChangeType(value, GetVariableType()), null);
        }

        public override object GetValue(object obj)
        {
            return Member.GetValue(obj, null);
        }

        protected override Type GetVarType(PropertyInfo pi)
        {
            return pi.PropertyType;
        }
    }
}