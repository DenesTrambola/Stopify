using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Titlebar;

public class TitlebarViewModel : ViewModelBase
{
    #region Fields

    private char _avatarPlaceholder;
    private string _username;

    #endregion

    #region Properties

    public char AvatarPlaceholder
    {
        get => _avatarPlaceholder;
        set => SetProperty(ref _avatarPlaceholder, value);
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    #endregion

    #region Constructors

    public TitlebarViewModel()
    {
        AvatarPlaceholder = 'D';
        Username = "Dénes Trambola";
    }

    #endregion
}
