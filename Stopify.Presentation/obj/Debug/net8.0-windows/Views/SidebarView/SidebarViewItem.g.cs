﻿#pragma checksum "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "179FC79D7315975E091EEBFAA637A23A979A61BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Stopify.Presentation.Views.SidebarView;
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


namespace Stopify.Presentation.Views.SidebarView {
    
    
    /// <summary>
    /// SidebarViewItem
    /// </summary>
    public partial class SidebarViewItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ItemBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemBorder;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ItemImgBtn;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayBtn;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SidebarPlayingIcon;
        
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
            System.Uri resourceLocater = new System.Uri("/Stopify.Presentation;component/views/sidebarview/sidebarviewitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
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
            
            #line 16 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ItemBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ItemBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemBtn.Click += new System.Windows.RoutedEventHandler(this.ItemBtn_Click);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemBtn.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.PlayPauseEvent);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ItemBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.ItemImgBtn = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemImgBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ItemImgBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 44 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemImgBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ItemImgBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 45 "..\..\..\..\..\Views\SidebarView\SidebarViewItem.xaml"
            this.ItemImgBtn.Click += new System.Windows.RoutedEventHandler(this.PlayPauseEvent);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PlayBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.SidebarPlayingIcon = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

