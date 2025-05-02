using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.NowPlaying;

public class NowPlayingCreditsItemViewModel(string artist, bool isFollowing) : ViewModelBase
{
    #region Fields

    private bool _isFollowing = isFollowing;
    private string _artist = artist;

    #endregion

    #region Properties

    public bool IsFollowing
    {
        get => _isFollowing;
        set => SetProperty(ref _isFollowing, value);
    }

    public string Artist
    {
        get => _artist;
        set => SetProperty(ref _artist, value);
    }

    #endregion
}
