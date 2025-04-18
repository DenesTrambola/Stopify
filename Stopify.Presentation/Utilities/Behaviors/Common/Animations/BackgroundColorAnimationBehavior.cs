﻿using Stopify.Presentation.Utilities.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Animations;

public static class BackgroundColorAnimationBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableOnHoverProperty =
        DependencyProperty.RegisterAttached(
            "EnableOnHover",
            typeof(bool),
            typeof(BackgroundColorAnimationBehavior),
            new PropertyMetadata(false, OnEnableOnHoverChanged));

    public static readonly DependencyProperty InColorProperty =
        DependencyProperty.RegisterAttached(
            "InColor",
            typeof(Color),
            typeof(BackgroundColorAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty OutColorProperty =
        DependencyProperty.RegisterAttached(
            "OutColor",
            typeof(Color),
            typeof(BackgroundColorAnimationBehavior),
            new PropertyMetadata(Colors.Transparent));

    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.RegisterAttached(
            "Duration",
            typeof(double),
            typeof(BackgroundColorAnimationBehavior),
            new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static bool GetEnableOnHover(UIElement element) =>
        (bool)element.GetValue(EnableOnHoverProperty);
    public static void SetEnableOnHover(UIElement element, bool value) =>
        element.SetValue(EnableOnHoverProperty, value);

    public static Color GetInColor(UIElement element) =>
        (Color)element.GetValue(InColorProperty);
    public static void SetInColor(UIElement element, Color value) =>
        element.SetValue(InColorProperty, value);

    public static Color GetOutColor(UIElement element) =>
        (Color)element.GetValue(OutColorProperty);
    public static void SetOutColor(UIElement element, Color value) =>
        element.SetValue(OutColorProperty, value);

    public static double GetDuration(UIElement element) =>
        (double)element.GetValue(DurationProperty);
    public static void SetDuration(UIElement element, double value) =>
        element.SetValue(DurationProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Control element) return;

        if ((bool)e.NewValue)
        {
            element.MouseEnter += AnimateIn;
            element.MouseLeave += AnimateOut;
            element.Unloaded += DetachEvents;
        }
        else
        {
            element.MouseEnter -= AnimateIn;
            element.MouseLeave -= AnimateOut;
            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void AnimateIn(object sender, MouseEventArgs e)
    {
        if (sender is not Control element) return;

        ColorAnimations.AnimateBackground(element, GetInColor(element), GetDuration(element));
    }

    private static void AnimateOut(object sender, MouseEventArgs e)
    {
        if (sender is not Control element) return;

        ColorAnimations.AnimateBackground(element, GetOutColor(element), GetDuration(element));
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not Control element) return;

        element.MouseEnter -= AnimateIn;
        element.MouseLeave -= AnimateOut;
        element.Unloaded -= DetachEvents;

        SetEnableOnHover(element, false);
    }

    #endregion
}
