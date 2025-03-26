using Stopify.Presentation.Utilities.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class ForegroundAnimationBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableAnimateOnHoverProperty =
        DependencyProperty.RegisterAttached(
            "EnableAnimateOnHover",
            typeof(bool),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(false, OnEnableAnimateOnHoverChanged));

    public static readonly DependencyProperty EnableAnimateOnFocusProperty =
        DependencyProperty.RegisterAttached(
            "EnableAnimateOnFocus",
            typeof(bool),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(false, OnEnableAnimateOnFocusChanged));

    public static readonly DependencyProperty IsClickedProperty =
        DependencyProperty.RegisterAttached(
            "IsClicked",
            typeof(bool),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(false));

    public static readonly DependencyProperty AnimateInColorProperty =
        DependencyProperty.RegisterAttached(
            "AnimateInColor",
            typeof(Color),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty AnimateOutColorProperty =
        DependencyProperty.RegisterAttached(
            "AnimateOutColor",
            typeof(Color),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.RegisterAttached(
            "Duration",
            typeof(double),
            typeof(ForegroundAnimationBehavior),
            new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static void SetEnableAnimateOnHover(UIElement element, bool value) =>
        element.SetValue(EnableAnimateOnHoverProperty, value);
    public static bool GetEnableAnimateOnHover(UIElement element) =>
        (bool)element.GetValue(EnableAnimateOnHoverProperty);

    public static void SetEnableAnimateOnFocus(UIElement element, bool value) =>
        element.SetValue(EnableAnimateOnFocusProperty, value);
    public static bool GetEnableAnimateOnFocus(UIElement element) =>
        (bool)element.GetValue(EnableAnimateOnFocusProperty);

    public static void SetIsClicked(UIElement element, bool value) =>
        element.SetValue(IsClickedProperty, value);
    public static bool GetIsClicked(UIElement element) =>
        (bool)element.GetValue(IsClickedProperty);

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
            element.MouseEnter += OnMouseEnter;
            element.MouseLeave += OnMouseLeave;
        }
        else
        {
            element.MouseEnter -= OnMouseEnter;
            element.MouseLeave -= OnMouseLeave;
        }
    }

    private static void OnMouseEnter(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        if (!GetIsClicked(element))
            AnimateIn(element);
    }

    private static void OnMouseLeave(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        if (!GetIsClicked(element))
            AnimateOut(element);
    }

    private static void OnEnableAnimateOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
        {
            if ((bool)e.NewValue)
            {
                element.GotFocus += (s, args) => AnimateIn(element);
                element.LostFocus += (s, args) => AnimateOut(element);
            }
            else
            {
                element.GotFocus -= (s, args) => AnimateIn(element);
                element.LostFocus -= (s, args) => AnimateOut(element);
            }
        }
    }

    #endregion

    #region Methods

    private static void AnimateIn(Button element) =>
        ColorAnimations.AnimateForegroundColor(element, element.Foreground, GetAnimateInColor(element), GetDuration(element));

    private static void AnimateOut(Button element) =>
        ColorAnimations.AnimateForegroundColor(element, element.Foreground, GetAnimateOutColor(element), GetDuration(element));

    #endregion
}
