using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using Stopify.Presentation.ViewModels.Playlist;
using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.Playlist;

public partial class PlaylistHeader : UserControl
{
    private TextBlock _popupText = new();
    private byte _titleSortValue = 0; // 0 - Off, 1 - TitleDesc, 2 - TitleAsc, 3 - ArtistDesc, 4 - ArtistAsc.
    private byte _albumSortValue = 0; // 0 - Off, 1 - Descending, 2 - Ascending.
    private byte _dateSortValue = 0; // 0 - Off, 1 - Descending, 2 - Ascending.
    private byte _durationSortValue = 0; // 0 - Off, 1 - Descending, 2 - Ascending.

    public PlaylistHeader()
    {
        InitializeComponent();

        DataContext = new PlaylistHeaderViewModel();

        _popupText.FontSize = 14;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.Background = Brushes.Transparent;
        _popupText.Foreground = Brushes.White;
        _popupText.Text = "Duration";
    }


    // Playlist Header

    private void PlaylistHeader_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var element = (UserControl)sender;

        if (element.ActualWidth >= 740)
        {
            DateBtn.Width = double.NaN;
            DateColumn.Width = new GridLength(1, GridUnitType.Star);
        }
        else if (element.ActualWidth >= 510)
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


    // Title/Artist

    private void TitleBtn_MouseEnter(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(TitleText, TitleText.Foreground, Colors.White, .05);

    private void TitleBtn_MouseLeave(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(TitleText, TitleText.Foreground, Colors.DarkGray, .05);

    private void TitleBtn_Click(object sender, RoutedEventArgs e)
    {
        AlbumSort.Visibility = Visibility.Hidden;
        _albumSortValue = 0;
        DateSort.Visibility = Visibility.Hidden;
        _dateSortValue = 0;
        DurationSort.Visibility = Visibility.Hidden;
        _durationSortValue = 0;

        switch (_titleSortValue)
        {
            case 0:
                _titleSortValue = 1;
                TitleSort.Text = "\uf0d8";
                TitleSort.Visibility = Visibility.Visible;
                break;
            case 1:
                _titleSortValue = 2;
                TitleSort.Text = "\uf0d7";
                break;
            case 2:
                _titleSortValue = 3;
                TitleSort.Text = "\uf0d8";
                TitleText.Text = "Artist";
                break;
            case 3:
                _titleSortValue = 4;
                TitleSort.Text = "\uf0d7";
                break;
            case 4:
                _titleSortValue = 0;
                TitleText.Text = "Title";
                TitleSort.Visibility = Visibility.Hidden;
                break;
            default:
                TitleText.Text = "ERROR!";
                TitleText.Foreground = Brushes.Red;
                TitleText.FontWeight = FontWeights.Bold;
                break;
        }
    }


    // Album

    private void AlbumBtn_MouseEnter(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(AlbumText, TitleText.Foreground, Colors.White, .05);

    private void AlbumBtn_MouseLeave(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(AlbumText, TitleText.Foreground, Colors.DarkGray, .05);

    private void AlbumBtn_Click(object sender, RoutedEventArgs e)
    {
        TitleSort.Visibility = Visibility.Hidden;
        _titleSortValue = 0;
        DateSort.Visibility = Visibility.Hidden;
        _dateSortValue = 0;
        DurationSort.Visibility = Visibility.Hidden;
        _durationSortValue = 0;

        switch (_albumSortValue)
        {
            case 0:
                _albumSortValue = 1;
                AlbumSort.Text = "\uf0d8";
                AlbumSort.Visibility = Visibility.Visible;
                break;
            case 1:
                _albumSortValue = 2;
                AlbumSort.Text = "\uf0d7";
                break;
            case 2:
                _albumSortValue = 0;
                AlbumSort.Visibility = Visibility.Hidden;
                break;
            default:
                AlbumText.Text = "ERROR!";
                AlbumText.Foreground = Brushes.Red;
                AlbumText.FontWeight = FontWeights.Bold;
                break;
        }
    }


    // Date

    private void DateBtn_MouseEnter(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(DateText, TitleText.Foreground, Colors.White, .05);

    private void DateBtn_MouseLeave(object sender, MouseEventArgs e) =>
        ColorAnimations.AnimateForegroundColor(DateText, TitleText.Foreground, Colors.DarkGray, .05);

    private void DateBtn_Click(object sender, RoutedEventArgs e)
    {
        TitleSort.Visibility = Visibility.Hidden;
        _titleSortValue = 0;
        AlbumSort.Visibility = Visibility.Hidden;
        _albumSortValue = 0;
        DurationSort.Visibility = Visibility.Hidden;
        _durationSortValue = 0;

        switch (_dateSortValue)
        {
            case 0:
                _dateSortValue = 1;
                DateSort.Text = "\uf0d8";
                DateSort.Visibility = Visibility.Visible;
                break;
            case 1:
                _dateSortValue = 2;
                DateSort.Text = "\uf0d7";
                break;
            case 2:
                _dateSortValue = 0;
                DateSort.Visibility = Visibility.Hidden;
                break;
            default:
                DateText.Text = "ERROR!";
                DateText.Foreground = Brushes.Red;
                DateText.FontWeight = FontWeights.Bold;
                break;
        }
    }


    // Duration

    private void DurationBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        ColorAnimations.AnimateForegroundColor(DurationText, TitleText.Foreground, Colors.White, .05);
        HoverPopupHelper.DisplayPopup_Deprecated(DurationBtn, PlacementMode.Top, _popupText);
    }

    private void DurationBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        MainView mainWindow = (MainView)Application.Current.MainWindow;

        ColorAnimations.AnimateForegroundColor(DurationText, TitleText.Foreground, Colors.DarkGray, .05);
        HoverPopupHelper.HidePopup();
    }

    private void DurationBtn_Click(object sender, RoutedEventArgs e)
    {
        TitleSort.Visibility = Visibility.Hidden;
        _titleSortValue = 0;
        AlbumSort.Visibility = Visibility.Hidden;
        _albumSortValue = 0;
        DateSort.Visibility = Visibility.Hidden;
        _dateSortValue = 0;

        switch (_durationSortValue)
        {
            case 0:
                _durationSortValue = 1;
                DurationSort.Text = "\uf0d8";
                DurationSort.Visibility = Visibility.Visible;
                break;
            case 1:
                _durationSortValue = 2;
                DurationSort.Text = "\uf0d7";
                break;
            case 2:
                _durationSortValue = 0;
                DurationSort.Visibility = Visibility.Hidden;
                break;
            default:
                DurationText.Text = "ERROR!";
                DurationText.Foreground = Brushes.Red;
                DurationText.FontWeight = FontWeights.Bold;
                break;
        }
    }
}
