using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.NowPlayingView;

public partial class NowPlayingItem : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private TextBlock _popupText = new();
    private bool _isPlaying = false;

    public NowPlayingItem()
    {
        InitializeComponent();

        _popupText.Foreground = Brushes.White;
        _popupText.Background = Brushes.Transparent;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }


    // Now Playing Item

    private void ItemBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (!ItemBtn.IsFocused)
            ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(31, 31, 31), .1);
    }

    private void ItemBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!ItemBtn.IsFocused)
            ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(18, 18, 18), .1);
    }

    private void ItemBtn_Click(object sender, RoutedEventArgs e) =>
        ItemBtn.Focus();

    private void ItemBtn_GotFocus(object sender, RoutedEventArgs e) =>
        ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(42, 42, 42), .05);

    private void ItemBtn_LostFocus(object sender, RoutedEventArgs e) =>
        ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(18, 18, 18), .05);

    private void ItemBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e) { }


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.03, .05);
        _popupText.Text = "Play Hope from Lucid Keys";
        HoverPopupHelper.PopupAppear(_mainWindow, PlayBtn, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        ScaleAnimations.ResetScaleAnimation(PlayBtn, .05);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
        e.Handled = true;

        if (_isPlaying)
        {
            PlayBtn.Content = "\uf04b";
            PlayBtn.FontSize = 23;
            _isPlaying = false;
        }
        else
        {
            PlayBtn.Content = "\uf04c";
            PlayBtn.FontSize = 25;
            _isPlaying = true;
        }
    }


    // Artist

    private void ArtistBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (ArtistBtn.Content is TextBlock textBlock)
            textBlock.TextDecorations = TextDecorations.Underline;
        else if (ArtistBtn.Content is string text)
        {
            var newTextBlock = new TextBlock { Text = text, TextDecorations = TextDecorations.Underline };
            ArtistBtn.Content = newTextBlock;
        }
    }

    private void ArtistBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        if (ArtistBtn.Content is TextBlock textBlock)
            textBlock.TextDecorations = null;
    }

    private void ArtistBtn_Click(object sender, RoutedEventArgs e)
    {
        //_mainWindow.MainFrame.Navigate(new ArtistView());
    }


    // More Options

    private void OptionsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        ScaleAnimations.BeginScaleAnimation(OptionsBtn, 1.03, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.White, .1);
        _popupText.Text = "More options for Azahriah";
        HoverPopupHelper.PopupAppear(_mainWindow, OptionsBtn, PlacementMode.Top, _popupText);
    }

    private void OptionsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        ScaleAnimations.ResetScaleAnimation(OptionsBtn, .1);
        ColorAnimations.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.DarkGray, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void OptionsBtn_Click(object sender, RoutedEventArgs e) { }
}
