﻿#pragma checksum "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FF99901CA2533AC1E4981BE0B6AA43157B8CFC2251F15464EB6C3267F343FFFC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Gießformkonfigurator.WPF.MVVM.View;
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


namespace Gießformkonfigurator.WPF.MVVM.View {
    
    
    /// <summary>
    /// Settings_ApplicationSettingsView
    /// </summary>
    public partial class Settings_ApplicationSettingsView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CurrentLogFilePath;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogFilePathSelector;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AdminPassword;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AdminPasswordSetter;
        
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
            System.Uri resourceLocater = new System.Uri("/Gießformkonfigurator.WPF;component/mvvm/view/settings_applicationsettingsview.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
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
            this.CurrentLogFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.LogFilePathSelector = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\MVVM\View\Settings_ApplicationSettingsView.xaml"
            this.LogFilePathSelector.Click += new System.Windows.RoutedEventHandler(this.LogFilePathSelector_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AdminPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.AdminPasswordSetter = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

