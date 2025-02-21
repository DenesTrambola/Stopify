using Stopify.Presentation.Helpers.Animations;
using Stopify.Presentation.Helpers.Utilities;
using Stopify.Presentation.Views.PlaylistView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.SidebarView;

public partial class SidebarViewItem : UserControl
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    private TextBlock _basicPopupText = new();
    private TextBlock _popupText1 = new();
    private TextBlock _popupText2 = new();
    private bool _isPlaying = false;

    public SidebarViewItem()
    {
        InitializeComponent();

        _basicPopupText.Foreground = Brushes.White;
        _basicPopupText.FontWeight = FontWeights.SemiBold;
        _basicPopupText.FontSize = 14;

        _popupText1.Foreground = Brushes.White;
        _popupText1.FontWeight = FontWeights.SemiBold;
        _popupText1.FontSize = 15;

        _popupText2.Foreground = Brushes.DarkGray;
        _popupText2.FontWeight = FontWeights.SemiBold;
        _popupText2.FontSize = 13;
    }

    private void ItemBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(31, 31, 31), .1);

        if (_mainWindow.SidebarCollapsed == true)
        {
            _popupText1.Text = "Azahriah";
            _popupText2.Text = "Artist";
            PopupHelper.PopupAppear(_mainWindow, ItemBtn, PlacementMode.Right, _popupText1, _popupText2);
        }
        else
        {
            PlayBtn.Width = double.NaN;
            ItemImgBtn.Opacity = .4;
        }

        if (_isPlaying)
        {
            PlayBtn.Content = "\uf04c";
            PlayBtn.FontSize = 25;
        }
        else
        {
            PlayBtn.Content = "\uf04b";
            PlayBtn.FontSize = 22;
        }
    }

    private void ItemBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateBackgroundColor(ItemBorder, ItemBorder.Background, Color.FromRgb(18, 18, 18), .1);

        if (_mainWindow.SidebarCollapsed == true)
            PopupHelper.PopupDisappear(_mainWindow);
        else
        {
            PlayBtn.Width = 0;
            ItemImgBtn.Opacity = 1;
        }
    }

    private void ItemBtn_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new PlaylistViewMain());

    private void ItemImgBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        if (!_mainWindow.SidebarCollapsed == true)
        {
            ScaleAnimations.BeginScaleAnimation(PlayBtn, 1.02, .1);
            if (_isPlaying)
                _basicPopupText.Text = "Play Azahriah";
            else
                _basicPopupText.Text = "Pause Azahriah";
            PopupHelper.PopupAppear(_mainWindow, ItemImgBtn, PlacementMode.Top, _basicPopupText);
        }
    }

    private void ItemImgBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        if (_mainWindow.SidebarCollapsed == false)
        {
            ScaleAnimations.ResetScaleAnimation(PlayBtn, .1);
            PopupHelper.PopupDisappear(_mainWindow);
        }
    }

    private void PlayPauseEvent(object sender, RoutedEventArgs e)
    {
        if (_isPlaying)
        {
            PlayBtn.Content = "\uf04b";
            PlayBtn.FontSize = 22;
            SidebarPlayingIcon.Width = 0;
            _isPlaying = false;
        }
        else
        {
            PlayBtn.Content = "\uf04c";
            PlayBtn.FontSize = 25;
            SidebarPlayingIcon.Width = double.NaN;
            _isPlaying = true;
        }
    }
}
