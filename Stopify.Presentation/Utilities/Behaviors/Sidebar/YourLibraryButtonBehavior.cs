using Stopify.Presentation.Utilities.Animations;
using Stopify.Presentation.Utilities.Helpers;
using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Stopify.Presentation.Utilities.Behaviors.Sidebar;

public static class YourLibraryButtonBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(YourLibraryButtonBehavior),
            new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty YourLibraryTextProperty =
        DependencyProperty.RegisterAttached(
            "YourLibraryText",
            typeof(TextBlock),
            typeof(YourLibraryButtonBehavior),
            new PropertyMetadata(null));

    public static readonly DependencyProperty SearchGridHeightProperty =
        DependencyProperty.RegisterAttached(
            "SearchGridHeight",
            typeof(double),
            typeof(YourLibraryButtonBehavior),
            new PropertyMetadata((double)0));

    #endregion

    #region Getters/Setters

    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);

    public static TextBlock GetYourLibraryText(DependencyObject obj) =>
        (TextBlock)obj.GetValue(YourLibraryTextProperty);
    public static void SetYourLibraryText(DependencyObject obj, TextBlock value) =>
        obj.SetValue(YourLibraryTextProperty, value);

    public static double GetSearchGridHeight(DependencyObject obj) =>
        (double)obj.GetValue(SearchGridHeightProperty);
    public static void SetSearchGridHeight(DependencyObject obj, double value) =>
        obj.SetValue(SearchGridHeightProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
        {
            element.MouseEnter += OnMouseEnter;
            element.MouseLeave += OnMouseLeave;
            element.Click += OnClick;
            element.Unloaded += DetachEvents;
        }
        else
        {
            element.MouseEnter -= OnMouseEnter;
            element.MouseLeave -= OnMouseLeave;
            element.Click -= OnClick;
            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void OnMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not Button element) return;

        TextBlock yourLibraryText = GetYourLibraryText(element);
        MainView mainView = (MainView)Application.Current.MainWindow;

        ColorAnimations.AnimateForeground(element, Colors.White, .1);
        ColorAnimations.AnimateForeground(yourLibraryText, Colors.White, .1);

        HoverPopupHelper.DisplayPopupText(element, PlacementMode.MousePoint,
            mainView.SidebarCollapsed == false ? "Collapse Your Library" : "Expand Your Library");
    }

    private static void OnMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not Button element) return;

        TextBlock yourLibraryText = GetYourLibraryText(element);

        ColorAnimations.AnimateForeground(element, Color.FromRgb(179, 179, 179), .1);
        ColorAnimations.AnimateForeground(yourLibraryText, Color.FromRgb(179, 179, 179), .1);

        HoverPopupHelper.HidePopup();
    }

    private static void OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        MainView mainView = (MainView)Application.Current.MainWindow;

        if (mainView.SidebarCollapsed != false)
        {
            SetSearchGridHeight(element, double.NaN);
            mainView.SidebarCollapsed = false;
        }
        else
        {
            SetSearchGridHeight(element, 0);
            mainView.SidebarCollapsed = true;
        }

        mainView.UpdateSidebar();
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        element.MouseEnter -= OnMouseEnter;
        element.MouseLeave -= OnMouseLeave;
        element.Click -= OnClick;
        element.Unloaded -= DetachEvents;

        SetEnable(element, false);
    }

    #endregion
}
