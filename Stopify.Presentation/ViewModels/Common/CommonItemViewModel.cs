using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Common;

public class CommonItemViewModel : ViewModelBase
{
    private string _title;
    private string _description;
    private string _imagePath;

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

    public CommonItemViewModel(string title, string description, string imagePath)
    {
        Title = title;
        Description = description;
        ImagePath = imagePath;
    }
}
