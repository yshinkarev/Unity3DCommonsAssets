using System;
using System.Collections;
using System.Reflection;

namespace Commons.Reflection.Variable
{
    public abstract class SimpleVariable<T> : IVariable where T : MemberInfo
    {
        protected readonly T Member;
        private readonly Type _varType;

        protected SimpleVariable(T member)
        {
            Member = member;
            _varType = GetVarType(member);
        }

        public abstract void SetValue(object obj, object value);
        public abstract object GetValue(object obj);

        public Type GetVariableType()
        {
            return _varType;
        }

        public string GetName()
        {
            return Member.Name;
        }

        public object[] GetCustomAttributes<TAttr>(bool inherit) where TAttr : Attribute
        {
            return Member.GetCustomAttributes(typeof(TAttr), inherit);
        }

        public bool IsInt()
        {
            return _varType == typeof(Int16) || _varType == typeof(Int32);
        }

        public bool IsFloat()
        {
            return _varType == typeof(Single) || _varType == typeof(Double);
        }

        public bool IsBool()
        {
            return _varType == typeof(Boolean);
        }

        public bool IsEnumerable()
        {
            return _varType == typeof(IEnumerable);
        }

        protected abstract Type GetVarType(T member);

        public override string ToString()
        {
            return string.Format("{0}:{1}", GetName(), _varType);
        }
    }
}