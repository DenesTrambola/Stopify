using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeRecentPlaysItemViewModel : ViewModelBase
{
    private string _title;
    private string _imagePath;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public HomeRecentPlaysItemViewModel(string title, string imagePath)
    {
        Title = title;
        ImagePath = imagePath;
    }
}
