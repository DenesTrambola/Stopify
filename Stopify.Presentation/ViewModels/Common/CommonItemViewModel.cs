using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Common;

public class CommonItemViewModel(string title, string description, string imagePath) : ViewModelBase
{
    #region Fields

    private bool _isPlaying = false;

    private string _title = title;
    private string _description = description;
    private string _imagePath = imagePath;

    #endregion

    #region Properties

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    #endregion
}
