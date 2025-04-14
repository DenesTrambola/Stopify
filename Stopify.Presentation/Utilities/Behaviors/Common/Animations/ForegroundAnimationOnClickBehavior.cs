using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class ForegroundAnimationOnClickBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableOnClickProperty =
        DependencyProperty.RegisterAttached(
            "EnableOnClick",
            typeof(bool),
            typeof(ForegroundAnimationOnClickBehavior),
            new PropertyMetadata(false, OnEnableOnClickChanged));

    public static readonly DependencyProperty TargetColorProperty =
        DependencyProperty.RegisterAttached(
            "TargetColor",
            typeof(Color),
            typeof(ForegroundAnimationOnClickBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.RegisterAttached(
            "Duration",
            typeof(double),
            typeof(ForegroundAnimationOnClickBehavior),
            new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static bool GetEnableOnClick(UIElement element) =>
        (bool)element.GetValue(EnableOnClickProperty);
    public static void SetEnableOnClick(UIElement element, bool value) =>
        element.SetValue(EnableOnClickProperty, value);

    public static Color GetTargetColor(UIElement element) =>
        (Color)element.GetValue(TargetColorProperty);
    public static void SetTargetColor(UIElement element, Color value) =>
        element.SetValue(TargetColorProperty, value);

    public static double GetDuration(UIElement element) =>
        (double)element.GetValue(DurationProperty);
    public static void SetDuration(UIElement element, double value) =>
        element.SetValue(DurationProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
        {
            element.Click += AnimateOnClick;
            element.Unloaded += DetachEvents;
        }
        else
        {
            element.Click -= AnimateOnClick;
            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void AnimateOnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        if (ForegroundAnimationBehavior.GetIsClicked(element))
            ForegroundAnimationBehavior.SetIsClicked(element, false);
        else
            ForegroundAnimationBehavior.SetIsClicked(element, true);
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        element.Click -= AnimateOnClick;
        element.Unloaded -= DetachEvents;

        SetEnableOnClick(element, false);
    }

    #endregion
}
