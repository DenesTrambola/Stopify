using Stopify.Presentation.Views.CommonViews;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Stopify.Presentation.Views.SearchView;

public class WidthToHeightConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is double actualWidth)
            return actualWidth * 0.58;
        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class SearchViewItem : UserControl
{
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register(nameof(Image), typeof(string), typeof(CommonRowView), new PropertyMetadata(string.Empty));

    public string Image
    {
        get { return (string)GetValue(ImageProperty); }
        set
        {
            try
            {
                SetValue(ImageProperty, "D:\\IT Step\\C#\\Stopify\\Stopify.Presentation\\Resources\\SearchPage\\" + value + ".png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + value + "misspelled!");
            }
        }
    }

    public SearchViewItem()
    {
        InitializeComponent();
    }

    private void SearchPageItemBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void SearchPageItemBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void SearchPageItemBtn_Click(object sender, RoutedEventArgs e) { }
}
