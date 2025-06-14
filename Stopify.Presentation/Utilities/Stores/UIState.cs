using System.ComponentModel;

namespace Stopify.Presentation.Utilities.Stores;

public class UIState : INotifyPropertyChanged
{
    #region Fields

    private bool? _sidebarCollapseState;
    private bool? _nowPlayingCollapseState;
    private bool _queueCollapseState = true;

    #endregion

    #region Properties

    public bool? SidebarCollapseState
    {
        get => _sidebarCollapseState;
        set
        {
            if (_sidebarCollapseState != value)
            {
                _sidebarCollapseState = value;
                OnPropertyChanged(nameof(SidebarCollapseState));
            }
        }
    }

    public bool? NowPlayingCollapseState
    {
        get => _nowPlayingCollapseState;
        set
        {
            if (_nowPlayingCollapseState != value)
            {
                _nowPlayingCollapseState = value;
                OnPropertyChanged(nameof(NowPlayingCollapseState));
            }
        }
    }

    public bool QueueCollapseState
    {
        get => _queueCollapseState;
        set
        {
            if (_queueCollapseState != value)
            {
                _queueCollapseState = value;
                OnPropertyChanged(nameof(QueueCollapseState));
            }
        }
    }

    #endregion

    #region Events & Event Handlers

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
