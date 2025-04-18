using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.ViewModels.Queue;
using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.Queue;

public partial class QueueView : UserControl
{
    private MainView _mainWindow = (MainView)Application.Current.MainWindow;
    private byte _queueValue = 0; // 0 - Queue, 1 - Recently Played.

    public QueueView()
    {
        InitializeComponent();

        DataContext = new QueueViewModel();
    }


    // Queue

    private void QueueBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations_Deprecated.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.White, .1);
    }

    private void QueueBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_queueValue != 0)
            ColorAnimations_Deprecated.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.DarkGray, .1);
    }

    private void QueueBtn_Click(object sender, RoutedEventArgs e)
    {
        QueueBtn.BorderBrush = new SolidColorBrush(Color.FromRgb(30, 215, 96));
        RecentBtn.BorderBrush = Brushes.Transparent;
        ColorAnimations_Deprecated.AnimateForegroundColor(RecentBtn, RecentBtn.Foreground, Colors.DarkGray, .1);
        _queueValue = 0;

        NowPlaying.Height = double.NaN;
        NowPlaying.Margin = new Thickness(0, 28, 0, 28);
        NextFrom.Height = double.NaN;
    }


    // Recently Played

    private void RecentBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations_Deprecated.AnimateForegroundColor(RecentBtn, RecentBtn.Foreground, Colors.White, .1);
    }

    private void RecentBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_queueValue != 1)
            ColorAnimations_Deprecated.AnimateForegroundColor(RecentBtn, RecentBtn.Foreground, Colors.DarkGray, .1);
    }

    private void RecentBtn_Click(object sender, RoutedEventArgs e)
    {
        RecentBtn.BorderBrush = new SolidColorBrush(Color.FromRgb(30, 215, 96));
        QueueBtn.BorderBrush = Brushes.Transparent;
        ColorAnimations_Deprecated.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.DarkGray, .1);
        _queueValue = 1;

        NowPlaying.Height = 0;
        NowPlaying.Margin = new Thickness(0, 0, 0, 28);
        NextFrom.Height = 0;
    }


    // Close

    private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations_Deprecated.AnimateForegroundColor(CloseText, CloseText.Foreground, Colors.LightGray, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(CloseBorder, CloseBorder.Background, Color.FromRgb(41, 41, 41), .1);
    }

    private void CloseBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations_Deprecated.AnimateForegroundColor(CloseText, CloseText.Foreground, Colors.DarkGray, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(CloseBorder, CloseBorder.Background, Color.FromRgb(18, 18, 18), .1);
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
        _mainWindow.UpdateQueue();
    }


    // Next From Artist

    private void NextFromArtistBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;

        if (NextFromArtistBtn.Content is TextBlock textBlock)
            textBlock.TextDecorations = TextDecorations.Underline;
        else if (NextFromArtistBtn.Content is string text)
        {
            var newTextBlock = new TextBlock { Text = text, TextDecorations = TextDecorations.Underline };
            NextFromArtistBtn.Content = newTextBlock;
        }
    }

    private void NextFromArtistBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (NextFromArtistBtn.Content is TextBlock textBlock)
            textBlock.TextDecorations = null;
    }

    private void NextFromArtistBtn_Click(object sender, RoutedEventArgs e)
    {
        //_mainWindow.MainFrame.Navigate(new ArtistView());
    }
}
