using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Main;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private readonly NavigationStore _navigationStore;

    #endregion

    #region Properties

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    #endregion

    #region Constructors

    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    #endregion

    #region Event Handlers

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    #endregion
}
