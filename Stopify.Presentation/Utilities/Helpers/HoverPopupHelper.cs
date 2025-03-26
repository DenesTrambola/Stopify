using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Stopify.Presentation.Utilities.Helpers;

public static class HoverPopupHelper
{
    private static MainView _mainView = (MainView)Application.Current.MainWindow;

    public static void DisplayPopup(FrameworkElement element, PlacementMode placement, params TextBlock[] texts)
    {
        var popup = _mainView.BtnPopup;

        popup.PlacementTarget = element;
        popup.Placement = placement;

        _mainView.PopupItem.PopupStackPanel.Children.Clear();
        foreach (var text in texts)
            _mainView.PopupItem.PopupStackPanel.Children.Add(text);

        popup.IsOpen = true;
    }

    public static void HidePopup()
    {
        _mainView.BtnPopup.Visibility = Visibility.Collapsed;
        _mainView.BtnPopup.IsOpen = false;
    }
}
