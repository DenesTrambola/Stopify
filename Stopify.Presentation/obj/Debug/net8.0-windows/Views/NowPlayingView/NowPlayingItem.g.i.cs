﻿#pragma checksum "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FC3BCE8C3C6F66EC802BC9546BFACE90533C842E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Stopify.Presentation.Views.NowPlayingView;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Stopify.Presentation.Views.NowPlayingView {
    
    
    /// <summary>
    /// NowPlayingItem
    /// </summary>
    public partial class NowPlayingItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ItemBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemBorder;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayBtn;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ArtistBtn;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ArtistText;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OptionsBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Stopify.Presentation;V1.0.0.0;component/views/nowplayingview/nowplayingitem.xaml" +
                    "", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ItemBtn = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ItemBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ItemBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.Click += new System.Windows.RoutedEventHandler(this.ItemBtn_Click);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ItemBtn_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.GotFocus += new System.Windows.RoutedEventHandler(this.ItemBtn_GotFocus);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ItemBtn.LostFocus += new System.Windows.RoutedEventHandler(this.ItemBtn_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ItemBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.PlayBtn = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.PlayBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.PlayBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 59 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.PlayBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.PlayBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 60 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.PlayBtn.Click += new System.Windows.RoutedEventHandler(this.PlayBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ArtistBtn = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ArtistBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ArtistBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 88 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ArtistBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ArtistBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 89 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.ArtistBtn.Click += new System.Windows.RoutedEventHandler(this.ArtistBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ArtistText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.OptionsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 116 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.OptionsBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OptionsBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 117 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.OptionsBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OptionsBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 118 "..\..\..\..\..\Views\NowPlayingView\NowPlayingItem.xaml"
            this.OptionsBtn.Click += new System.Windows.RoutedEventHandler(this.OptionsBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

