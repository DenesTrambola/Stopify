using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Stopify.Presentation.Utilities.Converters
{
    public class PlaylistItemCommaVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int totalItems && parameter is string indexString && int.TryParse(indexString, out int index))
            {
                return index < totalItems - 1 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
