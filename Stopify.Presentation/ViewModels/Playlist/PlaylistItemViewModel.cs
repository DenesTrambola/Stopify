using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistItemViewModel : ViewModelBase
{
    private string _number;
    private string _title;
    private string _authors;
    private string _album;
    private string _dateAdded;
    private string _duration;
    private string _imagePath;

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

    public string Authors
    {
        get => _authors;
        set => SetProperty(ref _authors, value);
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

    public PlaylistItemViewModel(string number, string title, string authors, string album, string dateAdded, string duration, string imagePath)
    {
        Number = number;
        Title = title;
        Authors = authors;
        Album = album;
        DateAdded = dateAdded;
        Duration = duration;
        Number = number;
        ImagePath = imagePath;
        Title = title;
        Authors = authors;
        Album = album;
        DateAdded = dateAdded;
        Duration = duration;
        ImagePath = imagePath;
    }
}
