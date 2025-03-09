using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using Stopify.Presentation.ViewModels.Playlist;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Artist;

public class ArtistViewModel : ViewModelBase
{
    private string _title;
    private string _monthlyListeners;
    private string _description;
    private ObservableCollection<CommonItemViewModel> _discographyItems;
    private ObservableCollection<PlaylistItemViewModel> _populars;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string MonthlyListeners
    {
        get => _monthlyListeners;
        set => SetProperty(ref _monthlyListeners, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public IEnumerable<CommonItemViewModel> DiscographyItems => _discographyItems;

    public IEnumerable<PlaylistItemViewModel> Populars => _populars;

    public ArtistViewModel()
    {
        Title = "Azahriah";
        MonthlyListeners = "700,000";
        Description = "creator from hungary";

        _discographyItems = new ObservableCollection<CommonItemViewModel>
        {
            new CommonItemViewModel("ZHA MAJ DUR", "Latest Release · Single", String.Empty),
            new CommonItemViewModel("A ló túloldalán", "2022 · Album", String.Empty),
            new CommonItemViewModel("memento", "2023 · Album", String.Empty),
            new CommonItemViewModel("tripq", "2023 · EP", String.Empty),
            new CommonItemViewModel("silbak", "2022 · EP", String.Empty),
            new CommonItemViewModel("BAKPAKK", "2024 - Single", String.Empty),
            new CommonItemViewModel("skatulya I", "2024 · Album", String.Empty),
            new CommonItemViewModel("Puskás Aréna Live (2024)", "2024 · Album", String.Empty),
            new CommonItemViewModel("camouflage", "2021 · Album", String.Empty),
        };

        _populars = new ObservableCollection<PlaylistItemViewModel>
        {
            new PlaylistItemViewModel("1", "PANNONIA", "PANNONIA", "8 months ago", "2:27", String.Empty),
            new PlaylistItemViewModel("2", "BAKPAKK", "BAKPAKK", "8 months ago", "2:47", String.Empty),
            new PlaylistItemViewModel("3", "ZHA MAJ DUR", "ZHA MAJ DUR", "8 months ago", "3:39", String.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló tóloldalán", "8 months ago", "3:01", String.Empty),
            new PlaylistItemViewModel("5", "Mind1", "A ló tóloldalán", "8 months ago", "3:11", String.Empty),
            new PlaylistItemViewModel("6", "introvertált dal", "memento", "8 months ago", "2:49", String.Empty),
            new PlaylistItemViewModel("7", "Rét", "Rét", "8 months ago", "2:59", String.Empty),
            new PlaylistItemViewModel("8", "3korty", "memento", "8 months ago", "3:13", String.Empty),
            new PlaylistItemViewModel("9", "Rampapagam", "CARPE DIEM", "8 months ago", "3:09", String.Empty),
            new PlaylistItemViewModel("10", "Pullup", "A ló tóloldalán", "8 months ago", "2:17", String.Empty),
        };
    }
}
