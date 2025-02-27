using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeViewModel : ViewModelBase
{
    public ObservableCollection<CommonRowViewModel> _rows;

    public IEnumerable<CommonRowViewModel> Rows => _rows;

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
    }
}
