using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain.Contracts.Other;
using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Stopify.Presentation.Views;

public partial class PlayerControl : UserControl, INotifyPropertyChanged
{
    private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
    public TextBlock _popupText = new();
    private DispatcherTimer _timer = new();
    private Random _random = new();
    private List<string> _queue;
    private MediaPlayer _mediaPlayer = new();
    private IAudioStorageService _audioService;

    private int _currentQueueIndex = 0;
    private bool _isDragging = false;
    private bool _isSaved = false;
    private bool _isShuffling = false;
    private bool _isPlaying = false;
    private byte _repeatValue = 0; // 0 - Off, 1 - All, 2 - One
    private string? _songPath;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private double _volume = 1;
    public double Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            OnPropertyChanged();
        }
    }

    private double _mediaValue = 0;
    public double MediaValue
    {
        get => _mediaValue;
        set
        {
            _mediaValue = value;
            OnPropertyChanged();
        }
    }

    private double _mediaMaxValue = 1;
    public double MediaMaxValue
    {
        get => _mediaMaxValue;
        set
        {
            _mediaMaxValue = value;
            OnPropertyChanged();
        }
    }

    private string _currentTime = "0:00";
    public string CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            OnPropertyChanged();
        }
    }

    private string _totalTime = "0:00";
    public string TotalTime
    {
        get => _totalTime;
        set
        {
            _totalTime = value;
            OnPropertyChanged();
        }
    }

    public PlayerControl()
    {
        DataContext = this;
        InitializeComponent();

        _audioService = ((App)App.Current).Services.GetService<IAudioStorageService>()!;
        Setup();
    }

    private async void Setup()
    {
        await FillQueue();
        _songPath = _queue?.FirstOrDefault();

        PopupTextSetup();
        PlayerSetup();
        TimerSetup();
    }


    // Player

    private void TimerSetup()
    {
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_mediaPlayer.Source != null && _mediaPlayer.NaturalDuration.HasTimeSpan)
        {
            MediaValue = _mediaPlayer.Position.TotalSeconds;
            CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
        }
    }

    private void PlayerSetup()
    {
        _mediaPlayer.Open(new Uri(_songPath));

        _mediaPlayer.MediaOpened += Player_Opened;
        _mediaPlayer.MediaEnded += RepeatDisabled;
    }

    private void Player_Ended(object? sender, EventArgs e) =>
        PlayNext();

    private void Player_Opened(object? sender, EventArgs e)
    {
        if (_mediaPlayer.NaturalDuration.HasTimeSpan)
        {
            MediaMaxValue = _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            TotalTime = _mediaPlayer.NaturalDuration.TimeSpan.ToString(@"m\:ss");
        }
    }

    private void PopupTextSetup()
    {
        _popupText.Foreground = Brushes.White;
        _popupText.Background = Brushes.Transparent;
        _popupText.FontWeight = FontWeights.SemiBold;
        _popupText.FontSize = 14;
    }

    private void OnSizeChange(object sender, SizeChangedEventArgs e)
    {
        if (MusicPlayer.ActualWidth <= 355)
        {
            OptionsBtnsColumn.Width = new GridLength(0.74, GridUnitType.Star);

            OptionsBtns.HorizontalAlignment = HorizontalAlignment.Stretch;
            OptionsBtns.Width = double.NaN;
        }
        else if (OptionsBtns.ActualWidth >= OptionsBtns.MaxWidth)
        {
            OptionsBtnsColumn.Width = GridLength.Auto;

            OptionsBtns.HorizontalAlignment = HorizontalAlignment.Right;
            OptionsBtns.Width = OptionsBtns.ActualWidth;
        }
    }

    private async Task FillQueue() =>
        _queue = (await _audioService.ListAudioFilesAsync()).ToList();


    // Slider

    private void Slider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Slider slider)
            return;

        _isDragging = true;
        UpdateSliderValue(slider, e);

        if (slider.Name == "MediaBar")
        {
            _timer.Stop();
            CurrentTime = TimeSpan.FromSeconds(slider.Value).ToString(@"m\:ss");
        }
    }

    private void Slider_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (_isDragging)
            UpdateSliderValue(sender as Slider, e);

        if (sender is Slider slider && slider.Name == "MediaBar" && e.LeftButton == MouseButtonState.Pressed)
            CurrentTime = TimeSpan.FromSeconds(slider.Value).ToString(@"m\:ss");
    }

    private void Slider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        _isDragging = false;

        if (sender is Slider slider && slider.Name == "MediaBar")
        {
            _mediaPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
            _timer.Start();
        }
    }

    private void UpdateSliderValue(Slider slider, MouseEventArgs e)
    {
        var mousePosition = e.GetPosition(slider);
        double relativePosition = mousePosition.X / slider.ActualWidth;
        double newValue = slider.Minimum + (relativePosition * (slider.Maximum - slider.Minimum));
        slider.Value = newValue;
    }

    private void Slider_MouseEnter(object sender, MouseEventArgs e)
    {
        var slider = sender as Slider;
        var track = (Track)slider.Template.FindName("PART_Track", slider);

        if (track != null)
        {
            var decreaseButton = (RepeatButton)track.DecreaseRepeatButton;
            if (decreaseButton != null)
                decreaseButton.Background = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        }
    }

    private void Slider_MouseLeave(object sender, MouseEventArgs e)
    {
        var slider = sender as Slider;
        var track = (Track)slider.Template.FindName("PART_Track", slider);

        if (track != null)
        {
            var decreaseButton = (RepeatButton)track.DecreaseRepeatButton;
            if (decreaseButton != null)
                decreaseButton.Background = new SolidColorBrush(Colors.White);
        }
    }


    // Song Image

    private void ImgBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        NowPlayingBorder.Visibility = Visibility.Visible;
        Mouse.OverrideCursor = Cursors.Hand;

        if (_mainWindow.NowPlayingCollapsed == true)
        {
            _popupText.Text = "Expand";
            NowPlayingBtn.Content = "\uf106";
        }
        else
        {
            _popupText.Text = "Collapse";
            NowPlayingBtn.Content = "\uf107";
        }

        HoverPopupHelper.PopupAppear(_mainWindow, ImgBtn, PlacementMode.Top, _popupText);
    }

    private void ImgBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        NowPlayingBorder.Visibility = Visibility.Hidden;
        Mouse.OverrideCursor = Cursors.Arrow;
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void ImgBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.NowPlayingCollapsed == true)
        {
            _mainWindow.NowPlayingCollapsed = false;
            _popupText.Text = "Collapse";
            NowPlayingBtn.Content = "\uf107";
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Color.FromRgb(30, 215, 96), .1);
        }
        else
        {
            _mainWindow.NowPlayingCollapsed = true;
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Colors.DarkGray, .1);
            _popupText.Text = "Expand";
            NowPlayingBtn.Content = "\uf106";
        }

        _mainWindow.UpdateNowPlaying();
    }


    // Title & Artist

    private void Underline_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;

        if (sender is not Button button)
            return;

        if (button.Content is TextBlock textBlock)
            textBlock.TextDecorations = TextDecorations.Underline;
        else if (button.Content is string text)
        {
            var newTextBlock = new TextBlock { Text = text, TextDecorations = TextDecorations.Underline };
            button.Content = newTextBlock;
        }
    }

    private void Underline_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;

        if (sender is not Button button)
            return;

        if (button.Content is TextBlock textBlock)
            textBlock.TextDecorations = null;
    }

    private void Title_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new PlaylistView.PlaylistView());

    private void Artist_Click(object sender, RoutedEventArgs e) =>
        _mainWindow.MainFrame.Navigate(new ArtistView());


    // Add to Liked Songs

    private void SaveBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(SaveBtn, 1.03, .1);
        _popupText.Text = _isSaved ? "Remove from Liked Songs" : "Save to Liked Songs";
        HoverPopupHelper.PopupAppear(_mainWindow, SaveBtn, PlacementMode.Top, _popupText);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
        }
    }

    private void SaveBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(SaveBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);

        if (!_isSaved)
        {
            SaveBorder.BorderBrush = Brushes.DarkGray;
            SaveText.Foreground = Brushes.DarkGray;
        }
    }

    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isSaved)
        {
            SaveText.Margin = new Thickness(1.5, .7, 0, 0);
            SaveText.Text = "+";
            SaveBorder.Background = Brushes.Transparent;
            SaveBorder.BorderThickness = new Thickness(2);
            SaveBorder.BorderBrush = Brushes.White;
            SaveText.Foreground = Brushes.White;
            _popupText.Text = "Save to Liked Songs";
            _isSaved = false;
        }
        else
        {
            SaveText.Margin = new Thickness(3, 3, 0, 0);
            SaveText.Text = "\uf00c";
            SaveBorder.Background = new SolidColorBrush(Color.FromRgb(30, 215, 96));
            SaveBorder.BorderThickness = new Thickness(0);
            SaveBorder.BorderBrush = Brushes.Transparent;
            SaveText.Foreground = Brushes.Black;
            _popupText.Text = "Remove from Liked Songs";
            _isSaved = true;
        }
    }


    // Shuffle

    private void ShuffleBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(ShuffleBtn, 1.03, .1);
        _popupText.Text = _isShuffling ? "Disable Shuffle for Azahriah" : "Enable Shuffle for Azahriah";
        HoverPopupHelper.PopupAppear(_mainWindow, ShuffleBtn, PlacementMode.Top, _popupText);

        if (!_isShuffling)
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Colors.White, .1);
    }

    private void ShuffleBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(ShuffleBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);

        if (!_isShuffling)
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Colors.DarkGray, .1);
    }

    private void ShuffleBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isShuffling)
        {
            _isShuffling = false;
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Colors.DarkGray, .1);
            _popupText.Text = "Enable Shuffle for Azahriah";

            _queue = new();
            FillQueue();
        }
        else
        {
            _isShuffling = true;
            ColorAnimations.AnimateForegroundColor(ShuffleBtn, ShuffleBtn.Foreground, Color.FromRgb(30, 215, 96), .1);
            _popupText.Text = "Disable Shuffle for Azahriah";

            ShuffleQueue();
        }
    }

    private void ShuffleQueue()
    {
        int n = _queue.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            var temp = _queue[i];
            _queue[i] = _queue[j];
            _queue[j] = temp;
        }
    }


    // Previous

    private void PreviousBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(PreviousBtn, 1.03, .1);
        _popupText.Text = "Previous";
        HoverPopupHelper.PopupAppear(_mainWindow, PreviousBtn, PlacementMode.Top, _popupText);
        ColorAnimations.AnimateForegroundColor(PreviousBtn, PreviousBtn.Foreground, Colors.White, .1);
    }

    private void PreviousBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(PreviousBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
        ColorAnimations.AnimateForegroundColor(PreviousBtn, PreviousBtn.Foreground, Colors.DarkGray, .1);
    }

    private void PreviousBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_currentQueueIndex <= 0)
        {
            _currentQueueIndex = _queue.Count - 1;
            _songPath = _queue[^1];
        }
        else
            _songPath = _queue[--_currentQueueIndex];

        if (!_isPlaying) PlayMedia();
        _mediaPlayer.Open(new Uri(_songPath));
        _mediaPlayer.Play();
        CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
        if (_isShuffling) ShuffleQueue();
    }


    // Next

    private void NextBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(NextBtn, 1.03, .1);
        _popupText.Text = "Next";
        HoverPopupHelper.PopupAppear(_mainWindow, NextBtn, PlacementMode.Top, _popupText);
        ColorAnimations.AnimateForegroundColor(NextBtn, NextBtn.Foreground, Colors.White, .1);
    }

    private void NextBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(NextBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
        ColorAnimations.AnimateForegroundColor(NextBtn, NextBtn.Foreground, Colors.DarkGray, .1);
    }

    private void NextBtn_Click(object sender, RoutedEventArgs e) =>
        PlayNext();

    private void PlayNext()
    {
        if (_currentQueueIndex >= _queue.Count - 1)
        {
            _currentQueueIndex = 0;
            _songPath = _queue[0];
        }
        else
            _songPath = _queue[++_currentQueueIndex];

        if (!_isPlaying) PlayMedia();
        _mediaPlayer.Open(new Uri(_songPath));
        _mediaPlayer.Play();
        CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
    }


    // Play/Pause

    private void PlayBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(PlayBorder, 1.04, .1);
        _popupText.Text = _isPlaying ? "Pause" : "Play";
        HoverPopupHelper.PopupAppear(_mainWindow, PlayBorder, PlacementMode.Top, _popupText);
    }

    private void PlayBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(PlayBorder, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_isPlaying) PauseMedia();
        else PlayMedia();
    }

    private void PlayMedia()
    {
        _isPlaying = true;
        PlayBtn.Content = "\uf04c";
        PlayBtn.FontSize = 19;
        _popupText.Text = "Pause";
        _mediaPlayer.Play();
        MediaValue = _mediaPlayer.Position.TotalSeconds;
        _timer.Start();
    }

    private void PauseMedia()
    {
        _isPlaying = false;
        PlayBtn.Content = "\uf04b";
        PlayBtn.FontSize = 17;
        _popupText.Text = "Play";
        _mediaPlayer.Pause();
        _timer.Stop();
    }


    // Repeat

    private void RepeatBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(RepeatBtn, 1.04, .1);

        switch (_repeatValue)
        {
            case 0:
                _popupText.Text = "Enable repeat";
                ColorAnimations.AnimateForegroundColor(RepeatBtn, RepeatBtn.Foreground, Colors.White, .1);
                break;
            case 1:
                _popupText.Text = "Enable repeat one";
                break;
            case 2:
                _popupText.Text = "Disable repeat";
                break;
            default:
                _popupText.Text = "ERROR!";
                break;
        }

        HoverPopupHelper.PopupAppear(_mainWindow, RepeatBtn, PlacementMode.Top, _popupText);
    }

    private void RepeatBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(RepeatBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
        if (_repeatValue == 0)
            ColorAnimations.AnimateForegroundColor(RepeatBtn, RepeatBtn.Foreground, Colors.DarkGray, .1);
    }

    private void RepeatBtn_Click(object sender, RoutedEventArgs e)
    {
        switch (_repeatValue)
        {
            case 0:
                _repeatValue = 1;
                _popupText.Text = "Enable repeat one";
                ColorAnimations.AnimateForegroundColor(RepeatBtn, RepeatBtn.Foreground, Color.FromRgb(30, 215, 96), .1);
                _mediaPlayer.MediaEnded -= RepeatDisabled;
                _mediaPlayer.MediaEnded += RepeatAll;
                break;

            case 1:
                _repeatValue = 2;
                _popupText.Text = "Disable repeat";
                RepeatText.Visibility = Visibility.Visible;
                _mediaPlayer.MediaEnded -= RepeatAll;
                _mediaPlayer.MediaEnded += RepeatOne;
                break;

            case 2:
                _repeatValue = 0;
                _popupText.Text = "Enable repeat";
                ColorAnimations.AnimateForegroundColor(RepeatBtn, RepeatBtn.Foreground, Colors.White, .1);
                RepeatText.Visibility = Visibility.Hidden;
                _mediaPlayer.MediaEnded -= RepeatOne;
                _mediaPlayer.MediaEnded += RepeatDisabled;
                break;

            default:
                _popupText.Text = "ERROR!";
                break;
        }
    }

    private void RepeatDisabled(object? sender, EventArgs e)
    {
        PauseMedia();
        _mediaPlayer.Stop();
        _mediaPlayer.Position = TimeSpan.Zero;
        MediaValue = _mediaPlayer.Position.TotalSeconds;
        CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
    }

    private void RepeatAll(object? sender, EventArgs e) =>
        PlayNext();

    private void RepeatOne(object? sender, EventArgs e)
    {
        _mediaPlayer.Stop();
        _mediaPlayer.Position = TimeSpan.Zero;
        MediaValue = _mediaPlayer.Position.TotalSeconds;
        CurrentTime = _mediaPlayer.Position.ToString(@"m\:ss");
        _mediaPlayer.Play();
    }


    // Now Playing Option

    private void NowPlayingOption_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ScaleAnimations.BeginScaleAnimation(NowPlayingOption, 1.03, .1);
        _popupText.Text = "Now playing view";
        HoverPopupHelper.PopupAppear(_mainWindow, NowPlayingOption, PlacementMode.Top, _popupText);
        if (_mainWindow.NowPlayingCollapsed == true)
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Colors.White, .1);
    }

    private void NowPlayingOption_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ScaleAnimations.ResetScaleAnimation(NowPlayingOption, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
        if (_mainWindow.NowPlayingCollapsed == true)
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Colors.DarkGray, .1);
    }

    private void NowPlayingOption_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.NowPlayingCollapsed == true)
        {
            _mainWindow.NowPlayingCollapsed = false;
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Color.FromRgb(30, 215, 96), .1);
        }
        else
        {
            _mainWindow.NowPlayingCollapsed = true;
            ColorAnimations.AnimateForegroundColor(NowPlayingOption, NowPlayingOption.Foreground, Colors.White, .1);
        }

        _mainWindow.UpdateNowPlaying();
    }


    // Lyrics

    private void LyricsBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.No;
        _popupText.Text = "Lyrics";
        HoverPopupHelper.PopupAppear(_mainWindow, LyricsBtn, PlacementMode.Top, _popupText);
    }

    private void LyricsBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void LyricsBtn_Click(object sender, RoutedEventArgs e) { }


    // Queue

    private void QueueBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        _popupText.Text = "Queue";
        HoverPopupHelper.PopupAppear(_mainWindow, QueueBtn, PlacementMode.Top, _popupText);
        if (_mainWindow.QueueCollapsed)
            ColorAnimations.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.White, .1);
    }

    private void QueueBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        HoverPopupHelper.PopupDisappear(_mainWindow);
        if (_mainWindow.QueueCollapsed)
            ColorAnimations.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.DarkGray, .1);
    }

    private void QueueBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.QueueCollapsed)
        {
            _mainWindow.QueueCollapsed = false;
            ColorAnimations.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Color.FromRgb(30, 215, 96), .1);
        }
        else
        {
            _mainWindow.QueueCollapsed = true;
            ColorAnimations.AnimateForegroundColor(QueueBtn, QueueBtn.Foreground, Colors.White, .1);
        }
        _mainWindow.UpdateQueue();
    }


    // Connect to Device

    private void ConnectBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.No;
        _popupText.Text = "Connect to device";
        HoverPopupHelper.PopupAppear(_mainWindow, ConnectBtn, PlacementMode.Top, _popupText);
    }

    private void ConnectBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void ConnectBtn_Click(object sender, RoutedEventArgs e) { }


    // Volume

    private void VolumeBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(VolumeBtn, VolumeBtn.Foreground, Colors.White, .1);
        _popupText.Text = _mediaPlayer.IsMuted ? "Unmute" : "Mute";
        HoverPopupHelper.PopupAppear(_mainWindow, VolumeBtn, PlacementMode.Top, _popupText);
    }

    private void VolumeBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(VolumeBtn, VolumeBtn.Foreground, Colors.DarkGray, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void VolumeBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mediaPlayer.IsMuted)
        {
            VolumeBtn.Content = "\uf028";
            _popupText.Text = "Mute";
            VolumeBarCover.Opacity = 0;
            Canvas.SetZIndex(VolumeBarCover, 0);
            VolumeBtn.Margin = new Thickness(0, 0, 1, 0);
            _mediaPlayer.IsMuted = false;
        }
        else
        {
            VolumeBtn.Content = "\uf6a9";
            _popupText.Text = "Unmute";
            VolumeBarCover.Opacity = .6;
            Canvas.SetZIndex(VolumeBarCover, 2);
            VolumeBtn.Margin = new Thickness(0, 0, 3, 0);
            _mediaPlayer.IsMuted = true;
        }
    }

    private void VolumeBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) =>
        _mediaPlayer.Volume = VolumeBar.Value;


    // Full Screen

    private void FullScreenBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(FullScreenBtn, FullScreenBtn.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(FullScreenBtn, 1.03, .1);
        _popupText.Text = _mainWindow.FullScreen ? "Exit full screen" : "Full screen";
        HoverPopupHelper.PopupAppear(_mainWindow, FullScreenBtn, PlacementMode.Top, _popupText);
    }

    private void FullScreenBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(FullScreenBtn, FullScreenBtn.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(FullScreenBtn, .1);
        HoverPopupHelper.PopupDisappear(_mainWindow);
    }

    private void FullScreenBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_mainWindow.FullScreen)
        {
            _popupText.Text = "Full screen";
            _mainWindow.FullScreen = false;
        }
        else
        {
            _popupText.Text = "Exit full screen";
            _mainWindow.FullScreen = true;
        }
    }
}
