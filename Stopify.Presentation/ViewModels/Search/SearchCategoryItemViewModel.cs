using Stopify.Presentation.ViewModels.Base;
using System.IO;
using System.Windows;

namespace Stopify.Presentation.ViewModels.Search;

public class SearchCategoryItemViewModel : ViewModelBase
{
    #region Fields

    private string _imagePath = string.Empty;

    #endregion

    #region Properties

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            try
            {
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
                string imagePath = Path.Combine(projectDirectory, "Assets", "Images", "SearchPage", $"{value}.png");
                SetProperty(ref _imagePath, imagePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Search Category Image " + value + "does not exist in this directory!");
            }
        }
    }

    #endregion

    #region Constructors

    public SearchCategoryItemViewModel(string imagePath)
    {
        ImagePath = imagePath;
    }

    #endregion
}
