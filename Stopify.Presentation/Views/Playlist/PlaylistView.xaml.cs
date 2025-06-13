using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Behaviors.Playlist;
using Stopify.Presentation.Utilities.Helpers;
using Stopify.Presentation.ViewModels.Playlist;
using Stopify.Presentation.Views.Main;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.Playlist;

public partial class PlaylistView : UserControl
{
    private double _playlistTitleMaxFontSize = 38;
    private double _playlistTitleMinFontSize = 25;
    private TextBlock _popupText = new();
    private bool _isPlaying = false;
    private bool _isShuffling = false;
    private bool _isSaved = false;
    private bool _isSearching = false;

    public PlaylistView(PlaylistViewModel viewModel)
    {
        InitializeComponent();

        // MVVM code
        DataContext = viewModel;

        Binding playlistTitleFontSizeBinding = new()
        {
            Source = PlaylistTitle,
            Path = new PropertyPath("FontSize")
        };
        BindingOperations.SetBinding(this, PlaylistSizeChangeBehavior.PlaylistTitleFontSizeProperty, playlistTitleFontSizeBinding);

        Binding stickyHeaderBgBackgroundBinding = new()
        {
            Source = StickyHeaderBg,
            Path = new PropertyPath("Background")
        };
        BindingOperations.SetBinding(this, PlaylistSizeChangeBehavior.StickyHeaderBgBackgroundProperty, stickyHeaderBgBackgroundBinding);

        Binding scrollerBgColorBinding = new()
        {
            Source = ScrollerBg,
            Path = new PropertyPath("Color")
        };
        BindingOperations.SetBinding(this, PlaylistSizeChangeBehavior.ScrollerBgColorProperty, scrollerBgColorBinding);

        // Deprecated code
        _popupText.Background = System.Windows.Media.Brushes.Transparent;
        _popupText.Foreground = System.Windows.Media.Brushes.White;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }


    // Playlist

    private void Playlist_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double newFontSize = e.NewSize.Width / 22;

