using System;
using System.Reflection;
using Commons.Reflection.Variable;
using UnityEngine;

namespace Commons.Debugging.SettingsEditor.Filter
{
    class DebugSettingsFilter : IVariableFilter
    {
        public bool AcceptProperty(PropertyInfo pi)
        {
            return AcceptMember(pi, pi.PropertyType);
        }

        public bool AcceptField(FieldInfo fi)
        {
            return AcceptMember(fi, fi.FieldType);
        }

        protected virtual bool AcceptMember(MemberInfo mi, Type memberType)
        {
            if (memberType == typeof(GameObject) || memberType == typeof(string) || memberType == typeof(Color))
                return false;

            return mi.GetCustomAttributes(typeof(HideInInspector), true).Length == 0;
        }
    }
}