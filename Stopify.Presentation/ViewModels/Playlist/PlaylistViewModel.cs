using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistViewModel : ViewModelBase
{
    private string _title;
    private string _type;
    private string _description;
    private string _saves;
    private string _songs;
    private string _duration;
    private string _imagePath;
    private ObservableCollection<PlaylistAuthorViewModel> _authors;
    private ObservableCollection<PlaylistItemViewModel> _songItems;
    private ObservableCollection<PlaylistItemViewModel> _recommendedItems;

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

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public IEnumerable<PlaylistAuthorViewModel> Authors => _authors;

    public IEnumerable<PlaylistItemViewModel> SongItems => _songItems;

    public IEnumerable<PlaylistItemViewModel> RecommendedItems => _recommendedItems;

    public PlaylistViewModel()
    {
        Title = "Coding Music Programming Playlist";
        Type = "Public Playlist";
        Description = "best coding music - best coding songs - lofi code song - ";
        Saves = "125,000";
        Songs = "201";
        Duration = "7 hr 21 min";
        ImagePath = String.Empty;

        _authors = new ObservableCollection<PlaylistAuthorViewModel>()
        {
            new PlaylistAuthorViewModel("Azahriah", String.Empty),
            new PlaylistAuthorViewModel("DESH", String.Empty),
            new PlaylistAuthorViewModel("Young Fly", String.Empty),
        };

        _songItems = new ObservableCollection<PlaylistItemViewModel>()
        {
            new PlaylistItemViewModel("1", "Tisztán iszom", "A ló túloldalán", "3 years ago", "3:00", String.Empty),
            new PlaylistItemViewModel("2", "Drogba", "A ló túloldalán", "3 years ago", "2:43", String.Empty),
            new PlaylistItemViewModel("3", "Miafasz", "A ló túloldalán", "3 years ago", "3:13", String.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló túloldalán", "3 years ago", "3:01", String.Empty),
            new PlaylistItemViewModel("5", "Okari", "A ló túloldalán", "3 years ago", "4:09", String.Empty),
            new PlaylistItemViewModel("6", "Pullup", "A ló túloldalán", "3 years ago", "2:17", String.Empty),
            new PlaylistItemViewModel("7", "Habibi", "A ló túloldalán", "3 years ago", "2:41", String.Empty),
            new PlaylistItemViewModel("8", "tevagyazalány", "A ló túloldalán", "3 years ago", "2:18", String.Empty),
            new PlaylistItemViewModel("9", "Mind1", "A ló túloldalán", "3 years ago", "3:11", String.Empty),
            new PlaylistItemViewModel("10", "Lóerő", "A ló túloldalán", "3 years ago", "2:57", String.Empty),
            new PlaylistItemViewModel("11", "Megmentő", "A ló túloldalán", "3 years ago", "2:54", String.Empty),
            new PlaylistItemViewModel("12", "Domapin (Bonus Track)", "A ló túloldalán", "3 years ago", "2:08", String.Empty),
        };

        _recommendedItems = new ObservableCollection<PlaylistItemViewModel>()
        {
            new PlaylistItemViewModel("1", "Tisztán iszom", "A ló túloldalán", "3 years ago", "3:00", String.Empty),
            new PlaylistItemViewModel("2", "Drogba", "A ló túloldalán", "3 years ago", "2:43", String.Empty),
            new PlaylistItemViewModel("3", "Miafasz", "A ló túloldalán", "3 years ago", "3:13", String.Empty),
            new PlaylistItemViewModel("4", "Felednéd", "A ló túloldalán", "3 years ago", "3:01", String.Empty),
            new PlaylistItemViewModel("5", "Okari", "A ló túloldalán", "3 years ago", "4:09", String.Empty),
            new PlaylistItemViewModel("6", "Pullup", "A ló túloldalán", "3 years ago", "2:17", String.Empty),
            new PlaylistItemViewModel("7", "Habibi", "A ló túloldalán", "3 years ago", "2:41", String.Empty),
            new PlaylistItemViewModel("8", "tevagyazalány", "A ló túloldalán", "3 years ago", "2:18", String.Empty),
            new PlaylistItemViewModel("9", "Mind1", "A ló túloldalán", "3 years ago", "3:11", String.Empty),
            new PlaylistItemViewModel("10", "Lóerő", "A ló túloldalán", "3 years ago", "2:57", String.Empty),
        };
    }
}
