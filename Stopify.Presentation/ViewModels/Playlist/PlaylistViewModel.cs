using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistViewModel : ViewModelBase
{
    #region Fields

    private bool _isPlaying = false;
    private bool _isShuffling = false;
    private bool _isSaved = false;
    private bool _isSearching = false;

    private string _title = "Coding Music Programming Playlist";
    private string _type = "Public Playlist";
    private string _description = "best coding music - best coding songs - lofi code song - ";
    private string _saves = "125,000";
    private string _songs = "201";
    private string _duration = "7 hr 21 min";
    private string _hoverPopupText = string.Empty;
    private string _saveTo = "Liked Songs";
    private string _searchText = string.Empty;
    private string _imagePath = string.Empty;

    private ObservableCollection<PlaylistAuthorViewModel> _authors;
    private ObservableCollection<PlaylistItemViewModel> _songItems;
    private ObservableCollection<PlaylistItemViewModel> _recommendedItems;

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

    public bool IsSaved
    {
        get => _isSaved;
        set => SetProperty(ref _isSaved, value);
    }

    public bool IsSearching
    {
        get => _isSearching;
        set => SetProperty(ref _isSearching, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Saves
    {
        get => _saves;
        set => SetProperty(ref _saves, value);
    }

    public string Songs
    {
        get => _songs;
        set => SetProperty(ref _songs, value);
    }

    public string Duration
    {
        get => _duration;
        set => SetProperty(ref _duration, value);
    }

    public string HoverPopupText
    {
        get => _hoverPopupText;
        set => SetProperty(ref _hoverPopupText, value);
    }

    public string SaveTo
    {
        get => _saveTo;
        set => SetProperty(ref _saveTo, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public ObservableCollection<PlaylistAuthorViewModel> Authors => _authors;

    public ObservableCollection<PlaylistItemViewModel> SongItems => _songItems;

    public ObservableCollection<PlaylistItemViewModel> RecommendedItems => _recommendedItems;

    #endregion

    #region Constructors

    public PlaylistViewModel()
    {
        _authors = new ObservableCollection<PlaylistAuthorViewModel>()
        {
            new PlaylistAuthorViewModel("Azahriah", string.Empty),
            new PlaylistAuthorViewModel("DESH", string.Empty),
            new PlaylistAuthorViewModel("Young Fly", string.Empty),
        };

        _songItems = new ObservableCollection<PlaylistItemViewModel>()
        {
            new PlaylistItemViewModel("1", "Tisztán iszom", "A ló túloldalán", "3 years ago", "3:00", string.Empty),
            new PlaylistItemViewModel("2", "Drogba", "A ló túloldalán", "3 years ago", "2:43", string.Empty),
            new PlaylistItemViewModel("3", "Miafasz", "A ló túloldalán", "3 years ago", "3:13", string.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló túloldalán", "3 years ago", "3:01", string.Empty),
            new PlaylistItemViewModel("5", "Okari", "A ló túloldalán", "3 years ago", "4:09", string.Empty),
            new PlaylistItemViewModel("6", "Pullup", "A ló túloldalán", "3 years ago", "2:17", string.Empty),
            new PlaylistItemViewModel("7", "Habibi", "A ló túloldalán", "3 years ago", "2:41", string.Empty),
            new PlaylistItemViewModel("8", "tevagyazalány", "A ló túloldalán", "3 years ago", "2:18", string.Empty),
            new PlaylistItemViewModel("9", "Mind1", "A ló túloldalán", "3 years ago", "3:11", string.Empty),
            new PlaylistItemViewModel("10", "Lóerő", "A ló túloldalán", "3 years ago", "2:57", string.Empty),
            new PlaylistItemViewModel("11", "Megmentő", "A ló túloldalán", "3 years ago", "2:54", string.Empty),
            new PlaylistItemViewModel("12", "Domapin (Bonus Track)", "A ló túloldalán", "3 years ago", "2:08", string.Empty),
        };

        _recommendedItems = new ObservableCollection<PlaylistItemViewModel>()
        {
            new PlaylistItemViewModel("1", "Tisztán iszom", "A ló túloldalán", "3 years ago", "3:00", string.Empty),
            new PlaylistItemViewModel("2", "Drogba", "A ló túloldalán", "3 years ago", "2:43", string.Empty),
            new PlaylistItemViewModel("3", "Miafasz", "A ló túloldalán", "3 years ago", "3:13", string.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló túloldalán", "3 years ago", "3:01", string.Empty),
            new PlaylistItemViewModel("5", "Okari", "A ló túloldalán", "3 years ago", "4:09", string.Empty),
            new PlaylistItemViewModel("6", "Pullup", "A ló túloldalán", "3 years ago", "2:17", string.Empty),
            new PlaylistItemViewModel("7", "Habibi", "A ló túloldalán", "3 years ago", "2:41", string.Empty),
            new PlaylistItemViewModel("8", "tevagyazalány", "A ló túloldalán", "3 years ago", "2:18", string.Empty),
            new PlaylistItemViewModel("9", "Mind1", "A ló túloldalán", "3 years ago", "3:11", string.Empty),
            new PlaylistItemViewModel("10", "Lóerő", "A ló túloldalán", "3 years ago", "2:57", string.Empty),
        };

        UpdateHoverPopupText();
    }

    #endregion

    #region Methods

    private void UpdateHoverPopupText() =>
        HoverPopupText = $"{(_isShuffling ? "Disable" : "Enable")} Shuffle for {_title}";

    #endregion
}
