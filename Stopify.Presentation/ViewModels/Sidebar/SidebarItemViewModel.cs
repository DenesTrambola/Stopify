using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarItemViewModel : ViewModelBase
{
    private string _title;
    private string _description;
    private string _image;

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

    public string Image
    {
        get => _image;
        set => SetProperty(ref _image, value);
    }

    public SidebarItemViewModel(string title, string description, string image)
    {
        Title = title;
        Description = description;
        Image = image;
    }
}
