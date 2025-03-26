using Stopify.Presentation.Utilities.Animations;
using System.Windows;
using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class ScaleAnimationBehavior
{
    #region Dependency Properties

    private static readonly DependencyProperty EnableScaleOnHoverProperty = DependencyProperty.RegisterAttached(
        "EnableScaleOnHover",
        typeof(bool),
        typeof(ScaleAnimationBehavior),
        new PropertyMetadata(false, OnEnableScaleOnHoverChanged));

    private static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.RegisterAttached(
        "ScaleFactor",
        typeof(double),
        typeof(ScaleAnimationBehavior),
        new PropertyMetadata(1.0));

    private static readonly DependencyProperty DurationProperty = DependencyProperty.RegisterAttached(
        "Duration",
        typeof(double),
        typeof(ScaleAnimationBehavior),
        new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static bool GetEnableScaleOnHover(DependencyObject obj) =>
        (bool)obj.GetValue(EnableScaleOnHoverProperty);
    public static void SetEnableScaleOnHover(DependencyObject obj, bool value) =>
        obj.SetValue(EnableScaleOnHoverProperty, value);

    public static double GetScaleFactor(DependencyObject obj) =>
        (double)obj.GetValue(ScaleFactorProperty);
    public static void SetScaleFactor(DependencyObject obj, double value) =>
        obj.SetValue(ScaleFactorProperty, value);

    public static double GetDuration(DependencyObject obj) =>
        (double)obj.GetValue(DurationProperty);
    public static void SetDuration(DependencyObject obj, double value) =>
        obj.SetValue(DurationProperty, value);

    #endregion

    #region Event Handlers

    private static void OnEnableScaleOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

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

    private static void OnMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement element) return;

        ScaleAnimations.BeginScaleAnimation(element, GetScaleFactor(element), GetDuration(element));
    }

    private static void OnMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement element) return;

        ScaleAnimations.ResetScaleAnimation(element, GetDuration(element));
    }

    #endregion
}
