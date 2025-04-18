using Stopify.Presentation.Utilities.Helpers;
using Stopify.Presentation.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Behaviors.Player;

public static class ImageButtonBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty = DependencyProperty.RegisterAttached(
        "Enable",
        typeof(bool),
        typeof(ImageButtonBehavior),
        new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty NowPlayingBorderVisibilityProperty = DependencyProperty.RegisterAttached(
        "NowPlayingBorderVisibility",
        typeof(Visibility),
        typeof(ImageButtonBehavior),
        new PropertyMetadata(Visibility.Collapsed));

    public static readonly DependencyProperty NowPlayingBtnContentProperty = DependencyProperty.RegisterAttached(
        "NowPlayingBtnContent",
        typeof(string),
        typeof(ImageButtonBehavior),
        new PropertyMetadata(null));

    #endregion

    #region Getters/Setters

    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);

    public static Visibility GetNowPlayingBorderVisibility(DependencyObject obj) =>
        (Visibility)obj.GetValue(NowPlayingBorderVisibilityProperty);
    public static void SetNowPlayingBorderVisibility(DependencyObject obj, Visibility value) =>
        obj.SetValue(NowPlayingBorderVisibilityProperty, value);

    public static string GetNowPlayingBtnContent(DependencyObject obj) =>
        (string)obj.GetValue(NowPlayingBtnContentProperty);
    public static void SetNowPlayingBtnContent(DependencyObject obj, string value) =>
        obj.SetValue(NowPlayingBtnContentProperty, value);

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

        MainView mainView = (MainView)Application.Current.MainWindow;

        SetNowPlayingBorderVisibility(element, Visibility.Visible);

        if (mainView.NowPlayingCollapsed == true)
        {
            HoverPopupHelper.PopupText = "Expand";
            SetNowPlayingBtnContent(element, "\uf106");
        }
        else
        {
            HoverPopupHelper.PopupText = "Collapse";
            SetNowPlayingBtnContent(element, "\uf107");
        }

        HoverPopupHelper.DisplayPopupText(element, PlacementMode.Top);
    }

    private static void OnMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not Button element) return;

        SetNowPlayingBorderVisibility(element, Visibility.Collapsed);
        HoverPopupHelper.HidePopup();
    }

    private static void OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        MainView mainView = (MainView)Application.Current.MainWindow;

        if (mainView.NowPlayingCollapsed == true)
        {
            mainView.NowPlayingCollapsed = false;
            HoverPopupHelper.PopupText = "Collapse";
            SetNowPlayingBtnContent(element, "\uf107");
            //ColorAnimations.AnimateForeground(NowPlayingOption, Color.FromRgb(30, 215, 96), .1);
        }
        else
        {
            mainView.NowPlayingCollapsed = true;
            //ColorAnimations.AnimateForeground(NowPlayingOption, Colors.DarkGray, .1);
            HoverPopupHelper.PopupText = "Expand";
            SetNowPlayingBtnContent(element, "\uf106");
        }

        mainView.UpdateNowPlaying();
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
