﻿#pragma checksum "..\..\StartWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1CE6C917AD73C3014FFCFFB16832445C5F2AD6A39244C4D8BDDFE7455047CBE2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Gießformkonfigurator.WPF;
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


namespace Gießformkonfigurator.WPF {
    
    
    /// <summary>
    /// StartWindow
    /// </summary>
    public partial class StartWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Server_TextBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DB_TextBox;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox User_TextBox;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password_TextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Gießformkonfigurator.WPF;component/startwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StartWindow.xaml"
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
            this.Server_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\StartWindow.xaml"
            this.Server_TextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ConnectionDataChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DB_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\StartWindow.xaml"
            this.DB_TextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ConnectionDataChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.User_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 70 "..\..\StartWindow.xaml"
            this.User_TextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ConnectionDataChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Password_TextBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            
            #line 84 "..\..\StartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnOpenModal_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