        if (newFontSize <= _playlistTitleMaxFontSize && newFontSize >= _playlistTitleMinFontSize)
            PlaylistTitle.FontSize = newFontSize;
        else if (newFontSize >= _playlistTitleMaxFontSize)
            PlaylistTitle.FontSize = _playlistTitleMaxFontSize;
        else if (newFontSize <= _playlistTitleMinFontSize)
            PlaylistTitle.FontSize = _playlistTitleMinFontSize;
    }

    private void Playlist_Loaded(object sender, RoutedEventArgs e)
    {
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
        //MainHeader.Background = brush;
        StickyHeaderBg.Background = brush;
        ScrollerBg.Color = brush.Color;

        // Set the background of Search Button and Search Bar
        SearchBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 18, 18, 18));
        SearchBar.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 18, 18, 18));
    }


    // Scroll Viewer

    private void Scroller_ScrollChanged(object sender, ScrollChangedEventArgs e) =>
        StickyHeader.Height = Scroller.VerticalOffset > 195 ? double.NaN : 0;


    // Header Author

    private void HeaderAuthor_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;

        if (sender is Button btn)
            ScaleAnimations.BeginScaleAnimation(btn, 1.02, .05);
    }

    private void HeaderAuthor_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;

        if (sender is Button btn)
            ScaleAnimations.ResetScaleAnimation(btn, .05);
    }

    private void HeaderAuthor_Click(object sender, RoutedEventArgs e)
    {
        //_mainWindow.MainFrame.Navigate(new ArtistView());
    }


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.03, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(59, 228, 119), .1);
        _popupText.Text = _isPlaying ? "Pause" : "Play";
        HoverPopupHelper.DisplayPopupTextBlock(PlayBtn, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        MainView mainWindow = (MainView)Application.Current.MainWindow;

        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(PlayBtn, .1);
        ColorAnimations_Deprecated.AnimateBackgroundColor(PlayBorder, PlayBorder.Background, System.Windows.Media.Color.FromRgb(30, 215, 96), .1);
        HoverPopupHelper.HidePopup();
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
        HoverPopupHelper.DisplayPopupTextBlock(ShuffleBtn, PlacementMode.Top, _popupText);
    }

    private void ShuffleBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(ShuffleBtn, .1);
        HoverPopupHelper.HidePopup();
    }

    private void ShuffleBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isShuffling)
        {
            ColorAnimations_Deprecated.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Colors.DarkGray, .1);
            _popupText.Text = "Enable Shuffle for Azahriah";
            _isShuffling = false;
        }
        else
        {
            ColorAnimations_Deprecated.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, System.Windows.Media.Color.FromRgb(30, 215, 96), .1);
            _popupText.Text = "Disable Shuffle for Azahriah";
            _isShuffling = true;
        }
    }


    // More Options

    private void OptionsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(OptionsBtn, 1.03, .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.White, .1);
        _popupText.Text = "More options for Azahriah";
        HoverPopupHelper.DisplayPopupTextBlock(OptionsBtn, PlacementMode.Top, _popupText);
    }

    private void OptionsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(OptionsBtn, .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(OptionsBtn, OptionsBtn.Foreground, Colors.DarkGray, .1);
        HoverPopupHelper.HidePopup();
    }

    private void OptionsBtn_Click(object sender, RoutedEventArgs e) { }


    // Save

    private void SaveBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(SaveBtn, 1.04, .1);
        _popupText.Text = _isSaved ? "Remove from Your Library" : "Save to Your Library";
        HoverPopupHelper.DisplayPopupTextBlock(SaveBtn, PlacementMode.Top, _popupText);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = System.Windows.Media.Brushes.White;
            SaveText.Foreground = System.Windows.Media.Brushes.White;
        }
    }

    private void SaveBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(SaveBtn, .1);
        HoverPopupHelper.HidePopup();

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = System.Windows.Media.Brushes.DarkGray;
            SaveText.Foreground = System.Windows.Media.Brushes.DarkGray;
        }
    }

    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isSaved)
        {
            SaveText.Text = "+";
            SaveBorder.Background = System.Windows.Media.Brushes.Transparent;
            SaveBorder.BorderThickness = new Thickness(2);
            SaveBorder.BorderBrush = System.Windows.Media.Brushes.White;
            SaveText.Foreground = System.Windows.Media.Brushes.White;
            _popupText.Text = "Save to Your Library";
            _isSaved = false;
        }
        else
        {
            SaveText.Text = "\uf00c";
            SaveBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(30, 215, 96));
            SaveBorder.BorderThickness = new Thickness(0);
            SaveBorder.BorderBrush = System.Windows.Media.Brushes.Transparent;
            SaveText.Foreground = System.Windows.Media.Brushes.Black;
            _popupText.Text = "Remove from Your Library";
            _isSaved = true;
        }
    }


    // Download

    private void DownloadBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(DownloadBtn, 1.04, .1);
        DownloadBorder.BorderBrush = System.Windows.Media.Brushes.White;
        DownloadText.Foreground = System.Windows.Media.Brushes.White;
        _popupText.Text = "Download";
        HoverPopupHelper.DisplayPopupTextBlock(DownloadBtn, PlacementMode.Top, _popupText);
    }

    private void DownloadBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(DownloadBtn, .1);
        DownloadBorder.BorderBrush = System.Windows.Media.Brushes.DarkGray;
        DownloadText.Foreground = System.Windows.Media.Brushes.DarkGray;
        HoverPopupHelper.HidePopup();
    }

    private void DownloadBtn_Click(object sender, RoutedEventArgs e) { }


    // Search Button

    private void SearchBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (_isSearching)
            Mouse.OverrideCursor = Cursors.IBeam;
        else
        {
            Mouse.OverrideCursor = Cursors.Hand;
            ColorAnimations_Deprecated.AnimateBackgroundColor(SearchBorder, SearchBorder.Background, System.Windows.Media.Color.FromArgb(50, 18, 18, 18), .1);
            ColorAnimations_Deprecated.AnimateForegroundColor(SearchText, SearchText.Foreground, Colors.LightGray, .2);
        }
    }

    private void SearchBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!_isSearching)
        {
            ColorAnimations_Deprecated.AnimateBackgroundColor(SearchBorder, SearchBorder.Background, System.Windows.Media.Color.FromArgb(0, 18, 18, 18), .1);
            ColorAnimations_Deprecated.AnimateForegroundColor(SearchText, SearchText.Foreground, Colors.DarkGray, .2);
        }
    }

    private void SearchBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isSearching)
            CustomOrderBtn_Click(sender, e);
        else
        {
            SearchBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 18, 18, 18));
            SearchBorder.CornerRadius = new CornerRadius(5, 0, 0, 5);
            SearchBar.Width = Double.NaN;
            CustomOrderFilterText.Width = 0;
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


    // Custom Order Filter Button

    private void CustomOrderBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations_Deprecated.AnimateForegroundColor(CustomOrderFilterIcon, CustomOrderFilterIcon.Foreground, Colors.White, .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(CustomOrderFilterText, CustomOrderFilterText.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(CustomOrderBtn, 1.02, .1);
    }

    private void CustomOrderBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations_Deprecated.AnimateForegroundColor(CustomOrderFilterIcon, CustomOrderFilterIcon.Foreground, Colors.DarkGray, .1);
        ColorAnimations_Deprecated.AnimateForegroundColor(CustomOrderFilterText, CustomOrderFilterText.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(CustomOrderBtn, .1);
    }

    private void CustomOrderBtn_Click(object sender, RoutedEventArgs e)
    {
        SearchBorder.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 18, 18, 18));
        SearchBorder.CornerRadius = new CornerRadius(30);
        SearchBar.Width = 0;
        SearchBox.Text = String.Empty;
        SearchbarText.Text = "Search in Your Library";
        CustomOrderFilterText.Width = Double.NaN;
        _isSearching = false;
    }


    // Refresh

    private void RefreshBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations_Deprecated.AnimateForegroundColor(RefreshBtn, RefreshBtn.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(RefreshBtn, 1.03, .1);
    }

    private void RefreshBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations_Deprecated.AnimateForegroundColor(RefreshBtn, RefreshBtn.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(RefreshBtn, .1);
    }

    private void RefreshBtn_Click(object sender, RoutedEventArgs e) { }
}
