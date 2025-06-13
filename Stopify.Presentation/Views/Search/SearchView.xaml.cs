using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Views.Search;

public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void SearchPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        //_viewModel.OnSizeChanged(e.NewSize.Width);
    }
}
