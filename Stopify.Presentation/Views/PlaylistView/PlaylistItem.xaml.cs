using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.PlaylistView;

public partial class PlaylistItem : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private bool _isSelected = false;
    private bool _isPlaying = false;
    private TextBlock _popupText = new();
    private bool _isSaved = false;

    public PlaylistItem()
    {
        InitializeComponent();

        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.Foreground = Brushes.White;
        _popupText.Background = Brushes.Transparent;
        _popupText.FontSize = 14;
    }


    // Playlist Item

    private void PlaylistItem_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var element = (UserControl)sender;

        if (element.ActualWidth >= 724)
        {
            DateBtn.Width = double.NaN;
            DateColumn.Width = new GridLength(1, GridUnitType.Star);
        }
        else if (element.ActualWidth >= 494)
        {
            AlbumBtn.Width = double.NaN;
            DateBtn.Width = 0;

            AlbumColumn.Width = new GridLength(1, GridUnitType.Star);
            DateColumn.Width = new GridLength(0, GridUnitType.Auto);
        }
        else
        {
            AlbumBtn.Width = 0;
            DateBtn.Width = 0;

            AlbumColumn.Width = new GridLength(0, GridUnitType.Auto);
            DateColumn.Width = new GridLength(0, GridUnitType.Auto);
        }
    }

    private void PlaylistItemBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (!_isSelected)
            PlaylistItemBorder.Background = new SolidColorBrush(Color.FromArgb(100, 128, 128, 128));

        Number.Width = 0;
        PlayBtn.Width = double.NaN;
        SaveBtn.Visibility = Visibility.Visible;
        OptionsBtn.Visibility = Visibility.Visible;
        AlbumBtn.Foreground = Brushes.White;
    }

    private void PlaylistItemBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        if (!_isSelected)
            PlaylistItemBorder.Background = new SolidColorBrush(Color.FromArgb(0, 128, 128, 128));

        Number.Width = 29;
        PlayBtn.Width = 0;
        SaveBtn.Visibility = Visibility.Hidden;
        OptionsBtn.Visibility = Visibility.Hidden;
        AlbumBtn.Foreground = Brushes.DarkGray;
    }

    private void PlaylistItemBtn_GotFocus(object sender, RoutedEventArgs e)
    {
        PlaylistItemBorder.Background = new SolidColorBrush(Color.FromArgb(200, 128, 128, 128));
        _isSelected = true;
    }

    private void PlaylistItemBtn_LostFocus(object sender, RoutedEventArgs e)
    {
        PlaylistItemBorder.Background = new SolidColorBrush(Color.FromArgb(0, 128, 128, 128));
        _isSelected = false;
    }

    private void PlaylistItemBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e) { }


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = _isPlaying ? "Pause" : "Play Cry Me A River by Azahriah";
        HoverPopupHelper.PopupAppear(_mainWindow, PlayBtn, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e) =>
        HoverPopupHelper.PopupDisappear(_mainWindow);

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isPlaying)
        {
            _popupText.Text = "Play Cry Me A River from Azahriah";
            PlayBtn.Content = "\uf04b";
            PlayBtn.FontSize = 13;
            _isPlaying = false;
        }
        else
        {
            _popupText.Text = "Pause";
            PlayBtn.Content = "\uf04c";
            PlayBtn.FontSize = 15;
            _isPlaying = true;
        }
    }


    // Album

    private void AlbumBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(AlbumBtn, 1.02, .05);
    }

    private void AlbumBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(AlbumBtn, .05);
    }

    private void AlbumBtn_Click(object sender, RoutedEventArgs e)
    {
        //_mainWindow.MainFrame.Navigate(new PlaylistView());
    }


    // Save

    private void SaveBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        _popupText.Text = _isSaved ? "Remove from Liked Songs" : "Save to Liked Songs";
        HoverPopupHelper.PopupAppear(_mainWindow, SaveBtn, PlacementMode.Top, _popupText);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
        }
    }

    private void SaveBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        HoverPopupHelper.PopupDisappear(_mainWindow);

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
            SaveText.Text = "+";
            SaveText.Margin = new Thickness(1, 0, 0, 0);
            SaveBorder.Background = Brushes.Transparent;
            SaveBorder.BorderThickness = new Thickness(2);
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
            _popupText.Text = "Save to Your Library";
            _isSaved = false;
        }
        else
        {
            SaveText.Text = "\uf00c";
            SaveText.Margin = new Thickness(3, 2, 0, 0);
            SaveBorder.Background = new SolidColorBrush(Color.FromRgb(30, 215, 96));
            SaveBorder.BorderThickness = new Thickness(0);
            SaveBorder.BorderBrush = Brushes.Transparent;
            SaveText.Foreground = Brushes.Black;
            _popupText.Text = "Remove from Your Library";
            _isSaved = true;
        }
    }


    // More Options

    private void OptionsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(OptionsBtn, 1.03, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.White, .1);
        _popupText.Text = "More options for Cry Me A River";
        HoverPopupHelper.PopupAppear(_mainWindow, OptionsBtn, PlacementMode.Top, _popupText);
    }

    private void OptionsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(OptionsBtn, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.DarkGray, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void OptionsBtn_Click(object sender, RoutedEventArgs e) { }
}
