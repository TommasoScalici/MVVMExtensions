using System;

using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TommasoScalici.MVVMExtensions.WinRT.Converters
{
    /// <summary>
    /// Value converter that generate a BitmapImage from a String or Uri path.
    /// </summary>
    public sealed class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
                return new BitmapImage(new Uri(value.ToString(), UriKind.RelativeOrAbsolute));

            if (value is Uri)
                return new BitmapImage(value as Uri);

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
