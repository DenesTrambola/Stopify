using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Views.SearchView;

public partial class SearchView : Page
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void SearchPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        int totalColumns;

        if (MainPanel.ActualWidth < 730)
            totalColumns = 2;
        else if (MainPanel.ActualWidth < 1000)
            totalColumns = 3;
        else
            totalColumns = 4;

        // Define your column widths based on the number of columns
        var gridLengths = new List<GridLength>();
        for (int i = 0; i < totalColumns; i++)
            gridLengths.Add(new GridLength(1, GridUnitType.Star));
        gridLengths.Add(new GridLength(1, GridUnitType.Auto));

        // Update the column widths dynamically
        BrowseCol1.Width = gridLengths[0];
        BrowseCol2.Width = gridLengths.Count > 1 ? gridLengths[1] : new GridLength(1, GridUnitType.Auto);
        BrowseCol3.Width = gridLengths.Count > 2 ? gridLengths[2] : new GridLength(1, GridUnitType.Auto);
        BrowseCol4.Width = gridLengths.Count > 3 ? gridLengths[3] : new GridLength(1, GridUnitType.Auto);

        // Store the UI elements in an array or list
        var elements = new UIElement[]
        {
            BrowseMusic, BrowsePodcasts, BrowseLiveEvents, BrowseMadeForYou, BrowseNewReleases, BrowsePop, BrowseHipHop,
            BrowseRock, BrowseMood, BrowseComedy, BrowseEducational, BrowseTrueCrime, BrowseSports, BrowseCharts,
            BrowseDanceElectronic,  BrowseChill, BrowseIndie, BrowseWorkout, BrowseDiscover, BrowseFolkAndAcoustic,
            BrowseRAndB, BrowseKPop, BrowseLatin, BrowseSleep, BrowseParty, BrowseAtHome, BrowseDecades, BrowseLove,
            BrowseMetal, BrowseJazz, BrowseTrending, BrowseClassical, BrowseCountry, BrowseFocus, BrowseSoul,
            BrowseKidsAndFamily, BrowseGaming, BrowseAnime, BrowseTvAndMovies, BrowseInstrumental, BrowseWellness,
            BrowsePunk, BrowseAmbient, BrowseBlues, BrowseCookingAndDining, BrowseAlternative, BrowseTravel,
            BrowseCaribbean, BrowseAfro, BrowseSongwriters, BrowseNatureAndNoise, BrowseFunkAndDisco, BrowseGlow,
            BrowseSpotifySingles, BrowseNetflix, BrowseSummer, BrowseRadar, BrowseEqual, BrowseFreshFinds
        };

        // Dynamically set row and column based on the total number of columns
        for (int i = 0; i < elements.Length; i++)
        {
            int row = i / totalColumns; // Calculate row
            int column = i % totalColumns; // Calculate column

            Grid.SetRow(elements[i], row);
            Grid.SetColumn(elements[i], column);
        }
    }
}
