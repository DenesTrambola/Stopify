using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Views;
using Stopify.Presentation.Views.HomeView;
using Stopify.Presentation.Views.PlaylistView;
using Stopify.Presentation.Views.SearchView;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Stopify.Presentation;

public partial class MainWindow : Window
{
    private PlayerControl _player = new();
    public bool? SidebarCollapsed { get; set; } = null;
    public bool? NowPlayingCollapsed { get; set; } = null;
    public bool QueueCollapsed { get; set; } = true;
    public bool FullScreen { get; set; } = false;

    public MainWindow(HomeView homeView, SearchView searchView, ArtistView artistView, PlaylistView playlistView)
    {
        InitializeComponent();

        MainLayout.Content = playlistView;
    }

    public void Window_Loaded(object sender, RoutedEventArgs e)
    {
        //MainFrame.Navigate(new HomeView());
    }

    public void UpdateSidebar()
    {
        if (SidebarCollapsed == true)
        {
            SideBar.Width = 81;
            NowPlayingCollapsed = null;
        }
        else
        {
            SideBar.Width = 280;
            NowPlayingCollapsed = true;
        }

        UpdateNowPlaying();
    }

    public void UpdateNowPlaying()
    {
        if (NowPlayingCollapsed == true)
        {
            ColorAnimations.AnimateForegroundColor(_player.NowPlayingOption, _player.NowPlayingOption.Foreground, Color.FromRgb(30, 215, 96), .1);
            NowPlaying.Width = 0;
            if (QueueCollapsed)
                SongQueue.Width = 0;
        }
        else
        {
            ColorAnimations.AnimateForegroundColor(_player.NowPlayingOption, _player.NowPlayingOption.Foreground, Colors.DarkGray, .1);
            if (ActualWidth >= 1250)
                NowPlaying.Width = 350;
            else if (ActualWidth >= 1100)
                NowPlaying.Width = 281;
            else
                NowPlaying.Width = 281;
        }
    }

    public void UpdateQueue()
    {
        if (QueueCollapsed)
        {
            if (NowPlayingCollapsed == true && SongQueue.ActualWidth != 0)
                SongQueue.Width = 0;

            var heightIncreaseAnimation = new DoubleAnimation
            {
                From = NowPlaying.ActualHeight + 7,
                To = 0,
                Duration = TimeSpan.FromSeconds(.3),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop,
            };

            SongQueue.BeginAnimation(HeightProperty, heightIncreaseAnimation);
            SongQueue.Width = 0;
        }
        else
        {
            var heightIncreaseAnimation = new DoubleAnimation
            {
                From = 0,
                To = NowPlaying.ActualHeight + 7,
                Duration = TimeSpan.FromSeconds(.3),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop,
            };

            SongQueue.BeginAnimation(HeightProperty, heightIncreaseAnimation);
            SongQueue.Height = NowPlaying.ActualHeight + 7;

            if (ActualWidth >= 1250)
                SongQueue.Width = 350;
            else if (ActualWidth >= 1100)
                SongQueue.Width = 281;
            else
                SongQueue.Width = 281;

            PlayerControl player = new();
            player.NowPlayingBtn.Content = "\uf106";
        }
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if ((NowPlayingCollapsed == false || NowPlayingCollapsed == null) && !QueueCollapsed)
            SongQueue.Height = NowPlaying.ActualHeight + 7;

        if (ActualWidth >= 1250)
        {
            if (NowPlayingCollapsed == false || NowPlayingCollapsed == null)
                NowPlaying.Width = 350;
            if (!QueueCollapsed)
                SongQueue.Width = 350;
        }
        else if (ActualWidth >= 1100)
        {
            if (SidebarCollapsed == null)
                SideBar.Width = 280;
            if (NowPlayingCollapsed == false || NowPlayingCollapsed == null)
                NowPlaying.Width = 281;
            if (!QueueCollapsed)
                SongQueue.Width = 281;
        }
        else
        {
            if (SidebarCollapsed == null)
                SideBar.Width = 81;
            if (NowPlayingCollapsed == null)
                NowPlaying.Width = 281;
            if (!QueueCollapsed)
                SongQueue.Width = 281;
        }
    }
}
