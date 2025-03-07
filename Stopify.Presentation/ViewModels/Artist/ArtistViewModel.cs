using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Artist;

public class ArtistViewModel : ViewModelBase
{
    private string _title;
    private string _monthlyListeners;
    private string _description;
    private ObservableCollection<CommonItemViewModel> _discographyItems;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string MonthlyListeners
    {
        get => _monthlyListeners;
        set => SetProperty(ref _monthlyListeners, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public IEnumerable<CommonItemViewModel> DiscographyItems => _discographyItems;

    public ArtistViewModel()
    {
        Title = "Azahriah";
        MonthlyListeners = "700,000";
        Description = "creator from hungary";

        _discographyItems = new ObservableCollection<CommonItemViewModel>
        {
            new CommonItemViewModel("ZHA MAJ DUR", "Latest Release · Single", String.Empty),
            new CommonItemViewModel("A ló túloldalán", "2022 · Album", String.Empty),
            new CommonItemViewModel("memento", "2023 · Album", String.Empty),
            new CommonItemViewModel("tripq", "2023 · EP", String.Empty),
            new CommonItemViewModel("silbak", "2022 · EP", String.Empty),
            new CommonItemViewModel("BAKPAKK", "2024 - Single", String.Empty),
            new CommonItemViewModel("skatulya I", "2024 · Album", String.Empty),
            new CommonItemViewModel("Puskás Aréna Live (2024)", "2024 · Album", String.Empty),
            new CommonItemViewModel("camouflage", "2021 · Album", String.Empty),
        };
    }
}
