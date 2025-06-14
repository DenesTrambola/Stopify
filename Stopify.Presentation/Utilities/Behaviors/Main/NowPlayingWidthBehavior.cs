using System.Windows;

namespace Stopify.Presentation.Utilities.Behaviors.Main;

public static class NowPlayingWidthBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty NowPlayingCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingCollapseState",
        typeof(bool?),
        typeof(NowPlayingWidthBehavior),
        new PropertyMetadata(null, OnNowPlayingCollapseStateChanged));

    public static readonly DependencyProperty QueueCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "QueueCollapseState",
        typeof(bool),
        typeof(NowPlayingWidthBehavior),
        new PropertyMetadata(false));

    public static readonly DependencyProperty NowPlayingWidthProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingWidth",
        typeof(double),
        typeof(NowPlayingWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty QueueWidthProperty =
        DependencyProperty.RegisterAttached(
        "QueueWidth",
        typeof(double),
        typeof(NowPlayingWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region Getters/Setters

    public static bool? GetNowPlayingCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(NowPlayingCollapseStateProperty);
    public static void SetNowPlayingCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(NowPlayingCollapseStateProperty, value);

    public static bool GetQueueCollapseState(DependencyObject obj) =>
        (bool)obj.GetValue(QueueCollapseStateProperty);
    public static void SetQueueCollapseState(DependencyObject obj, bool value) =>
        obj.SetValue(QueueCollapseStateProperty, value);

    public static double GetNowPlayingWidth(DependencyObject obj) =>
        (double)obj.GetValue(NowPlayingWidthProperty);
    public static void SetNowPlayingWidth(DependencyObject obj, double value) =>
        obj.SetValue(NowPlayingWidthProperty, value);

    public static double GetQueueWidth(DependencyObject obj) =>
        (double)obj.GetValue(QueueWidthProperty);
    public static void SetQueueWidth(DependencyObject obj, double value) =>
        obj.SetValue(QueueWidthProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnNowPlayingCollapseStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window element) return;

        if ((bool?)e.NewValue == true)
        {
            SetNowPlayingWidth(element, 0);
            if (GetQueueCollapseState(element))
                SetQueueWidth(element, 0);
        }
        else
        {
            if (element.ActualWidth >= 1250)
                SetNowPlayingWidth(element, 350);
            else if (element.ActualWidth >= 1100)
                SetNowPlayingWidth(element, 281);
            else
                SetNowPlayingWidth(element, 281);
        }
    }

    #endregion
}
