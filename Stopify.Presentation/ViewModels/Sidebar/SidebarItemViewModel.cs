using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Sidebar;

public class SidebarItemViewModel : ViewModelBase
{
    #region Fields

    private string _title;
    private string _description;
    private string _image;

    #endregion

    #region Properties

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

    #endregion

    #region Constructors

    public SidebarItemViewModel(string title, string description, string image)
    {
        Title = title;
        Description = description;
        Image = image;
    }

    #endregion
}
