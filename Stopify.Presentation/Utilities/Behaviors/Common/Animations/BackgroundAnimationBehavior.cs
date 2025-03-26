using Stopify.Presentation.Utilities.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class BackroundAnimationBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableAnimateOnHoverProperty =
        DependencyProperty.RegisterAttached(
            "EnableAnimateOnHover",
            typeof(bool),
            typeof(BackroundAnimationBehavior),
            new PropertyMetadata(false, OnEnableAnimateOnHoverChanged));

    public static readonly DependencyProperty AnimateInColorProperty =
        DependencyProperty.RegisterAttached(
            "AnimateInColor",
            typeof(Color),
            typeof(BackroundAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty AnimateOutColorProperty =
        DependencyProperty.RegisterAttached(
            "AnimateOutColor",
            typeof(Color),
            typeof(BackroundAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.RegisterAttached(
            "Duration",
            typeof(double),
            typeof(BackroundAnimationBehavior),
            new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static void SetEnableAnimateOnHover(UIElement element, bool value) =>
        element.SetValue(EnableAnimateOnHoverProperty, value);
    public static bool GetEnableAnimateOnHover(UIElement element) =>
        (bool)element.GetValue(EnableAnimateOnHoverProperty);

    public static void SetAnimateInColor(UIElement element, Color value) =>
        element.SetValue(AnimateInColorProperty, value);
    public static Color GetAnimateInColor(UIElement element) =>
        (Color)element.GetValue(AnimateInColorProperty);

    public static void SetAnimateOutColor(UIElement element, Color value) =>
        element.SetValue(AnimateOutColorProperty, value);
    public static Color GetAnimateOutColor(UIElement element) =>
        (Color)element.GetValue(AnimateOutColorProperty);

    public static void SetDuration(UIElement element, double value) =>
        element.SetValue(DurationProperty, value);
    public static double GetDuration(UIElement element) =>
        (double)element.GetValue(DurationProperty);

    #endregion

    #region Event Handlers

    private static void OnEnableAnimateOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
        {
            element.MouseEnter += (s, args) => AnimateIn(element);
            element.MouseLeave += (s, args) => AnimateOut(element);
        }
        else
        {
            element.MouseEnter -= (s, args) => AnimateIn(element);
            element.MouseLeave -= (s, args) => AnimateOut(element);
        }
    }

    #endregion

    #region Methods

    private static void AnimateIn(Button element) =>
        ColorAnimations.AnimateBackgroundColor(element, element.Background, GetAnimateInColor(element), GetDuration(element));

    private static void AnimateOut(Button element) =>
        ColorAnimations.AnimateBackgroundColor(element, element.Background, GetAnimateOutColor(element), GetDuration(element));

    #endregion
}
