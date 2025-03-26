using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Utilities.Behaviors.Titlebar;

public static class WindowStateBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty StateOnClickProperty = DependencyProperty.RegisterAttached(
        "StateOnClick",
        typeof(WindowState),
        typeof(WindowStateBehavior),
        new PropertyMetadata(WindowState.Normal, OnStateOnClickChanged));

    #endregion

    #region Getters/Setters

    public static void SetStateOnClick(DependencyObject obj, WindowState value) =>
        obj.SetValue(StateOnClickProperty, value);
    public static WindowState GetStateOnClick(DependencyObject obj) =>
        (WindowState)obj.GetValue(StateOnClickProperty);

    #endregion

    #region Event Handlers

    private static void OnStateOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((WindowState)e.NewValue == WindowState.Minimized)
            element.Click += Minimize;
        else if ((WindowState)e.NewValue == WindowState.Maximized)
            element.Click += Maximize;
        else
        {
            element.Click -= Minimize;
            element.Click -= Maximize;
        }
    }

    private static void Minimize(object sender, RoutedEventArgs e) =>
        Window.GetWindow((Button)sender).WindowState = WindowState.Minimized;

    private static void Maximize(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow((Button)sender);
        window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    #endregion
}
