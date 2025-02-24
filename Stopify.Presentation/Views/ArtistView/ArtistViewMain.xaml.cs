using Stopify.Presentation.Helpers.Animations;
using Stopify.Presentation.Helpers.Utilities;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.ArtistView;

public partial class ArtistViewMain : Page
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private double _artistNameMaxFontSize = 95;
    private TextBlock _popupText = new();
    private bool _isPlaying = false;
    private bool _isShuffling = false;
    private bool _isFollowing = false;
    private byte _discographyFilterValue = 0; // 0 - Popular, 1 - Albums, 2 - Singles.

    public ArtistViewMain()
    {
        InitializeComponent();

        _popupText.Foreground = System.Windows.Media.Brushes.White;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.Background = System.Windows.Media.Brushes.Transparent;
        _popupText.FontSize = 14;
    }


    // Artist View

    private void ArtistView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double newFontSize = e.NewSize.Width / 11;
        ArtistName.FontSize = newFontSize >= _artistNameMaxFontSize ? _artistNameMaxFontSize : newFontSize;
        ArtistName.MaxHeight = newFontSize * 1.2;
    }

    private void ArtistView_Loaded(object sender, RoutedEventArgs e)
    {
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string imagePath = Path.Combine(projectDirectory, "Resources", "song.jpg");

        using Bitmap bitmap = new Bitmap(imagePath);

        // Variables to store sum of RGB components
        long rSum = 0;
        long gSum = 0;
        long bSum = 0;
        int totalPixels = 0;

        // Loop through each pixel in the image
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                // Get pixel color
                System.Drawing.Color pixelColor = bitmap.GetPixel(x, y);

                // Sum up RGB components
                rSum += pixelColor.R;
                gSum += pixelColor.G;
                bSum += pixelColor.B;
                totalPixels++;
            }
        }

        // Calculate average RGB values
        byte avgR = (byte)(rSum / totalPixels);
        byte avgG = (byte)(gSum / totalPixels);
        byte avgB = (byte)(bSum / totalPixels);

        // Convert the average color to WPF Color and Brush
        System.Windows.Media.Color averageColor = System.Windows.Media.Color.FromRgb(avgR, avgG, avgB);
        SolidColorBrush brush = new SolidColorBrush(averageColor);

        // Set the background of both Main and Sticky Headers
        MainHeader.Background = brush;
        StickyHeaderBg.Background = brush;
        ScrollerBg.Color = brush.Color;
    }


    // Scroll Viewer

    private void Scroller_ScrollChanged(object sender, ScrollChangedEventArgs e) =>
        StickyHeader.Height = Scroller.VerticalOffset > 195 ? double.NaN : 0;


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.03, .1);
        ColorAnimations.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(59, 228, 119), .1);
        _popupText.Text = _isPlaying ? "Pause" : "Play";
        PopupHelper.PopupAppear(_mainWindow, PlayBtn, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(PlayBtn, .1);
        ColorAnimations.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(30, 215, 96), .1);
        PopupHelper.PopupDisappear(_mainWindow);
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
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


    // Shuffle

    private void ShuffleBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(ShuffleBtn, 1.03, .1);
        _popupText.Text = _isShuffling ? "Disable Shuffle for Azahriah" : "Enable Shuffle for Azahriah";
        PopupHelper.PopupAppear(_mainWindow, ShuffleBtn, PlacementMode.Top, _popupText);
    }

    private void ShuffleBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(ShuffleBtn, .1);
        PopupHelper.PopupDisappear(_mainWindow);
    }

    private void ShuffleBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isShuffling)
        {
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Colors.DarkGray, .1);
            _popupText.Text = "Enable Shuffle for Azahriah";
            _isShuffling = false;
        }
        else
        {
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, System.Windows.Media.Color.FromRgb(30, 215, 96), .1);
            _popupText.Text = "Disable Shuffle for Azahriah";
            _isShuffling = true;
        }
    }


    // Follow

    private void FollowBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(FollowBtn, 1.03, .1);
        FollowBorder.BorderBrush = System.Windows.Media.Brushes.LightGray;
    }

    private void FollowBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(FollowBtn, .1);
        FollowBorder.BorderBrush = System.Windows.Media.Brushes.DarkGray;
    }

    private void FollowBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isFollowing)
        {
            FollowText.Text = "Follow";
            _isFollowing = false;
        }
        else
        {
            FollowText.Text = "Following";
            _isFollowing = true;
        }
    }


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


    // SeeMore / ShowAll

    private void SeeMoreBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not Control button)
            return;

        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(button, button.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(button, 1.01, .1);
    }

    private void SeeMoreBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not Control button)
            return;

        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(button, button.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(button, .1);
    }

    private void SeeMoreBtn_Click(object sender, RoutedEventArgs e) { }


    // Discography

    private void CategoryTitle_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void CategoryTitle_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void DiscographyBtn_Click(object sender, RoutedEventArgs e) { }


    // About

    private void AboutBtn_MouseEnter(object sender, MouseEventArgs e) =>
        ScaleAnimations.BeginScaleAnimation(AboutBtn, 1.03, .3);

    private void AboutBtn_MouseLeave(object sender, MouseEventArgs e) =>
        ScaleAnimations.ResetScaleAnimation(AboutBtn, .3);

    private void AboutBtn_Click(object sender, RoutedEventArgs e) { }


    // Filter by Popular Releases

    private void PopularBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_discographyFilterValue != 0)
            ColorAnimations.AnimateBackgroundColor(PopularBorder, PopularBorder.Background, System.Windows.Media.Color.FromRgb(51, 51, 51), .1);
    }

    private void PopularBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_discographyFilterValue != 0)
            ColorAnimations.AnimateBackgroundColor(PopularBorder, PopularBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }

    private void PopularBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(PopularText, PopularText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(PopularBorder, PopularBorder.Background, Colors.White, .1);

        _discographyFilterValue = 0;

        ColorAnimations.AnimateForegroundColor(AlbumsText, AlbumsText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AlbumsBorder, AlbumsBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(SinglesText, SinglesText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(SinglesBorder, SinglesBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }


    // Filter by Albums

    private void AlbumsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_discographyFilterValue != 1)
            ColorAnimations.AnimateBackgroundColor(AlbumsBorder, AlbumsBorder.Background, System.Windows.Media.Color.FromRgb(51, 51, 51), .1);
    }

    private void AlbumsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_discographyFilterValue != 1)
            ColorAnimations.AnimateBackgroundColor(AlbumsBorder, AlbumsBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }

    private void AlbumsBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(AlbumsText, AlbumsText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(AlbumsBorder, AlbumsBorder.Background, Colors.White, .1);

        _discographyFilterValue = 1;

        ColorAnimations.AnimateForegroundColor(PopularText, PopularText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PopularBorder, PopularBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(SinglesText, SinglesText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(SinglesBorder, SinglesBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }


    // Filter by Singles And EPs

    private void SinglesBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_discographyFilterValue != 2)
            ColorAnimations.AnimateBackgroundColor(SinglesBorder, SinglesBorder.Background, System.Windows.Media.Color.FromRgb(51, 51, 51), .1);
    }

    private void SinglesBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_discographyFilterValue != 2)
            ColorAnimations.AnimateBackgroundColor(SinglesBorder, SinglesBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }

    private void SinglesBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(SinglesText, SinglesText.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(SinglesBorder, SinglesBorder.Background, Colors.White, .1);

        _discographyFilterValue = 2;

        ColorAnimations.AnimateForegroundColor(AlbumsText, AlbumsText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AlbumsBorder, AlbumsBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(PopularText, PopularText.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PopularBorder, PopularBorder.Background, System.Windows.Media.Color.FromRgb(42, 42, 42), .1);
    }
}
