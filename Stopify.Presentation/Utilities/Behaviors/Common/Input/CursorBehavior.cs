using System.Windows;
using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Input;

public static class CursorBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty = DependencyProperty.RegisterAttached(
        "Enable",
        typeof(bool),
        typeof(CursorBehavior),
        new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty HoverCursorProperty = DependencyProperty.RegisterAttached(
        "HoverCursor",
        typeof(Cursor),
        typeof(CursorBehavior),
        new PropertyMetadata(Cursors.Hand));

    #endregion

    #region Getters/Setters

    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);
    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);

    public static void SetHoverCursor(DependencyObject obj, Cursor value) =>
        obj.SetValue(HoverCursorProperty, value);
    public static Cursor GetHoverCursor(DependencyObject obj) =>
        (Cursor)obj.GetValue(HoverCursorProperty);

    #endregion

    #region Event Handlers

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

        if ((bool)e.NewValue)
        {
            element.MouseEnter += Element_MouseEnter;
            element.MouseLeave += Element_MouseLeave;
        }
        else
        {
            element.MouseEnter -= Element_MouseEnter;
            element.MouseLeave -= Element_MouseLeave;
        }
    }

    private static void Element_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement element) return;

        Cursor cursor = GetHoverCursor(element);
        Mouse.OverrideCursor = cursor ?? Cursors.Hand;
    }

    private static void Element_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    #endregion
}
