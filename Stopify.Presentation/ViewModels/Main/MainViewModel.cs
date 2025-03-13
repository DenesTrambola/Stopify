using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Home;

namespace Stopify.Presentation.ViewModels.Main;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private ViewModelBase _currentViewModel;

    private readonly HomeViewModel _homeViewModel;

    #endregion

    #region Properties

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    #endregion

    #region Constructors

    public MainViewModel(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;

        CurrentViewModel = _homeViewModel;
    }

    #endregion
}
