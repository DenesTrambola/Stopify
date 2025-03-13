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
            new SidebarItemViewModel("Liked Songs", "Playlist · 75 songs", String.Empty),
            new SidebarItemViewModel("Coding Music Programming Lofi Songs", "Playlist · Soave", String.Empty),
            new SidebarItemViewModel("Azahriah", "Artist", String.Empty),
            new SidebarItemViewModel("GYM PHONK 2025 AGGRESSIVE WORKOUT MUSIC", "Playlist · Magic Records", String.Empty),
            new SidebarItemViewModel("tiktok gym edits 2025 workout music", "Playlist · Love Bedroom Pop", String.Empty),
            new SidebarItemViewModel("VILE PHONK", "Playlist · VILE MUSIC (IG: @vileplaylist)", String.Empty),
            new SidebarItemViewModel("Kutyaknakplaylist", "Playlist · KárojbossKrisztián", String.Empty),
            new SidebarItemViewModel("MILLIONAIRE MODE Viral Tiktok Songs", "Playlist · Sounds", String.Empty),
            new SidebarItemViewModel("aesthetic gym posing", "Playlist · _", String.Empty),
            new SidebarItemViewModel("YAKTAK", "Artist", String.Empty),
        };
    }

    #endregion
}
