using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeViewModel : ViewModelBase
{
    public ObservableCollection<CommonRowViewModel> _rows;
    public ObservableCollection<HomeRecentPlaysItemViewModel> _recentPlays;
    private int _columnCount;

    public IEnumerable<CommonRowViewModel> Rows => _rows;
    public IEnumerable<HomeRecentPlaysItemViewModel> RecentPlays => _recentPlays;
    public int ColumnCount
    {
        get => _columnCount;
        set => SetProperty(ref _columnCount, value);
    }

    public HomeViewModel()
    {
        _rows = new ObservableCollection<CommonRowViewModel>()
        {
            new CommonRowViewModel("Recently played"),
            new CommonRowViewModel("Your favorite artists"),
            new CommonRowViewModel("Jump back in"),
            new CommonRowViewModel("Best of artists"),
            new CommonRowViewModel("Recommended for today"),
            new CommonRowViewModel("New releases for you"),
            new CommonRowViewModel("More like {Artist}"),
            new CommonRowViewModel("Fresh new music"),
            new CommonRowViewModel("More like {Artist}"),
            new CommonRowViewModel("Popular artists"),
            new CommonRowViewModel("For fans of {Artist}"),
        };

        _recentPlays = new ObservableCollection<HomeRecentPlaysItemViewModel>
        {
            new HomeRecentPlaysItemViewModel("Azahriah", ""),
            new HomeRecentPlaysItemViewModel("DESH", ""),
            new HomeRecentPlaysItemViewModel("YoungFly", ""),
            new HomeRecentPlaysItemViewModel("Nessaj", ""),
            new HomeRecentPlaysItemViewModel("Coding Music", ""),
            new HomeRecentPlaysItemViewModel("Gym Songs", ""),
            new HomeRecentPlaysItemViewModel("Calisthenics", ""),
            new HomeRecentPlaysItemViewModel("Toth Gabi", ""),
        };

        ColumnCount = 2;
    }

    public void UpdateColumnCount(double width) =>
        ColumnCount = width >= 1000 ? 4 : 2;
}
