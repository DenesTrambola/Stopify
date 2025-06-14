using System.Windows;

namespace Stopify.Presentation.Utilities.Behaviors.Main;

public static class SidebarWidthBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty SidebarCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "SidebarCollapseState",
        typeof(bool?),
        typeof(SidebarWidthBehavior),
        new PropertyMetadata(null, OnSidebarCollapseStateChanged));

    public static readonly DependencyProperty NowPlayingCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingCollapseState",
        typeof(bool?),
        typeof(SidebarWidthBehavior),
        new PropertyMetadata(null));

    public static readonly DependencyProperty SidebarWidthProperty =
        DependencyProperty.RegisterAttached(
        "SidebarWidth",
        typeof(double),
        typeof(SidebarWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region Getters/Setters

    public static bool? GetSidebarCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(SidebarCollapseStateProperty);
    public static void SetSidebarCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(SidebarCollapseStateProperty, value);

    public static bool? GetNowPlayingCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(NowPlayingCollapseStateProperty);
    public static void SetNowPlayingCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(NowPlayingCollapseStateProperty, value);

    public static double GetSidebarWidth(DependencyObject obj) =>
        (double)obj.GetValue(SidebarWidthProperty);
    public static void SetSidebarWidth(DependencyObject obj, double value) =>
        obj.SetValue(SidebarWidthProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnSidebarCollapseStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window element) return;

        if ((bool?)e.NewValue == true)
        {
            SetSidebarWidth(element, 81);
            SetNowPlayingCollapseState(element, null);
        }
        else
        {
            SetSidebarWidth(element, 280);
            SetNowPlayingCollapseState(element, true);
        }
    }

    #endregion
}
