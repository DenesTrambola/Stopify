using Stopify.Presentation.ViewModels.Base;
using System.Windows.Input;

namespace Stopify.Presentation.ViewModels.Titlebar;

public class MenuItemViewModel : ViewModelBase
{
    public string Header { get; set; }
    public ICommand Command { get; set; }

    public MenuItemViewModel(string header, ICommand command)
    {
        Header = header;
        Command = command;
    }
}
