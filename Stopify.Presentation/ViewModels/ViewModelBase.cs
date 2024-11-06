using Stopify.Presentation.Helpers.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Stopify.Presentation.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isLoading;
    private string _errorMessage;
    private readonly Dictionary<string, ICommand> _commands = new();

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void HandleError(Exception ex) =>
        ErrorMessage = ex.Message;

    protected ICommand GetOrCreateCommand(string commandName, Action execute, Func<bool> canExecute = null)
    {
        if (!_commands.ContainsKey(commandName))
            _commands[commandName] = new RelayCommand(execute, canExecute);

        return _commands[commandName];
    }

    public virtual void Initialize() { }
    public virtual void Dispose() { }
}
