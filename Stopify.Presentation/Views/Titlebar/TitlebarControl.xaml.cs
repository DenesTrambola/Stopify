using Stopify.Presentation.ViewModels.Titlebar;
using System.Windows.Controls;

namespace Stopify.Presentation.Views.Titlebar;

public partial class TitlebarControl : UserControl
{
    public TitlebarControl()
    {
        InitializeComponent();
        DataContext = new TitlebarViewModel();
    }
}
