using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Utilities.Behaviors.Titlebar;

public class CloseWindowBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty CloseOnClickProperty = DependencyProperty.RegisterAttached(
        "CloseOnClick",
        typeof(bool),
        typeof(CloseWindowBehavior),
        new PropertyMetadata(false, OnCloseOnClickChanged));

    #endregion

    #region Getters/Setters

    public static void SetCloseOnClick(DependencyObject obj, bool value) =>
        obj.SetValue(CloseOnClickProperty, value);
    public static bool GetCloseOnClick(DependencyObject obj) =>
        (bool)obj.GetValue(CloseOnClickProperty);

    #endregion

    #region Event Handlers

    private static void OnCloseOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
            element.Click += Close;
        else
            element.Click -= Close;
    }

    private static void Close(object sender, RoutedEventArgs e) =>
        Window.GetWindow((Button)sender).Close();

    #endregion
}
