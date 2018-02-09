using System;
using System.Linq;

namespace Commons.Utils
{
    public static class TypesUtils
    {
        public static string GetSimpleClassName(System.Object obj)
        {
            if (obj == null)
                return U.NULL;
            else
                return obj.GetType().Name;
        }

        public static bool IsInstanceOf(System.Object obj, params Type[] types)
        {
            Type typeOfObj = obj.GetType();
            return types.FirstOrDefault(t => t == typeOfObj) != null;
        }
    }
}