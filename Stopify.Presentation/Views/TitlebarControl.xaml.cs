using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views;

public partial class TitlebarControl : UserControl
{
    MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    TextBlock _popupText = new();
    private double _originalWidth;
    private double _originalHeight;
    private double _originalLeft;
    private double _originalTop;
    private bool _isWindowedFullScreen = false;

    public TitlebarControl()
    {
        InitializeComponent();

        _popupText.Foreground = Brushes.White;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }


    // TitleBar

    private void TitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (ActualWidth <= 850 && SearchBar.Width != 0)
        {
            SearchbarTxt.Width = 0;
            SearchbarBox.Width = 0;
            SearchbarLine.Width = 0;
            SearchbarBrowse.Width = 0;
            SearchBar.Width = 0;
            SearchBtnBorder.CornerRadius = new CornerRadius(30);
            SearchbarBox.Text = String.Empty;

            WhatsNewBtn.Width = 0;
            FriendActivityBtn.Width = 0;
        }
        else if (ActualWidth > 850 && SearchBar.Width == 0)
        {
            SearchbarTxt.Width = double.NaN;
            SearchbarBox.Width = double.NaN;
            SearchbarLine.Width = double.NaN;
            SearchbarBrowse.Width = double.NaN;
            SearchBar.Width = Double.NaN;
            SearchBtnBorder.CornerRadius = new CornerRadius(23, 0, 0, 23);
        }
        else if (WhatsNewBtn.Width == 0 && FriendActivityBtn.Width == 0)
        {
            WhatsNewBtn.Width = double.NaN;
            FriendActivityBtn.Width = double.NaN;
        }
    }

    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
                window.DragMove();
        }
    }


    // Options

    private void OptionsBtn_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn)
            return;

        ContextMenu dropdownMenu = new ContextMenu();

        // Add menu items
        MenuItem item1 = new MenuItem() { Header = "File" };
        MenuItem item2 = new MenuItem() { Header = "Edit" };
        MenuItem item3 = new MenuItem() { Header = "View" };
        MenuItem item4 = new MenuItem() { Header = "Playback" };
        MenuItem item5 = new MenuItem() { Header = "Help" };

        // Optionally attach click events to the menu items
        item1.Click += (s, args) => MessageBox.Show("File clicked");
        item2.Click += (s, args) => MessageBox.Show("Edit clicked");
        item3.Click += (s, args) => MessageBox.Show("View clicked");
        item4.Click += (s, args) => MessageBox.Show("Playback clicked");
        item5.Click += (s, args) => MessageBox.Show("Help clicked");

        // Add items to the ContextMenu
        dropdownMenu.Items.Add(item1);
        dropdownMenu.Items.Add(item2);
        dropdownMenu.Items.Add(item3);
        dropdownMenu.Items.Add(item4);
        dropdownMenu.Items.Add(item5);

        // Set the ContextMenu to the button and open it
        btn.ContextMenu = dropdownMenu;
        btn.ContextMenu.PlacementTarget = btn;
        btn.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        btn.ContextMenu.IsOpen = true;
    }

    private void OptionsBtn_GotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
            ColorAnimations.AnimateForegroundColor(btn, btn.Foreground, Colors.DarkGray, .02);
    }

    private void OptionsBtn_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
            ColorAnimations.AnimateForegroundColor(btn, btn.Foreground, Colors.White, .02);
    }


    // Back, Forward

    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.MainFrame.CanGoBack)
            _mainWindow.MainFrame.GoBack();
        UpdatePageBtns();
    }

    private void ForwardBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.MainFrame.CanGoForward)
            _mainWindow.MainFrame.GoForward();
        UpdatePageBtns();
    }

    private void UpdatePageBtns()
    {
        if (_mainWindow.MainFrame.CanGoBack)
            ColorAnimations.AnimateForegroundColor(BackBtnLabel, BackBtnLabel.Foreground, Colors.DarkGray, .02);
        else
            ColorAnimations.AnimateForegroundColor(BackBtnLabel, BackBtnLabel.Foreground, Color.FromRgb(50, 50, 50), .02);

        if (_mainWindow.MainFrame.CanGoForward)
            ColorAnimations.AnimateForegroundColor(ForwardBtnLabel, ForwardBtnLabel.Foreground, Colors.DarkGray, .02);
        else
            ColorAnimations.AnimateForegroundColor(ForwardBtnLabel, ForwardBtnLabel.Foreground, Color.FromRgb(50, 50, 50), .02);
    }

    private void BackBtn_MouseMove(object sender, MouseEventArgs e)
    {
        BackBtn_MouseEnter(sender, e);
        if (!_mainWindow.MainFrame.CanGoBack)
            Mouse.OverrideCursor = Cursors.No;
        else
            Mouse.OverrideCursor = Cursors.Arrow;
    }

    private void ForwardBtn_MouseMove(object sender, MouseEventArgs e)
    {
        ForwardBtn_MouseEnter(sender, e);
        if (!_mainWindow.MainFrame.CanGoForward)
            Mouse.OverrideCursor = Cursors.No;
        else
            Mouse.OverrideCursor = Cursors.Arrow;
    }

    private void BackBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (_mainWindow.MainFrame.CanGoBack)
            ColorAnimations.AnimateForegroundColor(BackBtnLabel, BackBtnLabel.Foreground, Colors.White, .02);

        TextBlock textBlock = new();
        textBlock.Text = "Go back";
        textBlock.Foreground = Brushes.White;
        textBlock.FontWeight = FontWeights.SemiBold;
        textBlock.FontSize = 14;
        HoverPopupHelper.PopupAppear(_mainWindow, BackBtn, PlacementMode.Bottom, textBlock);
    }

    private void ForwardBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (_mainWindow.MainFrame.CanGoForward)
            ColorAnimations.AnimateForegroundColor(ForwardBtnLabel, ForwardBtnLabel.Foreground, Colors.White, .02);

        TextBlock textBlock = new();
        textBlock.Text = "Go forward";
        textBlock.Foreground = Brushes.White;
        textBlock.FontWeight = FontWeights.SemiBold;
        textBlock.FontSize = 14;
        HoverPopupHelper.PopupAppear(_mainWindow, ForwardBtn, PlacementMode.Bottom, textBlock);
    }

    private void BackBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        CursorReset();
        UpdatePageBtns();
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void ForwardBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        CursorReset();
        UpdatePageBtns();
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void CursorReset() =>
        Mouse.OverrideCursor = Cursors.Arrow;


    // Home

    private void HomeBtn_Click(object sender, RoutedEventArgs e)
    {
        _mainWindow.MainFrame.Navigate(new HomeView.HomeView());
        ColorAnimations.AnimateForegroundColor(HomeBtn, HomeBtn.Foreground, Colors.White, .2);
        UpdatePageBtns();
    }

    private void HomeBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Home";
        HoverPopupHelper.PopupAppear(_mainWindow, HomeBtn, PlacementMode.Bottom, _popupText);
        ScaleAnimations.BeginScaleAnimation(HomeBtn, 1.05, .2);
        Mouse.OverrideCursor = Cursors.Hand;
    }

    private void HomeBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        ScaleAnimations.ResetScaleAnimation(HomeBtn, .2);
        Mouse.OverrideCursor = Cursors.Arrow;
    }


    // Search

    private void SearchBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Search";
        HoverPopupHelper.PopupAppear(_mainWindow, SearchBtn, PlacementMode.Bottom, _popupText);
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(SearchBtnBorder, SearchBtnBorder.Background, Color.FromRgb(54, 54, 53), .1);
        ColorAnimations.AnimateBackgroundColor(SearchBar, SearchBar.Background, Color.FromRgb(54, 54, 53), .1);
        ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.White, .2);

        if (SearchBar.ActualWidth == 0)
            ScaleAnimations.BeginScaleAnimation(SearchBtn, 1.03, .2);
    }

    private void SearchBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(SearchBtn, .2);
        ColorAnimations.AnimateBackgroundColor(SearchBtnBorder, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
        ColorAnimations.AnimateBackgroundColor(SearchBar, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
        ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.DarkGray, .2);
    }

    private void SearchBtn_Click(object sender, RoutedEventArgs e)
    {
        UpdatePageBtns();

        if (SearchBar.Width == 0)
        {
            _mainWindow.MainFrame.Navigate(new SearchView.SearchView());

            SearchbarTxt.Width = double.NaN;
            SearchbarBox.Width = double.NaN;
            SearchbarLine.Width = double.NaN;
            SearchbarBrowse.Width = double.NaN;
            SearchBar.Width = Double.NaN;
            SearchBtnBorder.CornerRadius = new CornerRadius(23, 0, 0, 23);

            if (ActualWidth < 850)
            {
                WhatsNewBtn.Width = 0;
                FriendActivityBtn.Width = 0;
            }
            else
            {
                WhatsNewBtn.Width = double.NaN;
                FriendActivityBtn.Width = double.NaN;
            }

            ScaleAnimations.ResetScaleAnimation(SearchBtn, .2);
        }
        else
        {
            SearchbarTxt.Width = 0;
            SearchbarBox.Width = 0;
            SearchbarLine.Width = 0;
            SearchbarBrowse.Width = 0;
            SearchBar.Width = 0;
            SearchBtnBorder.CornerRadius = new CornerRadius(30);
            SearchbarBox.Text = String.Empty;

            WhatsNewBtn.Width = Double.NaN;
            FriendActivityBtn.Width = Double.NaN;

            ScaleAnimations.BeginScaleAnimation(SearchBtn, 1.03, .2);
        }
    }

    private void SearchbarBox_GotFocus(object sender, RoutedEventArgs e)
    {
        SearchBar.BorderThickness = new Thickness(0, 2, 2, 2);
        SearchBtnBorder.BorderThickness = new Thickness(2, 2, 0, 2);
        SearchBtnBorder.Padding = new Thickness(0, 0, 2, 0);
        ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.White, .1);
    }

    private void SearchbarBox_LostFocus(object sender, RoutedEventArgs e)
    {
        SearchBar.BorderThickness = new Thickness(0);
        SearchBtnBorder.BorderThickness = new Thickness(0);
        SearchBtnBorder.Padding = new Thickness(0);
        ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.DarkGray, .1);
        ColorAnimations.AnimateBackgroundColor(SearchBtnBorder, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
        ColorAnimations.AnimateBackgroundColor(SearchBar, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
    }

    private void SearchbarBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchbarBox.Text == String.Empty)
            SearchbarTxt.Foreground = Brushes.DarkGray;
        else
            SearchbarTxt.Foreground = Brushes.Transparent;
    }

    private void SearchbarBox_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.IBeam;
        ColorAnimations.AnimateBackgroundColor(SearchBtnBorder, SearchBtnBorder.Background, Color.FromRgb(54, 54, 53), .1);
        ColorAnimations.AnimateBackgroundColor(SearchBar, SearchBar.Background, Color.FromRgb(54, 54, 53), .1);
        ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.White, .1);
    }

    private void SearchbarBox_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        if (!SearchbarBox.IsFocused)
        {
            ColorAnimations.AnimateForegroundColor(SearchBtnText, SearchBtnText.Foreground, Colors.DarkGray, .1);
            ColorAnimations.AnimateBackgroundColor(SearchBtnBorder, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
            ColorAnimations.AnimateBackgroundColor(SearchBar, SearchBar.Background, Color.FromRgb(31, 31, 31), .1);
        }
    }


    // Browse

    private void SearchbarBrowse_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Browse";
        HoverPopupHelper.PopupAppear(_mainWindow, SearchbarBrowse, PlacementMode.Bottom, _popupText);
        Mouse.OverrideCursor = Cursors.Hand;
    }

    private void SearchbarBrowse_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        Mouse.OverrideCursor = Cursors.Arrow;
    }

    private void SearchbarBrowse_Click(object sender, RoutedEventArgs e)
    {
        HomeBtn.Foreground = Brushes.DarkGray;
    }


    // What's New

    private void WhatsNewBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "What's New";
        HoverPopupHelper.PopupAppear(_mainWindow, WhatsNewBtn, PlacementMode.Bottom, _popupText);
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(WhatsNewBtn, WhatsNewBtn.Foreground, Colors.White, .1);
    }

    private void WhatsNewBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(WhatsNewBtn, WhatsNewBtn.Foreground, Colors.LightGray, .1);
    }

    private void WhatsNewBtn_Click(object sender, RoutedEventArgs e) { }


    // Friend Activity

    private void FriendActivityBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Friend Activity";
        HoverPopupHelper.PopupAppear(_mainWindow, FriendActivityBtn, PlacementMode.Bottom, _popupText);
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(FriendActivityBtn, FriendActivityBtn.Foreground, Colors.White, .1);
    }

    private void FriendActivityBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(FriendActivityBtn, FriendActivityBtn.Foreground, Colors.LightGray, .1);
    }

    private void FriendActivityBtn_Click(object sender, RoutedEventArgs e) { }


    // Avatar

    private void AvatarBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Aqua Studios";
        HoverPopupHelper.PopupAppear(_mainWindow, AvatarBorder, PlacementMode.Bottom, _popupText);
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(AvatarBorder, 1.03, .1);
    }

    private void AvatarBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        HoverPopupHelper.PopupDisappear(_mainWindow);
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(AvatarBorder, .1);
    }

    private void AvatarBtn_Click(object sender, RoutedEventArgs e) { }


    // Minimize

    private void MinimizeBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Minimize";
        HoverPopupHelper.PopupAppear(_mainWindow, MinimizeBtn, PlacementMode.Bottom, _popupText);
    }

    private void MinimizeBtn_MouseLeave(object sender, MouseEventArgs e) =>
        HoverPopupHelper.PopupDisappear(_mainWindow);

    private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        if (window != null)
            window.WindowState = WindowState.Minimized;
    }


    // Maximize

    private void MaximizeBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Maximize";
        HoverPopupHelper.PopupAppear(_mainWindow, MinimizeBtn, PlacementMode.Bottom, _popupText);
    }

    private void MaximizeBtn_MouseLeave(object sender, MouseEventArgs e) =>
        HoverPopupHelper.PopupDisappear(_mainWindow);

    private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        if (window != null)
        {
            if (!_isWindowedFullScreen)
            {
                // Store the original window dimensions and position
                _originalWidth = window.Width;
                _originalHeight = window.Height;
                _originalLeft = window.Left;
                _originalTop = window.Top;

                // Set the window to a "windowed full screen" state
                var screen = System.Windows.SystemParameters.WorkArea;
                window.Left = screen.Left;
                window.Top = screen.Top;
                window.Width = screen.Width;
                window.Height = screen.Height;

                _isWindowedFullScreen = true; // Indicate it's in full screen
            }
            else
            {
                // Restore the window to its original size and position
                window.Width = _originalWidth;
                window.Height = _originalHeight;
                window.Left = _originalLeft;
                window.Top = _originalTop;

                _isWindowedFullScreen = false; // Indicate it's no longer in full screen
            }
        }
    }


    // Close

    private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        _popupText.Text = "Close";
        HoverPopupHelper.PopupAppear(_mainWindow, CloseBtn, PlacementMode.Bottom, _popupText);
    }

    private void CloseBtn_MouseLeave(object sender, MouseEventArgs e) =>
        HoverPopupHelper.PopupDisappear(_mainWindow);

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this);
        if (window != null)
            window.Close();
    }
}
