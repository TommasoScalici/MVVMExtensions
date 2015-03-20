using System;

using Windows.UI.Xaml.Data;

namespace  TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates a null object to false and not null object to true.
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Apply NOT operand to the result of the converter.
        /// </summary>
        public bool Inverted { get; set; }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null || Inverted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
