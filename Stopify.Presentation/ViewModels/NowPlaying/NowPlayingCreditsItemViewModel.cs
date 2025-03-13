using Stopify.Presentation.ViewModels.Base;

namespace Stopify.Presentation.ViewModels.NowPlaying;

public class NowPlayingCreditsItemViewModel : ViewModelBase
{
    #region Fields

    private string _artist;
    private string _followStatus;

    #endregion

    #region Properties

    public string Artist
    {
        get => _artist;
        set => SetProperty(ref _artist, value);
    }

    public string FollowStatus
    {
        get => _followStatus;
        set => SetProperty(ref _followStatus, value);
    }

    #endregion

    #region Constructors

    public NowPlayingCreditsItemViewModel(string artist, string followStatus)
    {
        Artist = artist;
        FollowStatus = followStatus;
    }

    #endregion
}
