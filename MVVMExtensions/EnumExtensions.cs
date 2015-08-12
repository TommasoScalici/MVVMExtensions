using System;
using System.Reflection;

namespace TommasoScalici.MVVMExtensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(Enum @enum)
        {
            var type = @enum.GetType();
            var fieldInfo = type.GetRuntimeField(@enum.ToString());
            var stringValue = fieldInfo.GetCustomAttribute<StringValueAttribute>();

            if (stringValue != null)
                return stringValue.StringValue;

            return string.Empty;
        }
    }
}
