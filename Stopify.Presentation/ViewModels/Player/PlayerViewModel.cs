using Stopify.Presentation.Utilities.Commands.Common;
using Stopify.Presentation.Utilities.Commands.Player;
using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Stopify.Presentation.ViewModels.Player;

public class PlayerViewModel : ViewModelBase
{
    #region Fields

    private byte _repeatState = 0;

    private double _maxMediaValue = 100;
    private double _currentMediaValue = 0;
    private double _volumeValue = 0.5;

    private bool _isSaved = false;
    private bool _isShuffling = false;
    private bool _isPlaying = false;
    private bool _isMuted = false;

    private string _imagePath = string.Empty;
    private string _title = string.Empty;
    private string _currentTime = "00:00";
    private string _totalTime = "00:00";
    private string _hoverPopupText = string.Empty;

    private MediaPlayer _mediaPlayer = new();
    private DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };

    private readonly ObservableCollection<AuthorItemViewModel> _authors = [];

    private readonly UIState _uiState;

    #endregion

    #region Properties

    public bool NowPlayingCollapseState
    {
        get => _uiState.NowPlayingCollapseState;
        set
        {
            if (_uiState.NowPlayingCollapseState != value)
            {
                _uiState.NowPlayingCollapseState = value;
                OnPropertyChanged();
            }
        }
    }

    public bool QueueCollapseState
    {
        get => _uiState.QueueCollapseState;
        set
        {
            if (_uiState.QueueCollapseState != value)
            {
                _uiState.QueueCollapseState = value;
                OnPropertyChanged();
            }
        }
    }

    public byte RepeatState
    {
        get => _repeatState;
        set => SetProperty(ref _repeatState, value);
    }

    public double MaxMediaValue
    {
        get => _maxMediaValue;
        set => SetProperty(ref _maxMediaValue, value);
    }

    public double CurrentMediaValue
    {
        get => _currentMediaValue;
        set => SetProperty(ref _currentMediaValue, value);
    }

    public double VolumeValue
    {
        get => _volumeValue;
        set => SetProperty(ref _volumeValue, value);
    }

    public bool IsSaved
    {
        get => _isSaved;
        set => SetProperty(ref _isSaved, value);
    }

    public bool IsShuffling
    {
        get => _isShuffling;
        set
        {
            SetProperty(ref _isShuffling, value);
            SetHoverPopupText("Azahriah");
        }
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public bool IsMuted
    {
        get => _isMuted;
        set => SetProperty(ref _isMuted, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string CurrentTime
    {
        get => _currentTime;
        set => SetProperty(ref _currentTime, value);
    }

    public string TotalTime
    {
        get => _totalTime;
        set => SetProperty(ref _totalTime, value);
    }

    public string HoverPopupText
    {
        get => _hoverPopupText;
        set => SetProperty(ref _hoverPopupText, value);
    }

    public MediaPlayer MediaPlayer
    {
        get => _mediaPlayer;
        set => SetProperty(ref _mediaPlayer, value);
    }
    public DispatcherTimer Timer
    {
        get => _timer;
        set => SetProperty(ref _timer, value);
    }

    public ObservableCollection<AuthorItemViewModel> Authors => _authors;

    #endregion

    #region Commands

    public ICommand NavigatePlaylistCommand { get; }
    public ICommand NavigateArtistCommand { get; }
    public ICommand SaveSongCommand { get; }
    public ICommand ShuffleQueueCommand { get; }
    public ICommand PreviousSongCommand { get; }
    public ICommand NextSongCommand { get; }
    public ICommand PlayCommand { get; }
    public ICommand RepeatSongCommand { get; }

    #endregion

    #region Constructors

    public PlayerViewModel(UIState uiState)
    {
        _isShuffling = false;
        _title = "introvertált dal";
        _totalTime = "2:45";

        _authors = new()
        {
            new("Azahriah", new()),
            new("DESH", new()),
            new("Young Fly", new()),
        };

        _uiState = uiState;
        _uiState.PropertyChanged += UIStatePropertyChanged;

        NavigatePlaylistCommand = new NavigatePlaylistCommand();
        NavigateArtistCommand = new NavigateArtistCommand();
        SaveSongCommand = new SaveSongCommand();
        ShuffleQueueCommand = new ShuffleQueueCommand();
        PreviousSongCommand = new PreviousSongCommand();
        NextSongCommand = new NextSongCommand();
        PlayCommand = new PlayCommand();
        RepeatSongCommand = new RepeatSongCommand();
    }

    #endregion

    #region Event Handlers

    private void UIStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_uiState.NowPlayingCollapseState):
                OnPropertyChanged(nameof(NowPlayingCollapseState));
                break;
            case nameof(_uiState.QueueCollapseState):
                OnPropertyChanged(nameof(QueueCollapseState));
                break;
        }
    }

    #endregion

    #region Methods

    private void SetHoverPopupText(string playlistTitle) =>
        HoverPopupText = $"{(IsShuffling ? "Disable" : "Enable")} Shuffle for {playlistTitle}";

    #endregion
}
