using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarViewModel : ViewModelBase
{
    #region Fields

    private ObservableCollection<SidebarItemViewModel> _items;

    #endregion

    #region Properties

    public IEnumerable<SidebarItemViewModel> Items => _items;

    #endregion

    #region Constructor

    public SidebarViewModel()
    {
        _items = new ObservableCollection<SidebarItemViewModel>
        {
            new SidebarItemViewModel("Liked Songs", "Playlist", string.Empty, string.Empty, 80),
            new SidebarItemViewModel("Coding Music Programming Lofi Songs", "Playlist", string.Empty, "programmer"),
            new SidebarItemViewModel("Azahriah", "Artist", string.Empty),
            new SidebarItemViewModel("GYM PHONK 2025 AGGRESSIVE WORKOUT MUSIC", "Playlist", string.Empty, "Magic Records"),
            new SidebarItemViewModel("tiktok gym edits 2025 workout music", "Playlist", string.Empty, "Love Bedroom Pop"),
            new SidebarItemViewModel("VILE PHONK", "Playlist", string.Empty, "VILE MUSIC (IG: @vileplaylist)"),
            new SidebarItemViewModel("Kutyaknakplaylist", "Playlist", string.Empty, "KárojbossKrisztián"),
            new SidebarItemViewModel("MILLIONAIRE MODE Viral Tiktok Songs", "Playlist", string.Empty, "Sounds"),
            new SidebarItemViewModel("aesthetic gym posing", "Playlist", string.Empty, "_"),
            new SidebarItemViewModel("YAKTAK", "Artist", string.Empty),
        };
    }

    #endregion
}
