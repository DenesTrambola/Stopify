﻿using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Common;

public class CommonRowViewModel : ViewModelBase
{
    private string _category;
    private ObservableCollection<CommonItemViewModel> _items;

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public IEnumerable<CommonItemViewModel> Items => _items;

    public CommonRowViewModel(string category)
    {
        Category = category;

        _items = new ObservableCollection<CommonItemViewModel> {
            new CommonItemViewModel("Azahriah", "Artist", String.Empty),
            new CommonItemViewModel("DESH", "Artist", String.Empty),
            new CommonItemViewModel("YoungFly", "Artist", String.Empty),
            new CommonItemViewModel("Nessaj", "Streamer", String.Empty),
            new CommonItemViewModel("Baukó Attila", "Artist", String.Empty),
            new CommonItemViewModel("Azahriah", "Gamer", String.Empty),
            new CommonItemViewModel("Azahriah", "Bunko", String.Empty),
            new CommonItemViewModel("Azahriah", "Rozsda", String.Empty),
        };
    }
}
