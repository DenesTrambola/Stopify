using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarItemViewModel : ViewModelBase
{
    #region Fields

    private int _playlistSongQuantity = 0;

    private bool _isPlaying = false;

    private string _playlistImagePath = string.Empty;
    private string _playlistAuthor = string.Empty;
    private string _playlistTitle = string.Empty;
    private string _playlistType = string.Empty;

    #endregion

    #region Properties

    public int PlaylistSongQuantity
    {
        get => _playlistSongQuantity;
        set => SetProperty(ref _playlistSongQuantity, value);
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public string PlaylistImagePath
    {
        get => _playlistImagePath;
        set => SetProperty(ref _playlistImagePath, value);
    }

    public string PlaylistAuthor
    {
        get => _playlistAuthor;
        set => SetProperty(ref _playlistAuthor, value);
    }

    public string PlaylistTitle
    {
        get => _playlistTitle;
        set => SetProperty(ref _playlistTitle, value);
    }

    public string PlaylistType
    {
        get => _playlistType;
        set => SetProperty(ref _playlistType, value);
    }

    #endregion

    #region Constructors

    public SidebarItemViewModel(string playlistTitle, string playlistType, string playlistImagePath, string? playlistAuthor = null, int playlistSongQuantity = default)
    {
        PlaylistTitle = playlistTitle;
        PlaylistType = playlistType;
        PlaylistImagePath = playlistImagePath;
        PlaylistAuthor = playlistAuthor ?? string.Empty;
        PlaylistSongQuantity = playlistSongQuantity;
    }

    #endregion
}
