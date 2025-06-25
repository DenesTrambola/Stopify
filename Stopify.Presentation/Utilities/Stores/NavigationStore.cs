using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.Utilities.Stores;

public class NavigationStore
{
    private ViewModelBase _currentViewModel = null!;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            if (_currentViewModel != value)
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
    }
    public event Action? CurrentViewModelChanged;
    protected virtual void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
