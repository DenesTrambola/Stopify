using Stopify.Presentation.Helpers.Animations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Views.CommonViews;

public partial class CommonRowView : UserControl
{
    public static readonly DependencyProperty CategoryProperty =
        DependencyProperty.Register(nameof(Category), typeof(string), typeof(CommonRowView), new PropertyMetadata(string.Empty));

    public string Category
    {
        get { return (string)GetValue(CategoryProperty); }
        set { SetValue(CategoryProperty, value); }
    }


    public CommonRowView()
    {
        InitializeComponent();
    }


    // Category

    private void CategoryBtn_MouseEnter(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Hand;

    private void CategoryBtn_MouseLeave(object sender, MouseEventArgs e) =>
        Mouse.OverrideCursor = Cursors.Arrow;

    private void CategoryBtn_Click(object sender, RoutedEventArgs e) { }


    // Show All

    private void ShowAllBtn_MouseEnter(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Hand;
        ColorAnimations.AnimateForegroundColor(ShowAllBtn, ShowAllBtn.Foreground, Colors.White, .1);
        ScaleAnimations.BeginScaleAnimation(ShowAllBtn, 1.01, .1);
    }

    private void ShowAllBtn_MouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        ColorAnimations.AnimateForegroundColor(ShowAllBtn, ShowAllBtn.Foreground, Colors.DarkGray, .1);
        ScaleAnimations.ResetScaleAnimation(ShowAllBtn, .1);
    }

    private void ShowAllBtn_Click(object sender, RoutedEventArgs e) { }
}
