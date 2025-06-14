using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Stopify.Presentation.Utilities.Behaviors.Main;

public static class QueueWidthBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty QueueCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "QueueCollapseState",
        typeof(bool),
        typeof(QueueWidthBehavior),
        new PropertyMetadata(false, OnQueueCollapseStateChanged));

    public static readonly DependencyProperty NowPlayingCollapseStateProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingCollapseState",
        typeof(bool?),
        typeof(QueueWidthBehavior),
        new PropertyMetadata(null));

    public static readonly DependencyProperty QueueWidthProperty =
        DependencyProperty.RegisterAttached(
        "QueueWidth",
        typeof(double),
        typeof(QueueWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty QueueHeightProperty =
        DependencyProperty.RegisterAttached(
        "QueueHeight",
        typeof(double),
        typeof(QueueWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty NowPlayingWidthProperty =
        DependencyProperty.RegisterAttached(
        "NowPlayingWidth",
        typeof(double),
        typeof(QueueWidthBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty QueueProperty =
        DependencyProperty.RegisterAttached(
        "Queue",
        typeof(Border),
        typeof(QueueWidthBehavior),
        new PropertyMetadata(null));

    #endregion

    #region Getters/Setters

    public static bool GetQueueCollapseState(DependencyObject obj) =>
        (bool)obj.GetValue(QueueCollapseStateProperty);
    public static void SetQueueCollapseState(DependencyObject obj, bool value) =>
        obj.SetValue(QueueCollapseStateProperty, value);

    public static bool? GetNowPlayingCollapseState(DependencyObject obj) =>
        (bool?)obj.GetValue(NowPlayingCollapseStateProperty);
    public static void SetNowPlayingCollapseState(DependencyObject obj, bool? value) =>
        obj.SetValue(NowPlayingCollapseStateProperty, value);

    public static double GetQueueWidth(DependencyObject obj) =>
        (double)obj.GetValue(QueueWidthProperty);
    public static void SetQueueWidth(DependencyObject obj, double value) =>
        obj.SetValue(QueueWidthProperty, value);

    public static double GetQueueHeight(DependencyObject obj) =>
        (double)obj.GetValue(QueueHeightProperty);
    public static void SetQueueHeight(DependencyObject obj, double value) =>
        obj.SetValue(QueueHeightProperty, value);

    public static double GetNowPlayingWidth(DependencyObject obj) =>
        (double)obj.GetValue(NowPlayingWidthProperty);
    public static void SetNowPlayingWidth(DependencyObject obj, double value) =>
        obj.SetValue(NowPlayingWidthProperty, value);

    public static Border GetQueue(DependencyObject obj) =>
        (Border)obj.GetValue(QueueProperty);
    public static void SetQueue(DependencyObject obj, Border value) =>
        obj.SetValue(QueueProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnQueueCollapseStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window element) return;

        double nowPlayingWidth = GetNowPlayingWidth(element);
        Border queue = GetQueue(element);

        if (queue is null) return;

        if ((bool)e.NewValue)
        {
            if (GetNowPlayingCollapseState(element) == true && GetQueueWidth(element) != 0)
                SetQueueWidth(element, 0);

            var heightIncreaseAnimation = new DoubleAnimation
            {
                From = nowPlayingWidth + 7,
                To = 0,
                Duration = TimeSpan.FromSeconds(.3),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop,
            };

            queue.BeginAnimation(FrameworkElement.HeightProperty, heightIncreaseAnimation);
            SetQueueWidth(element, 0);
        }
        else
        {
            var heightIncreaseAnimation = new DoubleAnimation
            {
                From = 0,
                To = nowPlayingWidth + 7,
                Duration = TimeSpan.FromSeconds(.3),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop,
            };

            queue.BeginAnimation(FrameworkElement.HeightProperty, heightIncreaseAnimation);
            queue.Height = nowPlayingWidth + 7;

            if (element.ActualWidth >= 1250)
                SetQueueWidth(element, 350);
            else if (element.ActualWidth >= 1100)
                SetQueueWidth(element, 281);
            else
                SetQueueWidth(element, 281);
        }
    }

    #endregion
}
