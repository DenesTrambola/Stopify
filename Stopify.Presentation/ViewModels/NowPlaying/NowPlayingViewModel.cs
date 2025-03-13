using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Queue;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.NowPlaying;

public class NowPlayingViewModel : ViewModelBase
{
    #region Fields

    private string _playlistTitle;
    private string _songImagePath;
    private string _title;

    private string? _artist;
    private string? _artistImagePath;
    private string? _monthlyListeners;
    private string? _artistDescription;
    private string? _followStatus;

    private QueueItemViewModel _nextSong;

    private ObservableCollection<string> _authors;
    private ObservableCollection<NowPlayingCreditsItemViewModel> _credits;

    #endregion

    #region Properties

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

    public string? FollowStatus
    {
        get => _followStatus;
        set => SetProperty(ref _followStatus, value);
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
        PlaylistTitle = "Azahriah";
        SongImagePath = String.Empty;
        Title = "PANNONIA";
        Artist = "Azahriah";
        ArtistImagePath = String.Empty;
        MonthlyListeners = "700,000";
        ArtistDescription = "creator from hungary";
        FollowStatus = "Unfollow";

        _authors = new ObservableCollection<string>
        {
            "Azahriah",
            "DESH",
            "Young Fly",
        };

        _credits = new ObservableCollection<NowPlayingCreditsItemViewModel>
        {
            new NowPlayingCreditsItemViewModel("Azahriah", "Followed"),
            new NowPlayingCreditsItemViewModel("DESH", "Follow"),
            new NowPlayingCreditsItemViewModel("Young Fly", "Follow"),
        };

        NextSong = new QueueItemViewModel("BAKPAKK", String.Empty);
    }

    #endregion
}
