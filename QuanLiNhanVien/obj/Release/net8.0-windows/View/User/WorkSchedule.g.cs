﻿#pragma checksum "..\..\..\..\..\View\User\WorkSchedule.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1AFC0AC632E959E83B3A7C07DFD24C0D7A9377F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using QuanLiNhanVien.View.User;
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


namespace QuanLiNhanVien.View.User {
    
    
    /// <summary>
    /// WorkSchedule
    /// </summary>
    public partial class WorkSchedule : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ScheduleGrid;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day1;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day2;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day3;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day4;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day5;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day6;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\View\User\WorkSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Day7;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/QuanLiNhanVien;component/view/user/workschedule.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\User\WorkSchedule.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ScheduleGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Day1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Day2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Day3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Day4 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Day5 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.Day6 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.Day7 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 96 "..\..\..\..\..\View\User\WorkSchedule.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PreviousWeek_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 105 "..\..\..\..\..\View\User\WorkSchedule.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextWeek_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

