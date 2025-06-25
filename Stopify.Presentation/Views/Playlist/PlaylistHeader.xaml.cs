﻿using Stopify.Presentation.Utilities.Behaviors.Playlist.PlaylistItem;
using Stopify.Presentation.ViewModels.Playlist;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Stopify.Presentation.Views.Playlist;

public partial class PlaylistHeader : UserControl
{
    public PlaylistHeader()
    {
        InitializeComponent();

        DataContext = new PlaylistHeaderViewModel();

        Binding dateBtnWidthBinding = new()
        {
            Source = DateBtn,
            Path = new PropertyPath("Width")
        };
        BindingOperations.SetBinding(this, PlaylistItemSizeChangeBehavior.DateBtnWidthProperty, dateBtnWidthBinding);

        Binding dateColumnWidthBinding = new()
        {
            Source = DateColumn,
            Path = new PropertyPath("Width")
        };
        BindingOperations.SetBinding(this, PlaylistItemSizeChangeBehavior.DateColumnWidthProperty, dateColumnWidthBinding);

        Binding albumBtnWidthBinding = new()
        {
            Source = AlbumBtn,
            Path = new PropertyPath("Width")
        };
        BindingOperations.SetBinding(this, PlaylistItemSizeChangeBehavior.AlbumBtnWidthProperty, albumBtnWidthBinding);

        Binding albumColumnWidthBinding = new()
        {
            Source = AlbumColumn,
            Path = new PropertyPath("Width")
        };
        BindingOperations.SetBinding(this, PlaylistItemSizeChangeBehavior.AlbumColumnWidthProperty, albumColumnWidthBinding);
    }
}
