using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Search;

public class SearchViewModel : ViewModelBase
{
    private ObservableCollection<FilterRowViewModel> _filterRows;

    public IEnumerable<FilterRowViewModel> FilterRows => _filterRows;

    public SearchViewModel()
    {
        _filterRows = new ObservableCollection<FilterRowViewModel>()
        {
            new FilterRowViewModel("Search results"),
            new FilterRowViewModel("Recent searches"),
        };
    }
}
