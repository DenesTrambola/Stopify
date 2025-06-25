using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Stopify.Presentation.Utilities.Stores;

public class UIState : INotifyPropertyChanged
{
    #region Fields

    private bool _sidebarCollapseState = true;
    private bool _queueCollapseState = true;
    private bool _nowPlayingCollapseState = false;

    #endregion

    #region Properties

    public bool SidebarCollapseState
    {
        get => _sidebarCollapseState;
        set
        {
            if (_sidebarCollapseState != value)
            {
                _sidebarCollapseState = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
    }

    public bool NowPlayingCollapseState
    {
        get => _nowPlayingCollapseState;
        set
        {
            if (_nowPlayingCollapseState != value)
            {
                _nowPlayingCollapseState = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Events & Event Handlers

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
