using System;
using System.Reflection;

namespace TommasoScalici.MVVMExtensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo fi = type.GetRuntimeField(@enum.ToString());
            StringValueAttribute stringValue = fi.GetCustomAttribute<StringValueAttribute>();

            if (stringValue != null)
                return stringValue.StringValue;

            return string.Empty;
        }
    }
}
