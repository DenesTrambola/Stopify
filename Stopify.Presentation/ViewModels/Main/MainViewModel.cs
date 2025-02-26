using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Main;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public MainViewModel()
    {

    }
}
