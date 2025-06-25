using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeRecentPlaysItemViewModel(string title, bool isPlaying, string imagePath) : ViewModelBase
{
    #region Fields

    private string _title = title;
    private bool _isPlaying = isPlaying;
    private string _imagePath = imagePath;

    #endregion

    #region Properties

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    #endregion
}
