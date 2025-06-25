using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using Stopify.Presentation.ViewModels.Playlist;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Artist;

public class ArtistViewModel : ViewModelBase
{
    #region Fields

    private bool _isPlaying = false;
    private bool _isShuffling = false;
    private bool _isFollowing = false;
    private bool _isFilteringPopularReleases = true;
    private bool _isFilteringAlbums = false;
    private bool _isFilteringSingles = false;

    private string _title = "Azahriah";
    private string _monthlyListeners = "700,000";
    private string _description = "creator from hungary";
    private string _hoverPopupText = string.Empty;

    private ObservableCollection<CommonItemViewModel> _discographyItems;
    private ObservableCollection<PlaylistItemViewModel> _populars;

    #endregion

    #region Properties

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public bool IsShuffling
    {
        get => _isShuffling;
        set
        {
            SetProperty(ref _isShuffling, value);
            UpdateHoverPopupText();
        }
    }

    public bool IsFollowing
    {
        get => _isFollowing;
        set => SetProperty(ref _isFollowing, value);
    }

    public bool IsFilteringPopularReleases
    {
        get => _isFilteringPopularReleases;
        set => SetProperty(ref _isFilteringPopularReleases, value);
    }

    public bool IsFilteringAlbums
    {
        get => _isFilteringAlbums;
        set => SetProperty(ref _isFilteringAlbums, value);
    }

    public bool IsFilteringSingles
    {
        get => _isFilteringSingles;
        set => SetProperty(ref _isFilteringSingles, value);
    }

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

    public string HoverPopupText
    {
        get => _hoverPopupText;
        set => SetProperty(ref _hoverPopupText, value);
    }

    public ObservableCollection<CommonItemViewModel> DiscographyItems => _discographyItems;

    public ObservableCollection<PlaylistItemViewModel> Populars => _populars;

    #endregion

    #region Constructors

    public ArtistViewModel()
    {
        _discographyItems = new ObservableCollection<CommonItemViewModel>
        {
            new CommonItemViewModel("ZHA MAJ DUR", "Latest Release · Single", string.Empty),
            new CommonItemViewModel("A ló túloldalán", "2022 · Album", string.Empty),
            new CommonItemViewModel("memento", "2023 · Album", string.Empty),
            new CommonItemViewModel("tripq", "2023 · EP", string.Empty),
            new CommonItemViewModel("silbak", "2022 · EP", string.Empty),
            new CommonItemViewModel("BAKPAKK", "2024 - Single", string.Empty),
            new CommonItemViewModel("skatulya I", "2024 · Album", string.Empty),
            new CommonItemViewModel("Puskás Aréna Live (2024)", "2024 · Album", string.Empty),
            new CommonItemViewModel("camouflage", "2021 · Album", string.Empty),
        };

        _populars = new ObservableCollection<PlaylistItemViewModel>
        {
            new PlaylistItemViewModel("1", "PANNONIA", "PANNONIA", "8 months ago", "2:27", string.Empty),
            new PlaylistItemViewModel("2", "BAKPAKK", "BAKPAKK", "8 months ago", "2:47", string.Empty),
            new PlaylistItemViewModel("3", "ZHA MAJ DUR", "ZHA MAJ DUR", "8 months ago", "3:39", string.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló tóloldalán", "8 months ago", "3:01", string.Empty),
            new PlaylistItemViewModel("5", "Mind1", "A ló tóloldalán", "8 months ago", "3:11", string.Empty),
            new PlaylistItemViewModel("6", "introvertált dal", "memento", "8 months ago", "2:49", string.Empty),
            new PlaylistItemViewModel("7", "Rét", "Rét", "8 months ago", "2:59", string.Empty),
            new PlaylistItemViewModel("8", "3korty", "memento", "8 months ago", "3:13", string.Empty),
            new PlaylistItemViewModel("9", "Rampapagam", "CARPE DIEM", "8 months ago", "3:09", string.Empty),
            new PlaylistItemViewModel("10", "Pullup", "A ló tóloldalán", "8 months ago", "2:17", string.Empty),
        };

        UpdateHoverPopupText();
    }

    #endregion

    #region Methods

    private void UpdateHoverPopupText() =>
        HoverPopupText = $"{(_isShuffling ? "Disable" : "Enable")} Shuffle for {_title}";

    #endregion
}
