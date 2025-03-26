using Stopify.Presentation.Utilities.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Titlebar;

public static class NavigationButtonBehavior
{
    #region Dependency Properties

    private static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool?),
            typeof(NavigationButtonBehavior),
            new PropertyMetadata(null, OnEnableChanged));

    #endregion

    #region Getters/Setters

    public static void SetEnable(UIElement element, bool? value) =>
        element.SetValue(EnableProperty, value);
    public static bool? GetEnable(UIElement element) =>
        (bool?)element.GetValue(EnableProperty);

    #endregion

    #region Event Handlers

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Label element) return;

        element.MouseEnter -= OnMouseEnter;
        element.MouseLeave -= OnMouseLeave;

        if ((bool?)e.NewValue == true)
            ColorAnimations.AnimateForegroundColor(element, element.Foreground, Colors.DarkGray, .02);
        else if ((bool?)e.NewValue == false)
            ColorAnimations.AnimateForegroundColor(element, element.Foreground, Color.FromRgb(50, 50, 50), .02);

        element.MouseEnter += OnMouseEnter;
        element.MouseLeave += OnMouseLeave;
    }

    private static void OnMouseEnter(object sender, MouseEventArgs args)
    {
        if (sender is not Label element) return;

        Mouse.OverrideCursor = GetEnable(element) == true ? Cursors.Hand : Cursors.No;
    }

    private static void OnMouseLeave(object sender, MouseEventArgs args) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    #endregion
}
