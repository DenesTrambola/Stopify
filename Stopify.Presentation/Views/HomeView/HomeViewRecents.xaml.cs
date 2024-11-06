using Stopify.Presentation.Helpers.Animations;
using Stopify.Presentation.Views.PlaylistView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.HomeView;

public partial class HomeViewRecents : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(HomeViewRecents), new PropertyMetadata(string.Empty));

    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private bool _isPlaying = false;

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public HomeViewRecents()
    {
        InitializeComponent();
    }


    // Recent Playlist And Artists

    private void RecentBtn_Loaded(object sender, RoutedEventArgs e) =>
        RecentBorder.Background = new SolidColorBrush(Color.FromArgb(100, 73, 78, 78));

    private void RecentBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(RecentBorder, RecentBorder.Background, Color.FromArgb(100, 73, 78, 78), .05);
        PlayBtn.Visibility = Visibility.Visible;
    }

    private void RecentBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateBackgroundColor(RecentBorder, RecentBorder.Background, Color.FromArgb(200, 73, 78, 78), .05);
        PlayBtn.Visibility = Visibility.Hidden;
    }

    private void RecentBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new PlaylistViewMain());


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e) =>
        ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.03, .05);

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e) =>
        ScaleAnimations.ResetScaleAnimation(PlayBtn, .05);

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
        e.Handled = true; // Prevent clicking the RecentBtn

        if (_isPlaying)
        {
            PlayIcon.Text = "\uf04b";
            PlayIcon.FontSize = 16;
            PlayingIcon.Visibility = Visibility.Hidden;
            _isPlaying = false;
        }
        else
        {
            PlayIcon.Text = "\uf04c";
            PlayIcon.FontSize = 19;
            PlayingIcon.Visibility = Visibility.Visible;
            _isPlaying = true;
        }
    }
}
