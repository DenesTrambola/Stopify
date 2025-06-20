using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Stopify.Presentation.ViewModels.Queue;

public class QueueViewModel : ViewModelBase
{
    #region Fields

    private string _playlistTitle = "Azahriah";

    private QueueItemViewModel _nowPlayingSong;

    private ObservableCollection<QueueItemViewModel> _queueSongs;
    private ObservableCollection<QueueItemViewModel> _recentlyPlayedSongs;

    UIState _uiState;

    #endregion

    #region Properties

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

    public string PlaylistTitle
    {
        get => _playlistTitle;
        set => SetProperty(ref _playlistTitle, value);
    }

    public QueueItemViewModel NowPlayingSong
    {
        get => _nowPlayingSong;
        set => SetProperty(ref _nowPlayingSong, value);
    }

    public ObservableCollection<QueueItemViewModel> Songs => _queueSongs;

    #endregion

    #region Constructors

    public QueueViewModel(UIState uiState)
    {
        _nowPlayingSong = new QueueItemViewModel("introvertált dal", string.Empty);
        _queueSongs = new ObservableCollection<QueueItemViewModel>()
        {
            new QueueItemViewModel("zene1", string.Empty),
            new QueueItemViewModel("zene2", string.Empty),
            new QueueItemViewModel("zene3", string.Empty),
            new QueueItemViewModel("zene4", string.Empty),
            new QueueItemViewModel("zene5", string.Empty),
            new QueueItemViewModel("zene6", string.Empty),
            new QueueItemViewModel("zene7", string.Empty),
            new QueueItemViewModel("zene8", string.Empty),
            new QueueItemViewModel("zene9", string.Empty),
            new QueueItemViewModel("zene10", string.Empty),
        };

        _recentlyPlayedSongs = new ObservableCollection<QueueItemViewModel>()
        {
            new QueueItemViewModel("zene1", string.Empty),
            new QueueItemViewModel("zene2", string.Empty),
            new QueueItemViewModel("zene3", string.Empty),
            new QueueItemViewModel("zene4", string.Empty),
            new QueueItemViewModel("zene5", string.Empty),
            new QueueItemViewModel("zene6", string.Empty),
            new QueueItemViewModel("zene7", string.Empty),
            new QueueItemViewModel("zene8", string.Empty),
            new QueueItemViewModel("zene9", string.Empty),
            new QueueItemViewModel("zene10", string.Empty),
        };

        _uiState = uiState;
        _uiState.PropertyChanged += UIStatePropertyChanged;
    }

    #endregion

    #region Event Handlers

    private void UIStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_uiState.QueueCollapseState):
                OnPropertyChanged(nameof(QueueCollapseState));
                break;
        }
    }

    #endregion
}
