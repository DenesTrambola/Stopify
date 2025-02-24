using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.Sidebar;

public partial class SidebarControl : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private TextBlock _basicPopupText = new();

    private bool _filterPlaylists = false;
    private bool _filterArtists = false;
    private bool _isSearching = false;

    public SidebarControl()
    {
        InitializeComponent();

        _basicPopupText.Foreground = Brushes.White;
        _basicPopupText.FontWeight = FontWeights.SemiBold;
        _basicPopupText.FontSize = 14;
    }


    // Sidebar

    private void Sidebar_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var element = (UserControl)sender;

        if (element.ActualWidth >= 280)
        {
            YourLibraryText.Visibility = Visibility.Visible;
            FilterBtns.Height = double.NaN;
        }
        else
        {
            YourLibraryText.Visibility = Visibility.Hidden;
            FilterBtns.Height = 0;
        }
    }


    // Your Library

    private void YourLibraryBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(YourLibraryBtn, YourLibraryBtn.Foreground, Colors.White, .1);
        ColorAnimations.AnimateForegroundColor(YourLibraryText, YourLibraryText.Foreground, Colors.White, .1);
        _basicPopupText.Text = _mainWindow.SidebarCollapsed == false ? "Collapse Your Library" : "Expand Your Library";
        HoverPopupHelper.PopupAppear(_mainWindow, YourLibraryBtn, PlacementMode.MousePoint, _basicPopupText);
    }

    private void YourLibraryBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(YourLibraryBtn, YourLibraryBtn.Foreground, Color.FromRgb(179, 179, 179), .1);
        ColorAnimations.AnimateForegroundColor(YourLibraryText, YourLibraryText.Foreground, Color.FromRgb(179, 179, 179), .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void YourLibraryBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.SidebarCollapsed == true || _mainWindow.SidebarCollapsed == null)
        {
            SearchGrid.Height = double.NaN;
            _mainWindow.SidebarCollapsed = false;
        }
        else
        {
            SearchGrid.Height = 0;
            _mainWindow.SidebarCollapsed = true;
        }
        _mainWindow.UpdateSidebar();
    }


    // Create

    private void CreateBorder_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(CreateBorder, CreateBorder.Background, Color.FromRgb(31, 31, 31), .1);
        ColorAnimations.AnimateForegroundColor(CreateBtn, CreateBtn.Foreground, Colors.LightGray, .2);
        _basicPopupText.Text = "Create playlist or folder";
        HoverPopupHelper.PopupAppear(_mainWindow, CreateBorder, PlacementMode.Mouse, _basicPopupText);
    }

    private void CreateBorder_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateBackgroundColor(CreateBorder, CreateBorder.Background, Color.FromRgb(18, 18, 18), .1);
        ColorAnimations.AnimateForegroundColor(CreateBtn, CreateBtn.Foreground, Colors.DarkGray, .2);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void CreateBtn_Click(object sender, RoutedEventArgs e) { }


    // Playlists Filter

    private void PlaylistFilterBorder_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (!_filterPlaylists)
            ColorAnimations.AnimateBackgroundColor(PlaylistFilterBorder, PlaylistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
    }

    private void PlaylistFilterBorder_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!_filterPlaylists)
            ColorAnimations.AnimateBackgroundColor(PlaylistFilterBorder, PlaylistFilterBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }

    private void PlaylistFilterBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_filterPlaylists)
        {
            ColorAnimations.AnimateBackgroundColor(PlaylistFilterBorder, PlaylistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
            ColorAnimations.AnimateForegroundColor(PlaylistFilterBtn, PlaylistFilterBtn.Foreground, Colors.White, .1);
            _filterPlaylists = false;
        }
        else
        {
            ColorAnimations.AnimateBackgroundColor(PlaylistFilterBorder, PlaylistFilterBorder.Background, Colors.White, .1);
            ColorAnimations.AnimateForegroundColor(PlaylistFilterBtn, PlaylistFilterBtn.Foreground, Colors.Black, .1);
            _filterPlaylists = true;

            if (_filterArtists)
            {
                ColorAnimations.AnimateBackgroundColor(ArtistFilterBorder, ArtistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
                ColorAnimations.AnimateForegroundColor(ArtistFilterBtn, ArtistFilterBtn.Foreground, Colors.White, .1);
                _filterArtists = false;
            }
        }
    }


    // Artists Filter

    private void ArtistFilterBorder_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        if (!_filterArtists)
            ColorAnimations.AnimateBackgroundColor(ArtistFilterBorder, ArtistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
    }

    private void ArtistFilterBorder_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!_filterArtists)
            ColorAnimations.AnimateBackgroundColor(ArtistFilterBorder, ArtistFilterBorder.Background, Color.FromRgb(42, 42, 42), .1);
    }

    private void ArtistFilterBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_filterArtists)
        {
            ColorAnimations.AnimateBackgroundColor(ArtistFilterBorder, ArtistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
            ColorAnimations.AnimateForegroundColor(ArtistFilterBtn, ArtistFilterBtn.Foreground, Colors.White, .1);
            _filterArtists = false;
        }
        else
        {
            ColorAnimations.AnimateBackgroundColor(ArtistFilterBorder, ArtistFilterBorder.Background, Colors.White, .1);
            ColorAnimations.AnimateForegroundColor(ArtistFilterBtn, ArtistFilterBtn.Foreground, Colors.Black, .1);
            _filterArtists = true;

            if (_filterPlaylists)
            {
                ColorAnimations.AnimateBackgroundColor(PlaylistFilterBorder, PlaylistFilterBorder.Background, Color.FromRgb(51, 51, 51), .1);
                ColorAnimations.AnimateForegroundColor(PlaylistFilterBtn, PlaylistFilterBtn.Foreground, Colors.White, .1);
                _filterPlaylists = false;
            }
        }
    }


    // Search Button

    private void SearchBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (_isSearching)
            Mouse.OverrideCursor = Cursors.IBeam;
        else
        {
            Mouse.OverrideCursor = Cursors.Hand;
            ColorAnimations.AnimateBackgroundColor(SearchBorder, SearchBorder.Background, Color.FromRgb(31, 31, 31), .1);
            ColorAnimations.AnimateForegroundColor(SearchText, SearchText.Foreground, Colors.LightGray, .2);
        }
    }

    private void SearchBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!_isSearching && SearchBorder.Background is SolidColorBrush brush && brush.Color == Color.FromRgb(31, 31, 31))
        {
            ColorAnimations.AnimateBackgroundColor(SearchBorder, SearchBorder.Background, Color.FromRgb(18, 18, 18), .1);
            ColorAnimations.AnimateForegroundColor(SearchText, SearchText.Foreground, Colors.DarkGray, .2);
        }
    }

    private void SearchBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isSearching)
            RecentsBtn_Click(sender, e);
        else
        {
            SearchBorder.Background = new SolidColorBrush(Color.FromRgb(39, 39, 39));
            SearchBorder.CornerRadius = new CornerRadius(5, 0, 0, 5);
            SearchBar.Width = Double.NaN;
            RecentsFilterText.Width = 0;
            SearchBox.Focus();
            _isSearching = true;
        }
    }


    // Search Bar

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchbarText.Text = SearchBox.Text != String.Empty ? SearchbarText.Text = String.Empty : SearchbarText.Text = "Search in Your Library";
        SearchCloseBtn.Width = SearchBox.Text == String.Empty ? 0 : double.NaN;
    }

    private void SearchBox_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.IBeam;

    private void SearchBox_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void SearchCloseBtn_Click(object sender, RoutedEventArgs e)
    {
        SearchBox.Text = String.Empty;
        SearchbarText.Text = "Search in Your Library";
    }


    // Recents Filter

    private void RecentsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(RecentsFilterIcon, RecentsFilterIcon.Foreground, Colors.White, .1);
        ColorAnimations.AnimateForegroundColor(RecentsFilterText, RecentsFilterText.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(RecentsBtn, 1.03, .1);
    }

    private void RecentsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(RecentsFilterIcon, RecentsFilterIcon.Foreground, Colors.DarkGray, .1);
        ColorAnimations.AnimateForegroundColor(RecentsFilterText, RecentsFilterText.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(RecentsBtn, .1);
    }

    private void RecentsBtn_Click(object sender, RoutedEventArgs e)
    {
        SearchBorder.Background = Brushes.Transparent;
        SearchBorder.CornerRadius = new CornerRadius(30);
        SearchBar.Width = 0;
        SearchBox.Text = String.Empty;
        SearchbarText.Text = "Search in the playlist";
        RecentsFilterText.Width = Double.NaN;
        _isSearching = false;
    }
}
