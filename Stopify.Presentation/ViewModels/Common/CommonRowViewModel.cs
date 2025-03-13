using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Common;

public class CommonRowViewModel : ViewModelBase
{
    #region Fields

    private string _category;
    private string _author;

    private ObservableCollection<CommonItemViewModel> _items;

    #endregion

    #region Properties

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public string Author
    {
        get => _author;
        set => SetProperty(ref _author, value);
    }

    public IEnumerable<CommonItemViewModel> Items => _items;

    #endregion

    #region Constructors

    public CommonRowViewModel(string? category = null, string? author = null)
    {
        Category = category ?? String.Empty;
        Author = author ?? String.Empty;

        _items = new ObservableCollection<CommonItemViewModel> {
            new CommonItemViewModel("Azahriah", "Artist", String.Empty),
            new CommonItemViewModel("DESH", "Artist", String.Empty),
            new CommonItemViewModel("YoungFly", "Artist", String.Empty),
            new CommonItemViewModel("Nessaj", "Streamer", String.Empty),
            new CommonItemViewModel("Baukó Attila", "Artist", String.Empty),
            new CommonItemViewModel("Azahriah", "Gamer", String.Empty),
            new CommonItemViewModel("Azahriah", "Bunko", String.Empty),
            new CommonItemViewModel("Azahriah", "Rozsda", String.Empty),
            new CommonItemViewModel("Azahriah", "Rozsda", String.Empty),
        };
    }

    #endregion
}
