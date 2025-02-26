using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Commands.Base;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? (_ => true);
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => _canExecute((T)parameter);

    public void Execute(object parameter) => _execute((T)parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
