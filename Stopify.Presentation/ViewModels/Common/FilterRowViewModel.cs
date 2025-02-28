using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Common;

public class FilterRowViewModel : ViewModelBase
{
    private string _title;
    private ObservableCollection<CommonItemViewModel> _items;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public IEnumerable<CommonItemViewModel> Items => _items;

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
}
