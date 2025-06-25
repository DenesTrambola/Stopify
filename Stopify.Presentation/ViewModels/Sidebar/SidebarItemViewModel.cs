using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;
using System.ComponentModel;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarItemViewModel : ViewModelBase
{
    #region Fields

    private int _playlistSongQuantity = 0;

    private bool _isPlaying = false;
    private UIState _uiState;

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

    public bool SidebarCollapseState
    {
        get => _uiState.SidebarCollapseState;
        set
        {
            if (_uiState.SidebarCollapseState != value)
            {
                _uiState.SidebarCollapseState = value;
                OnPropertyChanged(nameof(SidebarCollapseState));
            }
        }
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

    public SidebarItemViewModel(UIState uiState,
                                string playlistTitle,
                                string playlistType,
                                string playlistImagePath,
                                string? playlistAuthor = null,
                                int playlistSongQuantity = default)
    {
        _uiState = uiState;
        _uiState.PropertyChanged += UIStatePropertyChanged;

        PlaylistTitle = playlistTitle;
        PlaylistType = playlistType;
        PlaylistImagePath = playlistImagePath;
        PlaylistAuthor = playlistAuthor ?? string.Empty;
        PlaylistSongQuantity = playlistSongQuantity;
    }

    #endregion

    #region Event Handlers

    private void UIStatePropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        OnPropertyChanged(nameof(SidebarCollapseState));

    #endregion
}
