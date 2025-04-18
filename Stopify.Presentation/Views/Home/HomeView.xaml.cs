using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.ViewModels.Home;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.Home;

public partial class HomeView : UserControl
{
    private byte _recentsFilterValue = 0; // 0 - All, 1 - Music, 2 - Podcasts.

    private readonly HomeViewModel _viewModel;

    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }


    // Home Page

    private void HomePage_SizeChanged(object sender, SizeChangedEventArgs e) =>
        _viewModel.UpdateColumnCount(e.NewSize.Width);

    private void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string imagePath = Path.Combine(projectDirectory, "Assets", "Images", "song.jpg");
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
        MainHeaderBg.Color = brush.Color;
        StickyHeaderBg.Color = brush.Color;
        ScrollerBg.Color = brush.Color;
    }


    // Scroll View

    private void Scroller_ScrollChanged(object sender, ScrollChangedEventArgs e) =>
        StickyHeader.Height = Scroller.VerticalOffset > 300 ? double.NaN : 0;


    // All Filter

    private void AllBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_recentsFilterValue != 0)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void AllBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 0)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void AllBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations_Deprecated.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(AllBorder, AllBorder.Background, Colors.White, .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, Colors.White, .1);

        _recentsFilterValue = 0;

        ColorAnimations_Deprecated.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }


    // Music Filter

    private void MusicBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_recentsFilterValue != 1)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void MusicBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 1)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void MusicBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations_Deprecated.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Colors.White, .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, Colors.White, .1);

        _recentsFilterValue = 1;

        ColorAnimations_Deprecated.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }


    // Podcasts Filter

    private void PodcastsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_recentsFilterValue != 2)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void PodcastsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 2)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void PodcastsBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations_Deprecated.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PodcastsBorder, PodcastsBtn.Background, Colors.White, .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.Black, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBtn.Background, Colors.White, .1);

        _recentsFilterValue = 2;

        ColorAnimations_Deprecated.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }
}
