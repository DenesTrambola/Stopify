using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistItemViewModel : ViewModelBase
{
    #region Fields

    private string _number;
    private string _title;
    private string _album;
    private string _dateAdded;
    private string _duration;
    private string _imagePath;

    private ObservableCollection<string> _authors;

    #endregion

    #region Properties

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

    public IEnumerable<string> Authors => _authors;

    #endregion

    #region Constructors

    public PlaylistItemViewModel(string number, string title, string album, string dateAdded, string duration, string imagePath)
    {
        Number = number;
        Title = title;
        Album = album;
        DateAdded = dateAdded;
        Duration = duration;

        _authors = new ObservableCollection<string>()
        {
            "Azahriah",
            "DESH",
            "Young Fly",
        };
    }

    #endregion
}
