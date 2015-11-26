using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Windows.UI.Xaml.Data;

namespace  TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates a <see cref="DateTime"/> or a <see cref="DateTimeOffset"/> to a localized <see cref="string"/>
    /// that can be also formatted passing a string fomat as a parameter. Returns "???" if the conversion fails for any reason.
    /// </summary>
    public sealed class DateTimeToLocalizedStringConverter : IValueConverter
    {
        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string convertedValue;

            if (value is DateTime)
            {
                convertedValue = ((DateTime)value).ToString(parameter as string, CultureInfo.CurrentCulture);

                if ((DateTime)value == default(DateTime))
                    convertedValue = "???";

                return convertedValue;
            }

            if (value is DateTimeOffset)
            {
                convertedValue = ((DateTimeOffset)value).LocalDateTime.ToString(parameter as string, CultureInfo.CurrentCulture);

                if ((DateTimeOffset)value == default(DateTimeOffset))
                    convertedValue = "???";

                return convertedValue;
            }

            return "???";
        }

        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DateTime.Parse(value.ToString(), DateTimeFormatInfo.CurrentInfo);
        }
    }
}
