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
    private bool _canNavigateBack;
    private bool _canNavigateForward;

    private ObservableCollection<MenuItemViewModel> _optionsMenuItems;

    private double _titlebarActualWidth;
    private double _friendActivityBtnWidth;
    private double _newsBtnWidth;
    private double _searchBarWidth;
    private CornerRadius _searchbarBorderRadius;
    private double _searchbarInputWidth;
    private double _searchbarBrowseWidth;
    private string _searchbarInput;
    private double _searchbarLineWidth;
    private double _searchbarTextWidth;
    private double _searchBarActualWidth;

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

    public ObservableCollection<MenuItemViewModel> OptionsMenuItems => _optionsMenuItems;

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
        AvatarPlaceholder = 'D';
        Username = "Dénes Trambola";

        IsOptionsMenuOpen = false;
        CanNavigateBack = true;
        CanNavigateForward = false;

        _optionsMenuItems = new ObservableCollection<MenuItemViewModel>
        {
            new MenuItemViewModel("File", new RelayCommand(() => MessageBox.Show("File clicked"))),
            new MenuItemViewModel("Edit", new RelayCommand(() => MessageBox.Show("Edit clicked"))),
            new MenuItemViewModel("View", new RelayCommand(() => MessageBox.Show("View clicked"))),
            new MenuItemViewModel("Playback", new RelayCommand(() => MessageBox.Show("Playback clicked"))),
            new MenuItemViewModel("Help", new RelayCommand(() => MessageBox.Show("Help clicked")))
        };

        FriendActivityBtnWidth = double.NaN;
        NewsBtnWidth = double.NaN;
        SearchBarWidth = 0;
        SearchBtnBorderRadius = new CornerRadius(30);
        SearchbarInputWidth = 0;
        SearchbarBrowseWidth = 0;
        SearchbarInput = string.Empty;
        SearchbarLineWidth = 0;
        SearchbarTextWidth = 0;

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
