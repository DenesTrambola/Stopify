﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Stopify.Presentation.Utilities.Converters;

public class PlaylistItemCommaVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count && count > 1)
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
