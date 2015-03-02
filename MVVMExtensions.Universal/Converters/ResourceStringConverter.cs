﻿using System;
using System.Reflection;

using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace MyAnimeList.Converters
{
    /// <summary>
    /// Value converter that look up for the string passed in the App Resources strings and returns its value, if found.
    /// </summary>
    public sealed class ResourceStringConverter : IValueConverter
    {
        private static readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string || value.GetType().GetTypeInfo().IsEnum)
                return resourceLoader.GetString(value.ToString());

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
