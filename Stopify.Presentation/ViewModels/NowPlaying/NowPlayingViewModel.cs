using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Queue;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.NowPlaying;

public class NowPlayingViewModel : ViewModelBase
{
    #region Fields

    private bool _isSaved = false;
    private bool _isFollowing = false;

    private string _playlistTitle = "Azahriah";
    private string _songImagePath = string.Empty;
    private string _title = "PANNONIA";
    private string _saveTo = "Liked Songs";

    private string? _artist;
    private string? _artistImagePath;
    private string? _monthlyListeners;
    private string? _artistDescription;

    private QueueItemViewModel _nextSong;

    private ObservableCollection<string> _authors;
    private ObservableCollection<NowPlayingCreditsItemViewModel> _credits;

    #endregion

    #region Properties

    public bool IsSaved
    {
        get => _isSaved;
        set => SetProperty(ref _isSaved, value);
    }

    public bool IsFollowing
    {
        get => _isFollowing;
        set => SetProperty(ref _isFollowing, value);
    }

    public string PlaylistTitle
    {
        get => _playlistTitle;
        set => SetProperty(ref _playlistTitle, value);
    }

    public string SongImagePath
    {
        get => _songImagePath;
        set => SetProperty(ref _songImagePath, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string SaveTo
    {
        get => _saveTo;
        set => SetProperty(ref _saveTo, value);
    }

    public string? Artist
    {
        get => _artist;
        set => SetProperty(ref _artist, value);
    }

    public string? ArtistImagePath
    {
        get => _artistImagePath;
        set => SetProperty(ref _artistImagePath, value);
    }

    public string? MonthlyListeners
    {
        get => _monthlyListeners;
        set => SetProperty(ref _monthlyListeners, value);
    }

    public string? ArtistDescription
    {
        get => _artistDescription;
        set => SetProperty(ref _artistDescription, value);
    }

    public QueueItemViewModel NextSong
    {
        get => _nextSong;
        set => SetProperty(ref _nextSong, value);
    }

    public ObservableCollection<string> Authors => _authors;

    public ObservableCollection<NowPlayingCreditsItemViewModel> Credits => _credits;

    #endregion

    #region Constructors

    public NowPlayingViewModel()
    {
        Artist = "Azahriah";
        ArtistImagePath = string.Empty;
        MonthlyListeners = "700,000";
        ArtistDescription = "creator from hungary";

        _authors = new ObservableCollection<string>
        {
            "Azahriah",
            "DESH",
            "Young Fly",
        };

        _credits = new ObservableCollection<NowPlayingCreditsItemViewModel>
        {
            new NowPlayingCreditsItemViewModel("Azahriah", true),
            new NowPlayingCreditsItemViewModel("DESH", false),
            new NowPlayingCreditsItemViewModel("Young Fly", false),
        };

        _nextSong = new QueueItemViewModel("BAKPAKK", string.Empty);
    }

    #endregion
}
