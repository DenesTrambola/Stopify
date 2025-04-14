using Stopify.Presentation.Utilities.Commands.Common;
using Stopify.Presentation.ViewModels.Base;
using System.Windows.Input;

namespace Stopify.Presentation.ViewModels.Common;

public class AuthorItemViewModel : ViewModelBase
{
    #region Fields

    private string _authorName;

    #endregion

    #region Properties

    public string AuthorName
    {
        get => _authorName;
        set => SetProperty(ref _authorName, value);
    }

    #endregion

    #region Commands

    public ICommand NavigateArtistCommand { get; }

    #endregion

    #region Costructors

    public AuthorItemViewModel(string authorName, NavigateArtistCommand navigateArtistCommand)
    {
        AuthorName = authorName;
        NavigateArtistCommand = navigateArtistCommand;
    }

    #endregion
}
