using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Common;

public class FilterRowViewModel : ViewModelBase
{
    #region Fields

    private string _title;

    private ObservableCollection<CommonItemViewModel> _items;

    #endregion

    #region Properties

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
        Title = title;

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
