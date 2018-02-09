using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Commons.Reflection.Variable;
using Commons.Utils;

namespace Commons.Reflection
{
    public static class ReflectionUtils
    {
        public static IEnumerable<IVariable> GetVariables(object obj, BindingFlags fieldsFlags,
            BindingFlags propertiesFlags, IVariableFilter filter)
        {
            Type type = obj.GetType();

            IEnumerable<IVariable> fields =
                type.GetFields(fieldsFlags)
                    .Where(fi => filter == null || filter.AcceptField(fi))
                    .Select(fi => new FieldVariable(fi) as IVariable);

            IEnumerable<IVariable> properties =
                type.GetProperties(propertiesFlags)
                    .Where(po => filter == null || filter.AcceptProperty(po))
                    .Select(po => new PropertyVariable(po) as IVariable);

            return fields.Concat(properties);
        }

        public static string PublicVariablesToString(object obj, bool appendNewLine = true)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            return PublicVariablesToString(obj, flags, flags | BindingFlags.DeclaredOnly, null, appendNewLine);
        }

        public static string PublicVariablesToString(object obj, BindingFlags fieldsFlags, BindingFlags propertiesFlags,
            IVariableFilter filter, bool appendNewLine = true)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<IVariable> variables = GetVariables(obj, fieldsFlags, propertiesFlags, filter);

            foreach (IVariable var in variables)
            {
                sb.Append(var.GetName()).Append(": ");

                object value = var.GetValue(obj);

                if (var.IsFloat())
                    sb.Append(((float)value).ToString("f2"));
                else
                {
                    IEnumerable enumerable = value as IEnumerable;

                    if (enumerable == null)
                        sb.Append(value);
                    else
                    {
                        StringBuilder subsb = new StringBuilder();

                        foreach (var item in enumerable)
                            subsb.Append(item).Append(",");

                        if (subsb.Length == 0)
                            sb.Append(U.NULL);
                        else
                        {
                            subsb.Remove(subsb.Length - 1, 1);
                            sb.Append(subsb);
                        }
                    }
                }

                if (appendNewLine)
                    sb.AppendLine();
                else
                    sb.Append(". ");
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }
    }
}