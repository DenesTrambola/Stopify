using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Base;
using System.ComponentModel;

namespace Stopify.Presentation.ViewModels.Main;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private readonly UIState _uiState;
    private readonly NavigationStore _navigationStore;

    #endregion

    #region Properties

    public bool? SidebarCollapseState => _uiState.SidebarCollapseState;

    public bool? NowPlayingCollapseState => _uiState.NowPlayingCollapseState;

    public bool QueueCollapseState => _uiState.QueueCollapseState;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    #endregion

    #region Constructors

    public MainViewModel(NavigationStore navigationStore, UIState uiState)
    {
        _uiState = uiState;
        _uiState.PropertyChanged += UIStatePropertyChanged;

        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    #endregion

    #region Event Handlers

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void UIStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_uiState.SidebarCollapseState):
                OnPropertyChanged(nameof(SidebarCollapseState));
                break;
            case nameof(_uiState.NowPlayingCollapseState):
                OnPropertyChanged(nameof(NowPlayingCollapseState));
                break;
            case nameof(_uiState.QueueCollapseState):
                OnPropertyChanged(nameof(QueueCollapseState));
                break;
        }
    }

    #endregion
}
