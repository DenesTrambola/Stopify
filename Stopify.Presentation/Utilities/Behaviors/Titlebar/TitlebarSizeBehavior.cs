using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Utilities.Behaviors.Titlebar;

public static class TitlebarSizeBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableTitlebarDynamicSizingProperty =
        DependencyProperty.RegisterAttached(
            "EnableTitlebarDynamicSizing",
            typeof(bool),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(false, OnTitlebarSizeChanged));

    public static readonly DependencyProperty SearchbarTextWidthProperty =
        DependencyProperty.RegisterAttached(
            "SearchbarTextWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty SearchbarInputWidthProperty =
        DependencyProperty.RegisterAttached(
            "SearchbarInputWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty SearchbarLineWidthProperty =
        DependencyProperty.RegisterAttached(
            "SearchbarLineWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty SearchbarBrowseWidthProperty =
        DependencyProperty.RegisterAttached(
            "SearchbarBrowseWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty SearchBarWidthProperty =
        DependencyProperty.RegisterAttached(
            "SearchBarWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty SearchBtnBorderRadiusProperty =
        DependencyProperty.RegisterAttached(
            "SearchBtnBorderRadius",
            typeof(CornerRadius),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(new CornerRadius(0)));

    public static readonly DependencyProperty SearchbarInputProperty =
        DependencyProperty.RegisterAttached(
            "SearchbarInput",
            typeof(string),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty NewsBtnWidthProperty =
        DependencyProperty.RegisterAttached(
            "NewsBtnWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty FriendActivityBtnWidthProperty =
        DependencyProperty.RegisterAttached(
            "FriendActivityBtnWidth",
            typeof(double),
            typeof(TitlebarSizeBehavior),
            new PropertyMetadata(0.0));

    #endregion

    #region Getters/Setters

    public static void SetEnableTitlebarDynamicSizing(UIElement element, bool value) =>
        element.SetValue(EnableTitlebarDynamicSizingProperty, value);
    public static bool GetEnableTitlebarDynamicSizing(UIElement element) =>
        (bool)element.GetValue(EnableTitlebarDynamicSizingProperty);

    public static void SetSearchbarTextWidth(UIElement element, double value) =>
        element.SetValue(SearchbarTextWidthProperty, value);
    public static double GetSearchbarTextWidth(UIElement element) =>
        (double)element.GetValue(SearchbarTextWidthProperty);

    public static void SetSearchbarInputWidth(UIElement element, double value) =>
        element.SetValue(SearchbarInputWidthProperty, value);
    public static double GetSearchbarInputWidth(UIElement element) =>
        (double)element.GetValue(SearchbarInputWidthProperty);

    public static void SetSearchbarLineWidth(UIElement element, double value) =>
        element.SetValue(SearchbarLineWidthProperty, value);
    public static double GetSearchbarLineWidth(UIElement element) =>
        (double)element.GetValue(SearchbarLineWidthProperty);

    public static void SetSearchbarBrowseWidth(UIElement element, double value) =>
        element.SetValue(SearchbarBrowseWidthProperty, value);
    public static double GetSearchbarBrowseWidth(UIElement element) =>
        (double)element.GetValue(SearchbarBrowseWidthProperty);

    public static void SetSearchBarWidth(UIElement element, double value) =>
        element.SetValue(SearchBarWidthProperty, value);
    public static double GetSearchBarWidth(UIElement element) =>
        (double)element.GetValue(SearchBarWidthProperty);

    public static void SetSearchBtnBorderRadius(UIElement element, CornerRadius value) =>
        element.SetValue(SearchBtnBorderRadiusProperty, value);
    public static CornerRadius GetSearchBtnBorderRadius(UIElement element) =>
        (CornerRadius)element.GetValue(SearchBtnBorderRadiusProperty);

    public static void SetSearchbarInput(UIElement element, string value) =>
        element.SetValue(SearchbarInputProperty, value);
    public static string GetSearchbarInput(UIElement element) =>
        (string)element.GetValue(SearchbarInputProperty);

    public static void SetNewsBtnWidth(UIElement element, double value) =>
        element.SetValue(NewsBtnWidthProperty, value);
    public static double GetNewsBtnWidth(UIElement element) =>
        (double)element.GetValue(NewsBtnWidthProperty);

    public static void SetFriendActivityBtnWidth(UIElement element, double value) =>
        element.SetValue(FriendActivityBtnWidthProperty, value);
    public static double GetFriendActivityBtnWidth(UIElement element) =>
        (double)element.GetValue(FriendActivityBtnWidthProperty);

    #endregion

    #region Event Handlers

    private static void OnTitlebarSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not UserControl element) return;

        if ((bool)e.NewValue)
            element.SizeChanged += OnTitlebarSizeChanged;
        else
            element.SizeChanged -= OnTitlebarSizeChanged;
    }

    private static void OnTitlebarSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not UserControl element) return;

        if (element.ActualWidth <= 850 && GetSearchBarWidth(element) != 0)
        {
            SetSearchbarTextWidth(element, 0);
            SetSearchbarInputWidth(element, 0);
            SetSearchbarLineWidth(element, 0);
            SetSearchbarBrowseWidth(element, 0);
            SetSearchBarWidth(element, 0);
            SetSearchBtnBorderRadius(element, new CornerRadius(30));
            SetSearchbarInput(element, string.Empty);

            SetNewsBtnWidth(element, 0);
            SetFriendActivityBtnWidth(element, 0);
        }
        else if (element.ActualWidth > 850 && GetSearchBarWidth(element) == 0)
        {
            SetSearchbarTextWidth(element, double.NaN);
            SetSearchbarInputWidth(element, double.NaN);
            SetSearchbarLineWidth(element, double.NaN);
            SetSearchbarBrowseWidth(element, double.NaN);
            SetSearchBarWidth(element, double.NaN);
            SetSearchBtnBorderRadius(element, new CornerRadius(23, 0, 0, 23));
        }
        else if (GetNewsBtnWidth(element) == 0 && GetFriendActivityBtnWidth(element) == 0)
        {
            SetNewsBtnWidth(element, double.NaN);
            SetFriendActivityBtnWidth(element, double.NaN);
        }
    }

    #endregion
}
