using System.Windows;

namespace Stopify.Presentation.Utilities.Behaviors.Main;

public static class MainViewSizeChangeBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
        "Enable",
        typeof(bool),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty NowPlayingCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingCollapseState",
        typeof(bool?),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata(null));

    public static readonly DependencyProperty SidebarCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "SidebarCollapseState",
        typeof(bool?),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata(null));

    public static readonly DependencyProperty QueueCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "QueueCollapseState",
        typeof(bool),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata(false));

    public static readonly DependencyProperty QueueHeightProperty =
        DependencyProperty.RegisterAttached(
        "QueueHeight",
        typeof(double),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata((double)0));

    public static readonly DependencyProperty QueueWidthProperty =
        DependencyProperty.RegisterAttached(
        "QueueWidth",
        typeof(double),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata((double)0));

    public static readonly DependencyProperty NowPlayingHeightProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingHeight",
        typeof(double),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata((double)0));

    public static readonly DependencyProperty NowPlayingWidthProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingWidth",
        typeof(double),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata((double)0));

    public static readonly DependencyProperty SidebarWidthProperty =
        DependencyProperty.RegisterAttached(
        "SidebarWidth",
        typeof(double),
        typeof(MainViewSizeChangeBehavior),
        new PropertyMetadata((double)0));

    #endregion

    #region Getters/Setters

    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);

    public static bool? GetNowPlayingCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(NowPlayingCollapseStateProperty);
    public static void SetNowPlayingCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(NowPlayingCollapseStateProperty, value);

    public static bool? GetSidebarCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(SidebarCollapseStateProperty);
    public static void SetSidebarCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(SidebarCollapseStateProperty, value);

    public static bool GetQueueCollapseState(DependencyObject obj) =>
        (bool)obj.GetValue(QueueCollapseStateProperty);
    public static void SetQueueCollapseState(DependencyObject obj, bool value) =>
        obj.SetValue(QueueCollapseStateProperty, value);

    public static double GetQueueHeight(DependencyObject obj) =>
        (double)obj.GetValue(QueueHeightProperty);
    public static void SetQueueHeight(DependencyObject obj, double value) =>
        obj.SetValue(QueueHeightProperty, value);

    public static double GetQueueWidth(DependencyObject obj) =>
        (double)obj.GetValue(QueueWidthProperty);
    public static void SetQueueWidth(DependencyObject obj, double value) =>
        obj.SetValue(QueueWidthProperty, value);

    public static double GetNowPlayingHeight(DependencyObject obj) =>
        (double)obj.GetValue(NowPlayingHeightProperty);
    public static void SetNowPlayingHeight(DependencyObject obj, double value) =>
        obj.SetValue(NowPlayingHeightProperty, value);

    public static double GetNowPlayingWidth(DependencyObject obj) =>
        (double)obj.GetValue(NowPlayingWidthProperty);
    public static void SetNowPlayingWidth(DependencyObject obj, double value) =>
        obj.SetValue(NowPlayingWidthProperty, value);

    public static double GetSidebarWidth(DependencyObject obj) =>
        (double)obj.GetValue(SidebarWidthProperty);
    public static void SetSidebarWidth(DependencyObject obj, double value) =>
        obj.SetValue(SidebarWidthProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window element) return;

        if ((bool)e.NewValue)
        {
            element.SizeChanged += OnSizeChanged;
            element.Unloaded += DetachEvents;
        }
        else
        {
            element.SizeChanged -= OnSizeChanged;
            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not Window element) return;

        bool? nowPlayingCollapseState = GetNowPlayingCollapseState(element);
        bool? sidebarCollapseState = GetSidebarCollapseState(element);
        bool queueCollapseState = GetQueueCollapseState(element);

        if ((nowPlayingCollapseState == false || nowPlayingCollapseState == null) && !queueCollapseState)
            SetQueueHeight(element, GetNowPlayingHeight(element) + 7);

        if (element.ActualWidth >= 1250)
        {
            if (nowPlayingCollapseState == false || nowPlayingCollapseState == null)
                SetNowPlayingWidth(element, 350);
            if (!queueCollapseState)
                SetQueueWidth(element, 350);
        }
        else if (element.ActualWidth >= 1100)
        {
            if (sidebarCollapseState == null)
                SetSidebarWidth(element, 280);
            if (nowPlayingCollapseState == false || nowPlayingCollapseState == null)
                SetNowPlayingWidth(element, 281);
            if (!queueCollapseState)
                SetQueueWidth(element, 281);
        }
        else
        {
            if (sidebarCollapseState == null)
                SetSidebarWidth(element, 81);
            if (nowPlayingCollapseState == null)
                SetNowPlayingWidth(element, 281);
            if (!queueCollapseState)
                SetQueueWidth(element, 281);
        }
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not Window element) return;

        element.SizeChanged -= OnSizeChanged;
        element.Unloaded -= DetachEvents;

        SetEnable(element, false);
    }

    #endregion
}
