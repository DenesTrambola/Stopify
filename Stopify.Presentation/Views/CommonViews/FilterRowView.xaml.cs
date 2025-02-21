using Stopify.Presentation.Helpers.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.CommonViews;

public partial class FilterRowView : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(CommonRowView), new PropertyMetadata(string.Empty));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    private byte _resultsFilterValue = 0; // 0 - All, 1 - Music, 2 - Podcasts.

    public FilterRowView()
    {
        InitializeComponent();
    }


    // Title

    private void TitleBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void TitleBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void TitleBtn_Click(object sender, RoutedEventArgs e) { }


    // Show All

    private void ShowAllBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(ShowAllBtn, ShowAllBtn.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(ShowAllBtn, 1.01, .1);
    }

    private void ShowAllBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(ShowAllBtn, ShowAllBtn.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(ShowAllBtn, .1);
    }


    // All Filter

    private void AllBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_resultsFilterValue != 0)
            ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Color.FromRgb(51, 51, 51), .1);
    }

    private void AllBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_resultsFilterValue != 0)
            ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }

    private void AllBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(AllText, AllText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Colors.White, .1);

        _resultsFilterValue = 0;

        ColorAnimations.AnimateForegroundColor(MusicText, MusicText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(PodcastsText, PodcastsText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }


    // Music Filter

    private void MusicBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_resultsFilterValue != 1)
            ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Color.FromRgb(51, 51, 51), .1);
    }

    private void MusicBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_resultsFilterValue != 1)
            ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }

    private void MusicBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(MusicText, MusicText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Colors.White, .1);

        _resultsFilterValue = 1;

        ColorAnimations.AnimateForegroundColor(AllText, AllText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(PodcastsText, PodcastsText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }


    // Podcasts Filter

    private void PodcastsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_resultsFilterValue != 2)
            ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, Color.FromRgb(51, 51, 51), .1);
    }

    private void PodcastsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_resultsFilterValue != 2)
            ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }

    private void PodcastsBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(PodcastsText, PodcastsText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, Colors.White, .1);

        _resultsFilterValue = 2;

        ColorAnimations.AnimateForegroundColor(MusicText, MusicText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(AllText, AllText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }
}
