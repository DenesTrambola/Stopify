using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Utilities.Behaviors.Home;

public static class ScrollerSizeChangeBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
        "Enable",
        typeof(bool),
        typeof(ScrollerSizeChangeBehavior),
        new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty StickyHeaderHeightProperty =
        DependencyProperty.RegisterAttached(
        "StickyHeaderHeight",
        typeof(double),
        typeof(ScrollerSizeChangeBehavior),
        new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region Getters/Setters

    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);

    public static double GetStickyHeaderHeight(DependencyObject obj) =>
        (double)obj.GetValue(StickyHeaderHeightProperty);
    public static void SetStickyHeaderHeight(DependencyObject obj, double value) =>
        obj.SetValue(StickyHeaderHeightProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ScrollViewer element) return;

        if ((bool)e.NewValue)
        {
            element.ScrollChanged += OnScrollChanged;
            element.Unloaded += DetachEvents;
        }
        else
        {
            element.ScrollChanged -= OnScrollChanged;
            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (sender is not ScrollViewer element) return;

        SetStickyHeaderHeight(element, element.VerticalOffset > 300 ? double.NaN : 0);
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not ScrollViewer element) return;

        element.ScrollChanged -= OnScrollChanged;
        element.Unloaded -= DetachEvents;

        SetEnable(element, false);
    }

    #endregion
}
