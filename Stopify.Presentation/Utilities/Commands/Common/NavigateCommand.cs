using Stopify.Presentation.Utilities.Commands.Base;
using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.Utilities.Commands.Common;

public class NavigateCommand<TViewModel>(NavigationStore navigationStore, Func<TViewModel> viewModelFactory) : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore = navigationStore;
    private readonly Func<TViewModel> _viewModelFactory = viewModelFactory;

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = _viewModelFactory.Invoke();
    }
}
