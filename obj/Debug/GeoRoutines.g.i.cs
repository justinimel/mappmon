﻿#pragma checksum "C:\Users\Josh\Documents\Visual Studio 2010\Projects\PhoneApp3\PhoneApp3\GeoRoutines.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C81B6731AC1CB40DAB227DE4BDA119D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Maps.MapControl;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;




public partial class GeoRoutines : System.Windows.Controls.UserControl {
    
    internal System.Windows.Controls.Grid LayoutRoot;
    
    internal Microsoft.Maps.MapControl.Map MyMap;
    
    internal Microsoft.Maps.MapControl.MapLayer MyLayer;
    
    private bool _contentLoaded;
    
    /// <summary>
    /// InitializeComponent
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public void InitializeComponent() {
        if (_contentLoaded) {
            return;
        }
        _contentLoaded = true;
        System.Windows.Application.LoadComponent(this, new System.Uri("/mappmon;component/GeoRoutines.xaml", System.UriKind.Relative));
        this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
        this.MyMap = ((Microsoft.Maps.MapControl.Map)(this.FindName("MyMap")));
        this.MyLayer = ((Microsoft.Maps.MapControl.MapLayer)(this.FindName("MyLayer")));
    }
}

