using Stopify.Presentation.Helpers.Animations;
using Stopify.Presentation.Helpers.Utilities;
using Stopify.Presentation.Views.ArtistView;
using Stopify.Presentation.Views.PlayerView;
using Stopify.Presentation.Views.PlaylistView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Stopify.Presentation.Views.NowPlayingView;

public partial class NowPlayingViewMain : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private PlayerViewMain _player = new();
    private TextBlock _popupText = new();
    private bool _isSaved = false;
    private bool _isFollowing = false;

    public NowPlayingViewMain()
    {
        InitializeComponent();

        _popupText.Foreground = Brushes.White;
        _popupText.Background = Brushes.Transparent;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }


    // Now Playing

    private void NowPlaying_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (NowPlayingPanel.ActualWidth >= 350)
        {
            if (_isSaved)
                SaveText.Margin = new Thickness(4, 3.5, 0, 0);
            else
                SaveText.Margin = new Thickness(2, .7, 0, 0);
            SaveBtn.Height = 35;
            SaveBorder.Width = 21;
            SaveText.FontSize = 14;
        }
        else
        {
            if (_isSaved)
                SaveText.Margin = new Thickness(3, 3, 0, 0);
            else
                SaveText.Margin = new Thickness(1.5, .7, 0, 0);
            SaveBtn.Height = 30;
            SaveBorder.Width = 16.5;
            SaveText.FontSize = 11;
        }
    }


    // Header Title

    private void Underline_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;

        if (sender is not Button button)
            return;

        if (button.Content is TextBlock textBlock)
            textBlock.TextDecorations = TextDecorations.Underline;
        else if (button.Content is string text)
        {
            var newTextBlock = new TextBlock { Text = text, TextDecorations = TextDecorations.Underline };
            button.Content = newTextBlock;
        }
    }

    private void Underline_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;

        if (sender is not Button button)
            return;

        if (button.Content is TextBlock textBlock)
            textBlock.TextDecorations = null;
    }

    private void HeaderTitleBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new ArtistViewMain());


    // More Options

    private void OptionsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(OptionsBtn, 1.03, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.White, .1);
        _popupText.Text = "More options for Azahriah";
        PopupHelper.PopupAppear(_mainWindow, OptionsBtn, PlacementMode.Top, _popupText);
    }

    private void OptionsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(OptionsBtn, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.DarkGray, .1);
        PopupHelper.PopupDisappear(_mainWindow);
    }

    private void OptionsBtn_Click(object sender, RoutedEventArgs e) { }


    // Close

    private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(CloseBorder, CloseBorder.Background, Color.FromRgb(31, 31, 31), .1);
        ColorAnimations.AnimateForegroundColor(CloseText, CloseText.Foreground, Colors.LightGray, .1);
        _popupText.Text = "Close";
        PopupHelper.PopupAppear(_mainWindow, CloseBtn, PlacementMode.Top, _popupText);
    }

    private void CloseBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateBackgroundColor(CloseBorder, CloseBorder.Background, Color.FromRgb(18, 18, 18), .1);
        ColorAnimations.AnimateForegroundColor(CloseText, CloseText.Foreground, Colors.DarkGray, .1);
        PopupHelper.PopupDisappear(_mainWindow);
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
        _mainWindow.NowPlayingCollapsed = true;
        _mainWindow.UpdateNowPlaying();
    }


    // Song Image

    private void SongImgBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void SongImgBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void SongImgBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new PlaylistViewMain());


    // Song Info

    private void SongTitleBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new PlaylistViewMain());

    private void SongArtistBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new ArtistViewMain());


    // Add to Liked Songs

    private void SaveBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(SaveBtn, 1.03, .1);
        _popupText.Text = _isSaved ? "Remove from Liked Songs" : "Save to Liked Songs";
        PopupHelper.PopupAppear(_mainWindow, SaveBtn, PlacementMode.Top, _popupText);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
        }
    }

    private void SaveBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(SaveBtn, .1);
        PopupHelper.PopupDisappear(_mainWindow);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = Brushes.DarkGray;
            SaveText.Foreground = Brushes.DarkGray;
        }
    }

    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isSaved)
        {
            if (NowPlayingPanel.ActualWidth < 350)
                SaveText.Margin = new Thickness(1.5, .7, 0, 0);
            else
                SaveText.Margin = new Thickness(2, .7, 0, 0);
            SaveText.Text = "+";
            SaveBorder.Background = Brushes.Transparent;
            SaveBorder.BorderThickness = new Thickness(2);
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
            _popupText.Text = "Save to Liked Songs";
            _isSaved = false;
        }
        else
        {
            if (NowPlayingPanel.ActualWidth < 350)
                SaveText.Margin = new Thickness(3, 3, 0, 0);
            else
                SaveText.Margin = new Thickness(4, 3.5, 0, 0);
            SaveText.Text = "\uf00c";
            SaveBorder.Background = new SolidColorBrush(Color.FromRgb(30, 215, 96));
            SaveBorder.BorderThickness = new Thickness(0);
            SaveBorder.BorderBrush = Brushes.Transparent;
            SaveText.Foreground = Brushes.Black;
            _popupText.Text = "Remove from Liked Songs";
            _isSaved = true;
        }
    }


    // About Artist

    private void AboutBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void AboutBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void AboutBtn_Click(object sender, RoutedEventArgs e) { }

    private void ArtistBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new ArtistViewMain());


    // Follow

    private void FollowBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(FollowBtn, 1.03, .1);
        FollowBorder.BorderBrush = Brushes.LightGray;
    }

    private void FollowBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(FollowBtn, .1);
        FollowBorder.BorderBrush = Brushes.DarkGray;
    }

    private void FollowBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isFollowing)
        {
            FollowText.Text = "Follow";
            FollowText2.Text = "Follow";
            _isFollowing = false;
        }
        else
        {
            FollowText.Text = "Following";
            FollowText2.Text = "Following";
            _isFollowing = true;
        }
    }

    private void FollowBtn2_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(FollowBtn2, 1.03, .1);
        FollowBorder2.BorderBrush = Brushes.LightGray;
    }

    private void FollowBtn2_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(FollowBtn2, .1);
        FollowBorder2.BorderBrush = Brushes.DarkGray;
    }


    // Show All

    private void ShowAll_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not Control button)
            return;

        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(button, 1.01, .1);
    }

    private void ShowAll_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not Control button)
            return;

        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(button, .1);
    }

    private void ShowAll_Click(object sender, RoutedEventArgs e) { }


    // Open Queue

    private void OpenQueue_Click(object sender, RoutedEventArgs e)
    {
        var heightIncreaseAnimation = new DoubleAnimation
        {
            From = 0,
            To = _mainWindow.NowPlaying.ActualHeight + 7,
            Duration = TimeSpan.FromSeconds(.3),
            EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
        };
        _mainWindow.NowPlayingQueue.BeginAnimation(HeightProperty, heightIncreaseAnimation);

        PlayerViewMain player = new();
        player.NowPlayingBtn.Content = "\uf106";
        _mainWindow.NowPlayingCollapsed = false;
    }
}
