using System;
using System.Diagnostics.CodeAnalysis;

using Windows.UI.Xaml.Data;

namespace  TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates <see cref="bool"/> values to <see cref="double"/>
    /// (<see cref="Windows.UI.Xaml.UIElement"/> opacity).
    /// </summary>
    public sealed class BooleanToOpacityConverter : IValueConverter
    {
        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? 1 : 0;

        }

        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToDouble(value) >= 1;
        }
    }
}
