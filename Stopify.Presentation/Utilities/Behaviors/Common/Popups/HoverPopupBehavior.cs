using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Popups;

public static class HoverPopupBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableHoverPopupOnMouseEnterProperty =
        DependencyProperty.RegisterAttached(
            "EnableHoverPopupOnMouseEnter",
            typeof(bool),
            typeof(HoverPopupBehavior),
            new PropertyMetadata(false, OnHoverPopupOnMouseEnterChanged));

    public static readonly DependencyProperty EnableHoverPopupOnMouseMoveProperty =
        DependencyProperty.RegisterAttached(
            "EnableHoverPopupOnMouseMove",
            typeof(bool),
            typeof(HoverPopupBehavior),
            new PropertyMetadata(false, OnHoverPopupOnMouseMoveChanged));

    public static readonly DependencyProperty TooltipTextProperty =
        DependencyProperty.RegisterAttached(
            "TooltipText",
            typeof(string),
            typeof(HoverPopupBehavior),
            new PropertyMetadata(string.Empty));

    #endregion

    #region Getters/Setters

    public static void SetEnableHoverPopupOnMouseEnter(UIElement element, string value) =>
        element.SetValue(EnableHoverPopupOnMouseEnterProperty, value);
    public static string GetEnableHoverPopupOnMouseEnter(UIElement element) =>
        (string)element.GetValue(EnableHoverPopupOnMouseEnterProperty);

    public static void SetEnableHoverPopupOnMouseMove(UIElement element, bool value) =>
        element.SetValue(EnableHoverPopupOnMouseMoveProperty, value);
    public static bool GetEnableHoverPopupOnMouseMove(UIElement element) =>
        (bool)element.GetValue(EnableHoverPopupOnMouseMoveProperty);

    public static void SetTooltipText(UIElement element, string value) =>
        element.SetValue(TooltipTextProperty, value);
    public static string GetTooltipText(UIElement element) =>
        (string)element.GetValue(TooltipTextProperty);

    #endregion

    #region Event Handlers

    private static void OnHoverPopupOnMouseEnterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

        if ((bool)e.NewValue)
        {
            if ((bool)e.NewValue)
            {
                element.MouseEnter += (s, args) => DisplayHoverPopup(element);
                element.MouseLeave += (s, args) => HoverPopupHelper.HidePopup();
            }
            else
            {
                element.MouseEnter -= (s, args) => DisplayHoverPopup(element);
                element.MouseLeave -= (s, args) => HoverPopupHelper.HidePopup();
            }
        }
    }

    private static void OnHoverPopupOnMouseMoveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

        element.MouseMove += (s, args) => DisplayHoverPopup(element);
    }

    #endregion

    #region Methods

    private static void DisplayHoverPopup(FrameworkElement element)
    {
        TextBlock textBlock = new()
        {
            Text = GetTooltipText(element),
            Foreground = Brushes.White,
            FontWeight = FontWeights.SemiBold,
            FontSize = 14,
        };

        HoverPopupHelper.DisplayPopup(element, PlacementMode.Bottom, textBlock);
    }

    #endregion
}
