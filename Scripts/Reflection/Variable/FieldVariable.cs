using System;
using System.Reflection;

namespace Commons.Reflection.Variable
{
    public class FieldVariable : SimpleVariable<FieldInfo>
    {
        public FieldVariable(FieldInfo fi)
            : base(fi)
        {
        }

        public override void SetValue(object obj, object value)
        {
            Member.SetValue(obj, Convert.ChangeType(value, GetVariableType()));
        }

        public override object GetValue(object obj)
        {
            return Member.GetValue(obj);
        }

        protected override Type GetVarType(FieldInfo fi)
        {
            return fi.FieldType;
        }
    }
}