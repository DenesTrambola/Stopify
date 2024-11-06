using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Stopify.Presentation.Helpers.Animations;

public static class ColorAnimations
{
    public static void AnimateBackgroundColor(Control target, Color startColor, Color endColor, double durationSeconds)
    {
        ColorAnimation colorAnimation = new ColorAnimation
        {
            From = startColor,
            To = endColor,
            Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
        };

        if (target.Background is SolidColorBrush backgroundBrush)
        {
            SolidColorBrush animatedBrush = new SolidColorBrush(backgroundBrush.Color);
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            target.Background = animatedBrush;
        }
    }

    public static void AnimateForegroundColor(Control target, Color startColor, Color endColor, double durationSeconds)
    {
        ColorAnimation colorAnimation = new ColorAnimation
        {
            From = startColor,
            To = endColor,
            Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
        };

        if (target.Foreground is SolidColorBrush foregroundBrush)
        {
            SolidColorBrush animatedBrush = new SolidColorBrush(foregroundBrush.Color);
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            target.Foreground = animatedBrush;
        }
    }

    public static void AnimateForegroundColor(TextBlock target, Brush startColorBrush, Color endColor, double durationSeconds)
    {
        ColorAnimation colorAnimation = new ColorAnimation
        {
            From = ((SolidColorBrush)startColorBrush).Color,
            To = endColor,
            Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
        };

        if (target.Foreground is SolidColorBrush foregroundBrush)
        {
            SolidColorBrush animatedBrush = new SolidColorBrush(foregroundBrush.Color);
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            target.Foreground = animatedBrush;
        }
    }

    public static void AnimateBackgroundColor(Control target, Brush startColorBrush, Color endColor, double durationSeconds) =>
        AnimateBackgroundColor(target, ((SolidColorBrush)startColorBrush).Color, endColor, durationSeconds);

    public static void AnimateForegroundColor(Control target, Brush startColorBrush, Color endColor, double durationSeconds) =>
        AnimateForegroundColor(target, ((SolidColorBrush)startColorBrush).Color, endColor, durationSeconds);

    public static void AnimateBackgroundColor(Border target, Brush startColorBrush, Color endColor, double durationSeconds)
    {
        ColorAnimation colorAnimation = new ColorAnimation
        {
            From = ((SolidColorBrush)startColorBrush).Color,
            To = endColor,
            Duration = new Duration(TimeSpan.FromSeconds(durationSeconds))
        };

        if (target.Background is SolidColorBrush backgroundBrush)
        {
            SolidColorBrush animatedBrush = new SolidColorBrush(backgroundBrush.Color);
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            target.Background = animatedBrush;
        }
    }
}
