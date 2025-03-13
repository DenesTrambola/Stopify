using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.Player;

public class PlayerViewModel : ViewModelBase
{
    private string _imagePath;
    private string _title;
    private string _artist;
    private string _currentTime;
    private string _totalTime;
    private double _maxMediaValue;
    private double _currentMediaValue;
    private double _volumeValue;

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Artist
    {
        get => _artist;
        set => SetProperty(ref _artist, value);
    }

    public string CurrentTime
    {
        get => _currentTime;
        set => SetProperty(ref _currentTime, value);
    }

    public string TotalTime
    {
        get => _totalTime;
        set => SetProperty(ref _totalTime, value);
    }

    public double MaxMediaValue
    {
        get => _maxMediaValue;
        set => SetProperty(ref _maxMediaValue, value);
    }

    public double CurrentMediaValue
    {
        get => _currentMediaValue;
        set => SetProperty(ref _currentMediaValue, value);
    }

    public double VolumeValue
    {
        get => _volumeValue;
        set => SetProperty(ref _volumeValue, value);
    }

    public PlayerViewModel()
    {
        ImagePath = String.Empty;
        Title = "introvertált dal";
        Artist = "Azahriah";
        CurrentTime = "00:00";
        TotalTime = "2:45";
        MaxMediaValue = 100;
        CurrentMediaValue = 0;
        VolumeValue = 50;
    }
}
