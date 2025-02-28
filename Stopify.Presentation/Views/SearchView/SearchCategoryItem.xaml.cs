using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Stopify.Presentation.Views.SearchView;

public partial class SearchCategoryItem : UserControl
{
    public SearchCategoryItem()
    {
        InitializeComponent();
    }

    private void SearchPageItemBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void SearchPageItemBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void SearchPageItemBtn_Click(object sender, RoutedEventArgs e) { }
}
