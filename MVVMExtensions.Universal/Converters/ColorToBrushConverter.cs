﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;


namespace TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates an hexadecimal <see cref="string"/> or a <see cref="Color"/> to a <see cref="SolidColorBrush"/>.
    /// </summary>
    [SuppressMessage("Warning", "CS0419")]
    public sealed class ColorToBrushConverter : IValueConverter
    {
        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            if (value is string)
                return new SolidColorBrush(Parse(value.ToString()));

            if (value is Color)
                return new SolidColorBrush((Color)value);

            throw new NotSupportedException("ColorToBurshConverter only supports converting from Color and String.");
        }

        [SuppressMessage("Message", "RECS0083")]
        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        [SuppressMessage("Warning", "CS1591")]
        static Color Parse(string color)
        {
            var offset = color.StartsWith("#", StringComparison.Ordinal) ? 1 : 0;

            var a = Byte.Parse(color.Substring(0 + offset, 2), NumberStyles.HexNumber);
            var r = Byte.Parse(color.Substring(2 + offset, 2), NumberStyles.HexNumber);
            var g = Byte.Parse(color.Substring(4 + offset, 2), NumberStyles.HexNumber);
            var b = Byte.Parse(color.Substring(6 + offset, 2), NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
