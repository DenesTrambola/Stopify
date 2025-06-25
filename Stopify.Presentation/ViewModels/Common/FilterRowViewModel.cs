using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Common;

public class FilterRowViewModel : ViewModelBase
{
    #region Fields

    private bool _isFilteringAll = true;
    private bool _isFilteringMusic = false;
    private bool _isFilteringPodcasts = false;

    private string _title;

    private ObservableCollection<CommonItemViewModel> _items;

    #endregion

    #region Properties

    public bool IsFilteringAll
    {
        get => _isFilteringAll;
        set => SetProperty(ref _isFilteringAll, value);
    }

    public bool IsFilteringMusic
    {
        get => _isFilteringMusic;
        set => SetProperty(ref _isFilteringMusic, value);
    }

    public bool IsFilteringPodcasts
    {
        get => _isFilteringPodcasts;
        set => SetProperty(ref _isFilteringPodcasts, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public IEnumerable<CommonItemViewModel> Items => _items;

    #endregion

    #region Constructors

    public FilterRowViewModel(string title)
    {
        _title = title;

        _items = new ObservableCollection<CommonItemViewModel>
        {
            new CommonItemViewModel("Azahriah", "Artist", String.Empty),
            new CommonItemViewModel("DESH", "Artist", String.Empty),
            new CommonItemViewModel("YoungFly", "Artist", String.Empty),
            new CommonItemViewModel("Nessaj", "Streamer", String.Empty),
            new CommonItemViewModel("TheBigO", "Minecraft", String.Empty),
            new CommonItemViewModel("UborCraft", "uTuber", String.Empty),
            new CommonItemViewModel("Sajt32", "Minecraft", String.Empty),
            new CommonItemViewModel("XP", "Minecraft", String.Empty),
        };
    }

    #endregion
}
