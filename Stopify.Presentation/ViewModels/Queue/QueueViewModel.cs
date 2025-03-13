using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Queue;

public class QueueViewModel : ViewModelBase
{
    #region Fields

    private string _playlistTitle;

    private QueueItemViewModel _nowPlayingSong;

    private ObservableCollection<QueueItemViewModel> _queueSongs;
    private ObservableCollection<QueueItemViewModel> _recentlyPlayedSongs;

    #endregion

    #region Properties

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

    public QueueViewModel()
    {
        NowPlayingSong = new QueueItemViewModel("introvertált dal", String.Empty);
        _queueSongs = new ObservableCollection<QueueItemViewModel>()
        {
            new QueueItemViewModel("zene1", String.Empty),
            new QueueItemViewModel("zene2", String.Empty),
            new QueueItemViewModel("zene3", String.Empty),
            new QueueItemViewModel("zene4", String.Empty),
            new QueueItemViewModel("zene5", String.Empty),
            new QueueItemViewModel("zene6", String.Empty),
            new QueueItemViewModel("zene7", String.Empty),
            new QueueItemViewModel("zene8", String.Empty),
            new QueueItemViewModel("zene9", String.Empty),
            new QueueItemViewModel("zene10", String.Empty),
        };

        _recentlyPlayedSongs = new ObservableCollection<QueueItemViewModel>()
        {
            new QueueItemViewModel("zene1", String.Empty),
            new QueueItemViewModel("zene2", String.Empty),
            new QueueItemViewModel("zene3", String.Empty),
            new QueueItemViewModel("zene4", String.Empty),
            new QueueItemViewModel("zene5", String.Empty),
            new QueueItemViewModel("zene6", String.Empty),
            new QueueItemViewModel("zene7", String.Empty),
            new QueueItemViewModel("zene8", String.Empty),
            new QueueItemViewModel("zene9", String.Empty),
            new QueueItemViewModel("zene10", String.Empty),
        };
    }

    #endregion
}
