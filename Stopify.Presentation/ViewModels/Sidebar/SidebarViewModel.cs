using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarViewModel : ViewModelBase
{
    #region Fields

    private bool _isPlaylistsFilter = false;
    private bool _isArtistsFilter = false;
    private bool _isSearching = false;
    private bool _isExpanded = false;

    private double _width = 0;

    private string _searchText = string.Empty;

    private readonly ObservableCollection<SidebarItemViewModel> _items;

    private readonly UIState _uiState;

    #endregion

    #region Properties

    public bool IsPlaylistsFilter
    {
        get => _isPlaylistsFilter;
        set => SetProperty(ref _isPlaylistsFilter, value);
    }

    public bool IsArtistsFilter
    {
        get => _isArtistsFilter;
        set => SetProperty(ref _isArtistsFilter, value);
    }

    public bool IsSearching
    {
        get => _isSearching;
        set => SetProperty(ref _isSearching, value);
    }

    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetProperty(ref _isExpanded, value);
    }

    public bool SidebarCollapseState
    {
        get => _uiState.SidebarCollapseState;
        set
        {
            if (_uiState.SidebarCollapseState != value)
            {
                _uiState.SidebarCollapseState = value;
                OnPropertyChanged();
            }
        }
    }

    public double Width
    {
        get => _width;
        set
        {
            SetProperty(ref _width, value);
            IsExpanded = _width >= 280;
        }
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<SidebarItemViewModel> Items => _items;

    #endregion

    #region Constructor

    public SidebarViewModel(UIState uiState)
    {
        _uiState = uiState;
        _uiState.PropertyChanged += UIStatePropertyChanged;

        _items = new ObservableCollection<SidebarItemViewModel>
        {
            new SidebarItemViewModel(_uiState, "Liked Songs", "Playlist", string.Empty, string.Empty, 80),
            new SidebarItemViewModel(_uiState, "Coding Music Programming Lofi Songs", "Playlist", string.Empty, "programmer"),
            new SidebarItemViewModel(_uiState, "Azahriah", "Artist", string.Empty),
            new SidebarItemViewModel(_uiState, "GYM PHONK 2025 AGGRESSIVE WORKOUT MUSIC", "Playlist", string.Empty, "Magic Records"),
            new SidebarItemViewModel(_uiState, "tiktok gym edits 2025 workout music", "Playlist", string.Empty, "Love Bedroom Pop"),
            new SidebarItemViewModel(_uiState, "VILE PHONK", "Playlist", string.Empty, "VILE MUSIC (IG: @vileplaylist)"),
            new SidebarItemViewModel(_uiState, "Kutyaknakplaylist", "Playlist", string.Empty, "KárojbossKrisztián"),
            new SidebarItemViewModel(_uiState, "MILLIONAIRE MODE Viral Tiktok Songs", "Playlist", string.Empty, "Sounds"),
            new SidebarItemViewModel(_uiState, "aesthetic gym posing", "Playlist", string.Empty, "_"),
            new SidebarItemViewModel(_uiState, "YAKTAK", "Artist", string.Empty),
        };
    }

    #endregion

    #region Event Handlers

    private void UIStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_uiState.SidebarCollapseState):
                OnPropertyChanged(nameof(SidebarCollapseState));
                break;
                // other state properties can be added here if needed
        }
    }

    #endregion
}
