using Stopify.Presentation.Utilities.Enums.Playlist;
using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Playlist;

public class PlaylistHeaderViewModel : ViewModelBase
{
    #region Fields

    private PlaylistSortType _sortType = PlaylistSortType.Off;

    #endregion

    #region Properties

    public PlaylistSortType SortType
    {
        get => _sortType;
        set => SetProperty(ref _sortType, value);
    }

    #endregion
}
