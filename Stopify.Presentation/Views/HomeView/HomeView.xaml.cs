using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.ViewModels.Home;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.HomeView;

public partial class HomeView : Page
{
    private byte _recentsFilterValue = 0; // 0 - All, 1 - Music, 2 - Podcasts.

    public HomeView(HomeViewModel homeVM)
    {
        InitializeComponent();

        DataContext = homeVM;
    }


    // Home Page

    private void HomePage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (HomePagePanel.ActualWidth >= 1000)
        {
            RecentCol1.Width = new GridLength(1, GridUnitType.Star);
            RecentCol2.Width = new GridLength(1, GridUnitType.Star);
            RecentCol3.Width = new GridLength(1, GridUnitType.Star);
            RecentCol4.Width = new GridLength(1, GridUnitType.Star);

            Grid.SetRow(RecentItem1, 0);
            Grid.SetColumn(RecentItem1, 0);

            Grid.SetRow(RecentItem2, 0);
            Grid.SetColumn(RecentItem2, 1);

            Grid.SetRow(RecentItem3, 0);
            Grid.SetColumn(RecentItem3, 2);

            Grid.SetRow(RecentItem4, 0);
            Grid.SetColumn(RecentItem4, 3);

            Grid.SetRow(RecentItem5, 1);
            Grid.SetColumn(RecentItem5, 0);

            Grid.SetRow(RecentItem6, 1);
            Grid.SetColumn(RecentItem6, 1);

            Grid.SetRow(RecentItem7, 1);
            Grid.SetColumn(RecentItem7, 2);

            Grid.SetRow(RecentItem8, 1);
            Grid.SetColumn(RecentItem8, 3);
        }
        else
        {
            RecentCol1.Width = new GridLength(1, GridUnitType.Star);
            RecentCol2.Width = new GridLength(1, GridUnitType.Star);
            RecentCol3.Width = new GridLength(1, GridUnitType.Auto);
            RecentCol4.Width = new GridLength(1, GridUnitType.Auto);

            Grid.SetRow(RecentItem1, 0);
            Grid.SetColumn(RecentItem1, 0);

            Grid.SetRow(RecentItem2, 0);
            Grid.SetColumn(RecentItem2, 1);

            Grid.SetRow(RecentItem3, 1);
            Grid.SetColumn(RecentItem3, 0);

            Grid.SetRow(RecentItem4, 1);
            Grid.SetColumn(RecentItem4, 1);

            Grid.SetRow(RecentItem5, 2);
            Grid.SetColumn(RecentItem5, 0);

            Grid.SetRow(RecentItem6, 2);
            Grid.SetColumn(RecentItem6, 1);

            Grid.SetRow(RecentItem7, 3);
            Grid.SetColumn(RecentItem7, 0);

            Grid.SetRow(RecentItem8, 3);
            Grid.SetColumn(RecentItem8, 1);
        }
    }

    private void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateBackgroundColor(MusicBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

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
            ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void AllBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 0)
        {
            ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void AllBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, Colors.White, .1);

        ColorAnimations.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, Colors.White, .1);

        _recentsFilterValue = 0;

        ColorAnimations.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }


    // Music Filter

    private void MusicBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_recentsFilterValue != 1)
        {
            ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void MusicBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 1)
        {
            ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void MusicBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, Colors.White, .1);

        ColorAnimations.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, Colors.White, .1);

        _recentsFilterValue = 1;

        ColorAnimations.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }


    // Podcasts Filter

    private void PodcastsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (_recentsFilterValue != 2)
        {
            ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(150, 42, 42, 42), .1);
        }
    }

    private void PodcastsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (_recentsFilterValue != 2)
        {
            ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
            ColorAnimations.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        }
    }

    private void PodcastsBtn_Click(object sender, RoutedEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(PodcastsBtn, PodcastsBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(PodcastsBorder, PodcastsBtn.Background, Colors.White, .1);

        ColorAnimations.AnimateForegroundColor(HeaderPodcastsBtn, HeaderPodcastsBtn.Foreground, Colors.Black, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderPodcastsBorder, HeaderPodcastsBtn.Background, Colors.White, .1);

        _recentsFilterValue = 2;

        ColorAnimations.AnimateForegroundColor(AllBtn, AllBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(AllBorder, AllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(MusicBtn, MusicBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(MusicBorder, MusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);

        ColorAnimations.AnimateForegroundColor(HeaderAllBtn, HeaderAllBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderAllBorder, HeaderAllBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
        ColorAnimations.AnimateForegroundColor(HeaderMusicBtn, HeaderMusicBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateBackgroundColor(HeaderMusicBorder, HeaderMusicBorder.Background, System.Windows.Media.Color.FromArgb(80, 42, 42, 42), .1);
    }
}
