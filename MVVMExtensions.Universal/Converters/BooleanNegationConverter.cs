using System;
using System.Diagnostics.CodeAnalysis;

using Windows.UI.Xaml.Data;

namespace  TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates true to false and vice versa.
    /// </summary>
    public sealed class BooleanNegationConverter : IValueConverter
    {
        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }
    }
}
