using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeRecentPlaysItemViewModel : ViewModelBase
{
    #region Fields

    private string _title;
    private string _imagePath;

    #endregion

    #region Properties

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

    #endregion

    #region Constructors

    public HomeRecentPlaysItemViewModel(string title, string imagePath)
    {
        Title = title;
        ImagePath = imagePath;
    }

    #endregion
}
