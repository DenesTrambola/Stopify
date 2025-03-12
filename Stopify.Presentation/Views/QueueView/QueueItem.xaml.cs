using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.QueueView;

public partial class QueueItem : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private TextBlock _popupText = new();
    private bool _isPlaying = false;

    public QueueItem()
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

    private void AuthorBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;

        if (sender is Button btn)
        {
            if (btn.Content is string text)
            {
                btn.Content = new TextBlock()
                {
                    Text = text,
                    TextDecorations = TextDecorations.Underline,
                    Foreground = Brushes.White,
                };
            }
            else if (btn.Content is TextBlock existingTextBlock)
            {
                existingTextBlock.TextDecorations = TextDecorations.Underline;
                existingTextBlock.Foreground = Brushes.White;
            }
        }
    }

    private void AuthorBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;

        if (sender is Button btn)
        {
            if (btn.Content is string text)
            {
                btn.Content = new TextBlock()
                {
                    Text = text,
                    TextDecorations = null,
                    Foreground = Brushes.DarkGray,
                };
            }
            else if (btn.Content is TextBlock existingTextBlock)
            {
                existingTextBlock.TextDecorations = null;
                existingTextBlock.Foreground = Brushes.DarkGray;
            }
        }
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
