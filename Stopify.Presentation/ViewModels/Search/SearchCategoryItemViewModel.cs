using Stopify.Presentation.ViewModels.Base;
using System.Windows;

namespace Stopify.Presentation.ViewModels.Search;

public class SearchCategoryItemViewModel : ViewModelBase
{
    #region Fields

    private string _image;

    #endregion

    #region Properties

    public string Image
    {
        get => _image;
        set
        {
            try
            {
                SetProperty(ref _image, "D:\\IT Step\\C#\\Stopify\\Stopify.Presentation\\Assets\\Images\\SearchPage\\" + value + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Search Category Image " + value + "misspelled!");
            }
        }
    }

    #endregion

    #region Constructors

    public SearchCategoryItemViewModel(string image)
    {
        Image = image;
    }

    #endregion
}
