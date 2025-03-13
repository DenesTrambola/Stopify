using Stopify.Presentation.ViewModels.Search;
using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Views.Search;

public partial class SearchView : UserControl
{
    private readonly SearchViewModel _viewModel;

    public SearchView(SearchViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    private void SearchPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        _viewModel.OnSizeChanged(e.NewSize.Width);
    }
}
