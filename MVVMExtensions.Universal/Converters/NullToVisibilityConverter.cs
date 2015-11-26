﻿using System;
using System.Diagnostics.CodeAnalysis;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TommasoScalici.MVVMExtensions.Universal.Converters
{
    /// <summary>
    /// Value converter that translates a null object to <see cref="Visibility.Collapsed"/> and a not null object to
    /// <see cref="Visibility.Visible"/>.
    /// </summary>
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Apply NOT operand to the result of the converter.
        /// </summary>
        public bool Inverted { get; set; }

        [SuppressMessage("Warning", "CS1591")]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ^ Inverted ? Visibility.Collapsed : Visibility.Visible;
        }

        [SuppressMessage("Message", "RECS0083")]
        [SuppressMessage("Warning", "CS1591")]
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
