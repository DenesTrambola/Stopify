using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Stopify.Presentation.Utilities.Helpers;

public static class HoverPopupHelper
{
    public static void PopupAppear(MainWindow mainWindow, UIElement element, PlacementMode placement, params TextBlock[] texts)
    {
        var popup = mainWindow.BtnPopup;

        popup.PlacementTarget = element;
        popup.Placement = placement;

        mainWindow.PopupItem.PopupStackPanel.Children.Clear();
        foreach (var text in texts)
            mainWindow.PopupItem.PopupStackPanel.Children.Add(text);

        popup.IsOpen = true;
    }

    public static void PopupDisappear(MainWindow mainWindow)
    {
        mainWindow.BtnPopup.Visibility = Visibility.Collapsed;
        mainWindow.BtnPopup.IsOpen = false;
    }
}
