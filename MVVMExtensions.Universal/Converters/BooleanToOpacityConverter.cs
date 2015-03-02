using System;

using Windows.UI.Xaml.Data;

namespace MyAnimeList.Converters
{
    /// <summary>
    /// Value converter that translates <see cref="bool"/> values to <see cref="double"/> (<see cref="UIElement"/> opacity).
    /// </summary>
    public sealed class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToDouble(value) >= 1;
        }
    }
}
