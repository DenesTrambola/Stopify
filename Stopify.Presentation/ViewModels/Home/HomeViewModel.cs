﻿using Stopify.Presentation.ViewModels.Base;
using Stopify.Presentation.ViewModels.Common;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Home;

public class HomeViewModel : ViewModelBase
{
    #region Fields

    private int _columnCount;

    private bool _isAllFiltered = true;
    private bool _isMusicFiltered = false;
    private bool _isPodcastsFiltered = false;

    public ObservableCollection<CommonRowViewModel> _rows;
    public ObservableCollection<HomeRecentPlaysItemViewModel> _recentPlays;

    #endregion

    #region Properties

    public int ColumnCount
    {
        get => _columnCount;
        set => SetProperty(ref _columnCount, value);
    }

    public bool IsAllFiltered
    {
        get => _isAllFiltered;
        set => SetProperty(ref _isAllFiltered, value);
    }

    public bool IsMusicFiltered
    {
        get => _isMusicFiltered;
        set => SetProperty(ref _isMusicFiltered, value);
    }

    public bool IsPodcastsFiltered
    {
        get => _isPodcastsFiltered;
        set => SetProperty(ref _isPodcastsFiltered, value);
    }

    public ObservableCollection<CommonRowViewModel> Rows => _rows;

    public ObservableCollection<HomeRecentPlaysItemViewModel> RecentPlays => _recentPlays;

    #endregion

    #region Constructors

    public HomeViewModel()
    {
        _rows = new ObservableCollection<CommonRowViewModel>()
        {
            new CommonRowViewModel("Recently played"),
            new CommonRowViewModel("Your favorite artists"),
            new CommonRowViewModel("Jump back in"),
            new CommonRowViewModel("Best of artists"),
            new CommonRowViewModel("Recommended for today"),
            new CommonRowViewModel("New releases for you"),
            new CommonRowViewModel("More like {Artist}"),
            new CommonRowViewModel("Fresh new music"),
            new CommonRowViewModel("More like {Artist}"),
            new CommonRowViewModel("Popular artists"),
            new CommonRowViewModel("For fans of {Artist}"),
        };

        _recentPlays = new ObservableCollection<HomeRecentPlaysItemViewModel>
        {
            new HomeRecentPlaysItemViewModel("Azahriah", false, ""),
            new HomeRecentPlaysItemViewModel("DESH", false, ""),
            new HomeRecentPlaysItemViewModel("YoungFly", false, ""),
            new HomeRecentPlaysItemViewModel("Nessaj", false, ""),
            new HomeRecentPlaysItemViewModel("Coding Music", false, ""),
            new HomeRecentPlaysItemViewModel("Gym Songs", false, ""),
            new HomeRecentPlaysItemViewModel("Calisthenics", false, ""),
            new HomeRecentPlaysItemViewModel("Toth Gabi", false, ""),
        };

        ColumnCount = 2;
    }

    #endregion
}
