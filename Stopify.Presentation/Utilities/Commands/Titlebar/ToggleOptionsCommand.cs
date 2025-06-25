using Stopify.Presentation.Utilities.Commands.Base;
using Stopify.Presentation.ViewModels.Titlebar;

namespace Stopify.Presentation.Utilities.Commands.Titlebar;

public class ToggleOptionsCommand : CommandBase
{
    private readonly TitlebarViewModel _titlebarViewModel;

    public override void Execute(object? parameter)
    {
        _titlebarViewModel.IsOptionsMenuOpen = !_titlebarViewModel.IsOptionsMenuOpen;
    }

    public ToggleOptionsCommand(TitlebarViewModel titlebarViewModel)
    {
        _titlebarViewModel = titlebarViewModel;
    }
}
