using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistItemViewModel : ViewModelBase
{
    #region Fields

    private bool _isSelected = false;
    private bool _isPlaying = false;
    private bool _isSaved = false;

    private string _number = string.Empty;
    private string _title = string.Empty;
    private string _album = string.Empty;
    private string _dateAdded = string.Empty;
    private string _duration = string.Empty;
    private string _imagePath = string.Empty;
    private string _saveTo = "Liked Songs";

    private ObservableCollection<string> _authors;

    #endregion

    #region Properties

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public bool IsSaved
    {
        get => _isSaved;
        set => SetProperty(ref _isSaved, value);
    }

    public string Number
    {
        get => _number;
        set => SetProperty(ref _number, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Album
    {
        get => _album;
        set => SetProperty(ref _album, value);
    }

    public string DateAdded
    {
        get => _dateAdded;
        set => SetProperty(ref _dateAdded, value);
    }

    public string Duration
    {
        get => _duration;
        set => SetProperty(ref _duration, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public string SaveTo
    {
        get => _saveTo;
        set => SetProperty(ref _saveTo, value);
    }

    public ObservableCollection<string> Authors => _authors;

    #endregion

    #region Constructors

    public PlaylistItemViewModel(string number, string title, string album, string dateAdded, string duration, string imagePath)
    {
        _number = number;
        _title = title;
        _album = album;
        _dateAdded = dateAdded;
        _duration = duration;
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
