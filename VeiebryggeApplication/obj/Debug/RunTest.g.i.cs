﻿#pragma checksum "..\..\RunTest.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7AC7B297980789421EAC75385D3B38F27CD6CE3B51402924BE15C20183B83204"
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
    /// RunTest
    /// </summary>
    public partial class RunTest : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox regNrText;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock outputText;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox equipmentText;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DiagnosticsButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RunButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\RunTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame PopUp;
        
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
            System.Uri resourceLocater = new System.Uri("/VeiebryggeApplication;component/runtest.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RunTest.xaml"
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
            this.regNrText = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\RunTest.xaml"
            this.regNrText.LostFocus += new System.Windows.RoutedEventHandler(this.regNr_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.outputText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.equipmentText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.DiagnosticsButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\RunTest.xaml"
            this.DiagnosticsButton.Click += new System.Windows.RoutedEventHandler(this.Diagnostics_Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RunButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\RunTest.xaml"
            this.RunButton.Click += new System.Windows.RoutedEventHandler(this.Run_Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PopUp = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

