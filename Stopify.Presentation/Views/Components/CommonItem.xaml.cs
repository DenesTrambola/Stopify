using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Stopify.Presentation.Views.Components;

public partial class CommonItem : UserControl
{
    private MainView _mainWindow = (MainView)Application.Current.MainWindow;
    TextBlock _popupText = new();
    private bool _isPlaying = false;

    public CommonItem()
    {
        InitializeComponent();

        _popupText.Background = Brushes.Transparent;
        _popupText.Foreground = Brushes.White;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }


    // General Item

    private void GeneralItemBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(GeneralItemBorder, GeneralItemBorder.Background, Color.FromRgb(31, 31, 31), .1);

        var moveUpAnimation = new DoubleAnimation
        {
            From = 20,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
        };

        var fadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
        };

        // Apply animations
        PlayBtn.BeginAnimation(Button.OpacityProperty, fadeInAnimation);
        PlayBtnTransform.BeginAnimation(TranslateTransform.YProperty, moveUpAnimation);
    }

    private void GeneralItemBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateBackgroundColor(GeneralItemBorder, GeneralItemBorder.Background, Color.FromRgb(18, 18, 18), .1);

        var moveDownAnimation = new DoubleAnimation
        {
            From = 0,
            To = 20,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
        };

        var fadeOutAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
        };

        // Apply animations
        PlayBtn.BeginAnimation(Button.OpacityProperty, fadeOutAnimation);
        PlayBtnTransform.BeginAnimation(TranslateTransform.YProperty, moveDownAnimation);
    }

    private void GeneralItemBtn_Click(object sender, RoutedEventArgs e)
    {
        //_mainWindow.MainFrame.Navigate(new PlaylistView.PlaylistView());
    }


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.03, .1);
        ColorAnimations.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(59, 228, 119), .1);
        _popupText.Text = _isPlaying ? "Pause" : "Play Azahriah";
        HoverPopupHelper.PopupAppear(_mainWindow, PlayBtn, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(PlayBtn, .1);
        ColorAnimations.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(30, 215, 96), .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
        e.Handled = true; // Prevent clicking the RecentBtn

        if (_isPlaying)
        {
            PlayIcon.Text = "\uf04b";
            PlayIcon.FontSize = 20;
            PlayIcon.Margin = new Thickness(2, 15, 0, 15);
            _popupText.Text = "Play";
            _isPlaying = false;
        }
        else
        {
            PlayIcon.Text = "\uf04c";
            PlayIcon.FontSize = 23;
            PlayIcon.Margin = new Thickness(0, 15, 0, 15);
            _popupText.Text = "Pause";
            _isPlaying = true;
        }
    }
}
