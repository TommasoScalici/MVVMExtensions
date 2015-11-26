using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace  TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that look up for the string passed in the App Resources strings and returns its value, if found.
    /// </summary>
    public sealed class ResourceStringConverter : IValueConverter
    {
        static readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();

        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string || value.GetType().GetTypeInfo().IsEnum)
                return resourceLoader.GetString(value.ToString());

            return value;
        }

        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
