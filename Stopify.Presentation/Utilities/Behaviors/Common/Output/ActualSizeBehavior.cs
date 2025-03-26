using System.Windows;

namespace Stopify.Presentation.Utilities.Behaviors.Common.Output;

public static class ActualSizeBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.RegisterAttached(
                "ActualWidth",
                typeof(double?),
                typeof(ActualSizeBehavior),
                new PropertyMetadata(null, OnActualWidthChanged));

    #endregion

    #region Getters/Setters

    public static void SetActualWidth(UIElement element, double value) =>
        element.SetValue(ActualWidthProperty, value);

    public static double GetActualWidth(UIElement element) =>
        (double)element.GetValue(ActualWidthProperty);

    #endregion

    #region Event Handlers

    private static void OnActualWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element) return;

        if (e.NewValue is not null)
            element.SizeChanged += SizeChanged;
        else
            element.SizeChanged -= SizeChanged;
    }

    private static void SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not FrameworkElement element) return;

        SetActualWidth(element, element.ActualWidth);
    }

    #endregion
}
