using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Queue;

public class QueueItemViewModel : ViewModelBase
{
    #region Fields

    private bool _isPlaying = false;
    private string _track;
    private string _imagePath;

    private ObservableCollection<string> _authors;

    #endregion

    #region Properties

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public string Track
    {
        get => _track;
        set => SetProperty(ref _track, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public IEnumerable<string> Authors => _authors;

    #endregion

    #region Constructors

    public QueueItemViewModel(string track, string imagePath)
    {
        _track = track;
        _imagePath = imagePath;

        _authors = new ObservableCollection<string>()
        {
            "Azahriah",
            "DESH",
            "Young Fly",
        };
    }

    #endregion
}
