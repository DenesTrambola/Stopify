using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class ForegroundAnimationOnClickBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableAnimateOnClickProperty =
        DependencyProperty.RegisterAttached(
            "EnableAnimateOnClick",
            typeof(bool),
            typeof(ForegroundAnimationOnClickBehavior),
            new PropertyMetadata(false, OnEnableAnimateOnClickChanged));

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

    public static void SetEnableAnimateOnClick(UIElement element, bool value) =>
        element.SetValue(EnableAnimateOnClickProperty, value);
    public static bool GetEnableAnimateOnClick(UIElement element) =>
        (bool)element.GetValue(EnableAnimateOnClickProperty);

    public static void SetTargetColor(UIElement element, Color value) =>
        element.SetValue(TargetColorProperty, value);
    public static Color GetTargetColor(UIElement element) =>
        (Color)element.GetValue(TargetColorProperty);

    public static void SetDuration(UIElement element, double value) =>
        element.SetValue(DurationProperty, value);
    public static double GetDuration(UIElement element) =>
        (double)element.GetValue(DurationProperty);

    #endregion

    #region Event Handlers

    private static void OnEnableAnimateOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
            element.Click += AnimateOnClick;
        else
            element.Click -= AnimateOnClick;
    }

    private static void AnimateOnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        if (ForegroundAnimationBehavior.GetIsClicked(element))
            ForegroundAnimationBehavior.SetIsClicked(element, false);
        else
            ForegroundAnimationBehavior.SetIsClicked(element, true);
    }

    #endregion
}
