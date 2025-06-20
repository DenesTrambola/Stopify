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

    private bool _isOptionsMenuOpen = false;
    private bool _canNavigateBack = true;
    private bool _canNavigateForward = false;

    private double _titlebarActualWidth = 0;
    private double _friendActivityBtnWidth = double.NaN;
    private double _newsBtnWidth = double.NaN;
    private double _searchBarWidth = 0;
    private CornerRadius _searchbarBorderRadius = new(30);
    private double _searchbarInputWidth = 0;
    private double _searchbarBrowseWidth = 0;
    private string _searchbarInput = string.Empty;
    private double _searchbarLineWidth = 0;
    private double _searchbarTextWidth = 0;
    private double _searchBarActualWidth = 0;

    private char _avatarPlaceholder = 'D';
    private string _username = "Dénes Trambola";

    private ObservableCollection<MenuItemViewModel> _optionsMenuItems;

    #endregion

    #region Properties

    public bool IsOptionsMenuOpen
    {
        get => _isOptionsMenuOpen;
        set => SetProperty(ref _isOptionsMenuOpen, value);
    }

    public bool CanNavigateBack
    {
        get => _canNavigateBack;
        set => SetProperty(ref _canNavigateBack, value);
    }

    public bool CanNavigateForward
    {
        get => _canNavigateForward;
        set => SetProperty(ref _canNavigateForward, value);
    }

    public double TitlebarActualWidth
    {
        get => _titlebarActualWidth;
        set => SetProperty(ref _titlebarActualWidth, value);
    }

    public double FriendActivityBtnWidth
    {
        get => _friendActivityBtnWidth;
        set => SetProperty(ref _friendActivityBtnWidth, value);
    }

    public double NewsBtnWidth
    {
        get => _newsBtnWidth;
        set => SetProperty(ref _newsBtnWidth, value);
    }

    public double SearchBarWidth
    {
        get => _searchBarWidth;
        set => SetProperty(ref _searchBarWidth, value);
    }

    public CornerRadius SearchBtnBorderRadius
    {
        get => _searchbarBorderRadius;
        set => SetProperty(ref _searchbarBorderRadius, value);
    }

    public double SearchbarInputWidth
    {
        get => _searchbarInputWidth;
        set => SetProperty(ref _searchbarInputWidth, value);
    }

    public double SearchbarBrowseWidth
    {
        get => _searchbarBrowseWidth;
        set => SetProperty(ref _searchbarBrowseWidth, value);
    }

    public string SearchbarInput
    {
        get => _searchbarInput;
        set => SetProperty(ref _searchbarInput, value);
    }

    public double SearchbarLineWidth
    {
        get => _searchbarLineWidth;
        set => SetProperty(ref _searchbarLineWidth, value);
    }

    public double SearchbarTextWidth
    {
        get => _searchbarTextWidth;
        set => SetProperty(ref _searchbarTextWidth, value);
    }

    public double SearchBarActualWidth
    {
        get => _searchBarActualWidth;
        set => SetProperty(ref _searchBarActualWidth, value);
    }

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

    public ObservableCollection<MenuItemViewModel> OptionsMenuItems => _optionsMenuItems;

    #endregion

    #region Commands

    public ICommand ToggleOptionsMenuCommand { get; }

    public ICommand NavigateBackCommand { get; }
    public ICommand NavigateForwardCommand { get; }

    public ICommand NavigateHomeCommand { get; }
    public ICommand ToggleSearchbarCommand { get; }
    public ICommand BrowseCommand { get; }

    public ICommand NavigateNewsCommand { get; }
    public ICommand ToggleFriendActivityCommand { get; }

    #endregion

    #region Constructors

    public TitlebarViewModel()
    {
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
        NavigateHomeCommand = new NavigateHomeCommand();
        ToggleSearchbarCommand = new ToggleSearchbarCommand(this);
        BrowseCommand = new TitlebarBrowseCommand();
        NavigateNewsCommand = new NavigateNewsCommand();
        ToggleFriendActivityCommand = new ToggleFriendActivityCommand();
    }

    #endregion
}
