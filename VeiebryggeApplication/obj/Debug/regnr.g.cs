﻿#pragma checksum "..\..\regnr.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "578110415E9671EDD08509B245ADD9D4DAB2D54A722CDC25AEC92AB1328FD9EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using VeiebryggeApplication;


namespace VeiebryggeApplication {
    
    
    /// <summary>
    /// regnr
    /// </summary>
    public partial class regnr : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\regnr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grdVehicles;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\regnr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox regText;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\regnr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameText;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\regnr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox wheelsText;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\regnr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox yearText;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/VeiebryggeApplication;component/regnr.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\regnr.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grdVehicles = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.regText = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\regnr.xaml"
            this.regText.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.regText_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 38 "..\..\regnr.xaml"
            this.regText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.regText_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.nameText = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\regnr.xaml"
            this.nameText.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.nameText_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.wheelsText = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\regnr.xaml"
            this.wheelsText.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.wheelsText_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.yearText = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\regnr.xaml"
            this.yearText.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.yearText_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 46 "..\..\regnr.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Search_Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 47 "..\..\regnr.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Insert_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

