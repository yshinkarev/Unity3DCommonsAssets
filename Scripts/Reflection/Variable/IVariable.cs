using System;

namespace Commons.Reflection.Variable
{
    public interface IVariable
    {
        void SetValue(object obj, object value);
        object GetValue(object obj);

        Type GetVariableType();
        string GetName();

        object[] GetCustomAttributes<T>(bool inherit) where T : Attribute;

        bool IsInt();
        bool IsFloat();
        bool IsBool();
    }
}
