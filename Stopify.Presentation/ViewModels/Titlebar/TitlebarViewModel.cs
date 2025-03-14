using Stopify.Presentation.Utilities.Commands.Base;
using Stopify.Presentation.Utilities.Commands.Titlebar;
using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Stopify.Presentation.ViewModels.Titlebar;

public class TitlebarViewModel : ViewModelBase
{
    #region Fields

    private char _avatarPlaceholder;
    private string _username;
    private bool _isOptionsMenuOpen;
    private ObservableCollection<MenuItemViewModel> _optionsMenuItems;

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

    public bool IsOptionsMenuOpen
    {
        get => _isOptionsMenuOpen;
        set => SetProperty(ref _isOptionsMenuOpen, value);
    }

    public ObservableCollection<MenuItemViewModel> OptionsMenuItems => _optionsMenuItems;

    #endregion

    #region Commands

    public ICommand ToggleOptionsMenuCommand { get; }

    public ICommand NavigateBackCommand { get; }
    public ICommand NavigateForwardCommand { get; }

    #endregion

    #region Constructors

    public TitlebarViewModel()
    {
        AvatarPlaceholder = 'D';
        Username = "Dénes Trambola";
        IsOptionsMenuOpen = false;

        _optionsMenuItems = new ObservableCollection<MenuItemViewModel>
        {
            new MenuItemViewModel("File", new RelayCommand(() => MessageBox.Show("File clicked"))),
            new MenuItemViewModel("Edit", new RelayCommand(() => MessageBox.Show("Edit clicked"))),
            new MenuItemViewModel("View", new RelayCommand(() => MessageBox.Show("View clicked"))),
            new MenuItemViewModel("Playback", new RelayCommand(() => MessageBox.Show("Playback clicked"))),
            new MenuItemViewModel("Help", new RelayCommand(() => MessageBox.Show("Help clicked")))
        };

        ToggleOptionsMenuCommand = new ToggleOptionsCommand(this);
        NavigateBackCommand = new NavigateBackCommand();
        NavigateForwardCommand = new NavigateForwardCommand();
    }

    #endregion
}
