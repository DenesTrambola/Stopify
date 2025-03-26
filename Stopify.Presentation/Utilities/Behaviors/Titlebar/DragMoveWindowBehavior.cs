using System.Windows;
using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Behaviors.Titlebar;

public static class DragMoveWindowBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableDragProperty =
        DependencyProperty.RegisterAttached(
            "EnableDrag",
            typeof(bool),
            typeof(DragMoveWindowBehavior),
            new PropertyMetadata(false, OnEnableDragChanged));

    #endregion

    #region Getters/Setters

    public static void SetEnableDrag(UIElement element, bool value) =>
        element.SetValue(EnableDragProperty, value);

    public static bool GetEnableDrag(UIElement element) =>
        (bool)element.GetValue(EnableDragProperty);

    #endregion

    #region Event Handlers

    private static void OnEnableDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

        if ((bool)e.NewValue)
            element.MouseDown += OnMouseDown;
        else
            element.MouseDown -= OnMouseDown;
    }

    private static void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            Window window = Window.GetWindow(sender as DependencyObject);
            window?.DragMove();
        }
    }

    #endregion
}
