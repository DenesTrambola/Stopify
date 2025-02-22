using System.Windows;
using System.Windows.Media.Animation;

namespace Stopify.Presentation.Utilities.Animations;

public static class WidthAnimations
{
    public static void BeginWidthAnimation(UIElement element, double fromWidth, double toWidth, double durationSeconds)
    {
        DoubleAnimation widthAnimation = new DoubleAnimation
        {
            From = fromWidth,
            To = toWidth,
            Duration = TimeSpan.FromSeconds(durationSeconds),
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };
        widthAnimation.Completed += (s, e) =>
        {
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Width = double.NaN;
            }
        };
        element.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
    }

    public static void ResetWidthAnimation(UIElement element, double originalWidth, double durationSeconds)
    {
        DoubleAnimation widthResetAnimation = new DoubleAnimation
        {
            To = originalWidth,
            Duration = TimeSpan.FromSeconds(durationSeconds),
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };
        element.BeginAnimation(FrameworkElement.WidthProperty, widthResetAnimation);
    }
}
