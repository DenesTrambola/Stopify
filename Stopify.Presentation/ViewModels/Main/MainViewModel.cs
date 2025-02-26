using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Home;

namespace Stopify.Presentation.ViewModels.Main;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    private readonly HomeViewModel _homeViewModel;

    public MainViewModel(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;

        CurrentViewModel = _homeViewModel;
    }
}
