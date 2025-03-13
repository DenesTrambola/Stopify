using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Titlebar;

public class TitlebarViewModel : ViewModelBase
{
    private char _avatarPlaceholder;
    private string _username;

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

    public TitlebarViewModel()
    {
        AvatarPlaceholder = 'D';
        Username = "Dénes Trambola";
    }
}
