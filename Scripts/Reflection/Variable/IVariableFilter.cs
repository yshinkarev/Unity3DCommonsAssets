using System.Reflection;

namespace Commons.Reflection.Variable
{
    public interface IVariableFilter
    {
        bool AcceptProperty(PropertyInfo pi);
        bool AcceptField(FieldInfo fi);
    }
}