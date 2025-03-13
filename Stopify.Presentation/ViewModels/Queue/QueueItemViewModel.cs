using Stopify.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Stopify.Presentation.ViewModels.Queue;

public class QueueItemViewModel : ViewModelBase
{
    #region Fields

    private string _title;
    private string _imagePath;

    private ObservableCollection<string> _authors;

    #endregion

    #region Properties

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public IEnumerable<string> Authors => _authors;

    #endregion

    #region Constructors

    public QueueItemViewModel(string title, string imagePath)
    {
        Title = title;
        ImagePath = imagePath;

        _authors = new ObservableCollection<string>()
        {
            "Azahriah",
            "DESH",
            "Young Fly",
        };
    }

    #endregion
}
