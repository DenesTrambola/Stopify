using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Stopify.Presentation.Utilities.Behaviors.Sidebar.SidebarItem;

public static class SidebarItemTogglePlayOnClicksBehavior
{
    #region Dependency Properties

    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(SidebarItemTogglePlayOnClicksBehavior),
            new PropertyMetadata(false, OnEnableChanged));

    public static readonly DependencyProperty IsPlayingProperty =
        DependencyProperty.RegisterAttached(
            "IsPlaying",
            typeof(bool),
            typeof(SidebarItemTogglePlayOnClicksBehavior),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region Getters/Setters

    public static bool GetEnable(DependencyObject obj) =>
        (bool)obj.GetValue(EnableProperty);
    public static void SetEnable(DependencyObject obj, bool value) =>
        obj.SetValue(EnableProperty, value);

    public static bool GetIsPlaying(DependencyObject obj) =>
        (bool)obj.GetValue(IsPlayingProperty);
    public static void SetIsPlaying(DependencyObject obj, bool value) =>
        obj.SetValue(IsPlayingProperty, value);

    #endregion

    #region Property Callbacks

    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button element) return;

        if ((bool)e.NewValue)
        {
            if (element.Name.Equals("ItemImgBtn"))
                element.Click += OnClick;
            else if (element.Name.Equals("ItemBtn"))
                element.MouseDoubleClick += OnDoubleClick;

            element.Unloaded += DetachEvents;
        }
        else
        {
            if (element.Name.Equals("ItemImgBtn"))
                element.Click -= OnClick;
            else if (element.Name.Equals("ItemBtn"))
                element.MouseDoubleClick -= OnDoubleClick;

            element.Unloaded -= DetachEvents;
        }
    }

    #endregion

    #region Event Handlers

    private static void OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        TogglePlay(element);
    }

    private static void OnDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Button element) return;

        TogglePlay(element);
    }

    private static void DetachEvents(object sender, RoutedEventArgs e)
    {
        if (sender is not Button element) return;

        if (element.Name.Equals("ItemImgBtn"))
            element.Click -= OnClick;
        else if (element.Name.Equals("ItemBtn"))
            element.MouseDoubleClick -= OnDoubleClick;

        element.Unloaded -= DetachEvents;

        SetEnable(element, false);
    }

    #endregion

    #region Methods

    private static void TogglePlay(Button element)
    {
        bool isPlaying = GetIsPlaying(element);
        SetIsPlaying(element, !isPlaying);
    }

    #endregion
}
