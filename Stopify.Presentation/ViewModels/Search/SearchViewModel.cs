using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Search;

public class SearchViewModel : ViewModelBase
{
    private ObservableCollection<FilterRowViewModel> _filterRows;
    private ObservableCollection<SearchCategoryItemViewModel> _searchCategoryItems;
    private int _totalColumns;

    public IEnumerable<FilterRowViewModel> FilterRows => _filterRows;
    public IEnumerable<SearchCategoryItemViewModel> SearchCategoryItems => _searchCategoryItems;
    public int TotalColumns
    {
        get => _totalColumns;
        set
        {
            SetProperty(ref _totalColumns, value);

        }
    }

    public SearchViewModel()
    {
        _filterRows = new ObservableCollection<FilterRowViewModel>()
        {
            new FilterRowViewModel("Search results"),
            new FilterRowViewModel("Recent searches"),
        };

        _searchCategoryItems = new ObservableCollection<SearchCategoryItemViewModel>
        {
            new SearchCategoryItemViewModel("music"),
            new SearchCategoryItemViewModel("podcasts"),
            new SearchCategoryItemViewModel("liveEvents"),
            new SearchCategoryItemViewModel("madeForYou"),
            new SearchCategoryItemViewModel("newReleases"),
            new SearchCategoryItemViewModel("pop"),
            new SearchCategoryItemViewModel("hip-hop"),
            new SearchCategoryItemViewModel("rock"),
            new SearchCategoryItemViewModel("mood"),
            new SearchCategoryItemViewModel("comedy"),
            new SearchCategoryItemViewModel("educational"),
            new SearchCategoryItemViewModel("trueCrime"),
            new SearchCategoryItemViewModel("sports"),
            new SearchCategoryItemViewModel("charts"),
            new SearchCategoryItemViewModel("dance_electronic"),
            new SearchCategoryItemViewModel("chill"),
            new SearchCategoryItemViewModel("indie"),
            new SearchCategoryItemViewModel("workout"),
            new SearchCategoryItemViewModel("discover"),
            new SearchCategoryItemViewModel("folkAndAcoustic"),
            new SearchCategoryItemViewModel("rAndB"),
            new SearchCategoryItemViewModel("k-Pop"),
            new SearchCategoryItemViewModel("latin"),
            new SearchCategoryItemViewModel("sleep"),
            new SearchCategoryItemViewModel("party"),
            new SearchCategoryItemViewModel("atHome"),
            new SearchCategoryItemViewModel("decades"),
            new SearchCategoryItemViewModel("love"),
            new SearchCategoryItemViewModel("metal"),
            new SearchCategoryItemViewModel("jazz"),
            new SearchCategoryItemViewModel("trending"),
            new SearchCategoryItemViewModel("classical"),
            new SearchCategoryItemViewModel("country"),
            new SearchCategoryItemViewModel("focus"),
            new SearchCategoryItemViewModel("soul"),
            new SearchCategoryItemViewModel("kidsAndFamily"),
            new SearchCategoryItemViewModel("gaming"),
            new SearchCategoryItemViewModel("anime"),
            new SearchCategoryItemViewModel("tvAndMovies"),
            new SearchCategoryItemViewModel("instrumental"),
            new SearchCategoryItemViewModel("wellness"),
            new SearchCategoryItemViewModel("punk"),
            new SearchCategoryItemViewModel("ambient"),
            new SearchCategoryItemViewModel("blues"),
            new SearchCategoryItemViewModel("cookingAndDining"),
            new SearchCategoryItemViewModel("alternative"),
            new SearchCategoryItemViewModel("travel"),
            new SearchCategoryItemViewModel("caribbean"),
            new SearchCategoryItemViewModel("afro"),
            new SearchCategoryItemViewModel("songwriters"),
            new SearchCategoryItemViewModel("natureAndNoise"),
            new SearchCategoryItemViewModel("funkAndDisco"),
            new SearchCategoryItemViewModel("glow"),
            new SearchCategoryItemViewModel("spotifySingles"),
            new SearchCategoryItemViewModel("netflix"),
            new SearchCategoryItemViewModel("summer"),
            new SearchCategoryItemViewModel("radar"),
            new SearchCategoryItemViewModel("equal"),
            new SearchCategoryItemViewModel("freshFinds"),
        };
    }

    public void OnSizeChanged(double actualWidth)
    {
        if (actualWidth < 730)
            TotalColumns = 2;
        else if (actualWidth < 1000)
            TotalColumns = 3;
        else
            TotalColumns = 4;
    }
}
