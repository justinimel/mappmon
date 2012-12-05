﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace mappmon
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        private CoordinateWatcher cw;
        private GeoRoutines gr;
        public PivotPage1()
        {
            InitializeComponent();
            cw = new CoordinateWatcher();
            gr = new GeoRoutines(map1, lineLayer);

            gr.plotPath("2012-12-04 00:00:00", "2012-12-05 00:00:00");
            //getLocations(uid, "2012-12-04 00:00:00", "2012-12-05 00:00:00");

            cw.getCoordinates();
        }

        public void getCurrentLocation_button(object sender, RoutedEventArgs e)
        {
            if (cw.getCoordinates())
            {
                MessageBox.Show("Get location successful!");
            }
            else
            {
                MessageBox.Show("Get location failed!");
            }
        }

        private void twodaysbut_Checked(object sender, RoutedEventArgs e)
        {
            (App.Current as App).day = 2;
        }

        private void yesterdaybut_Checked(object sender, RoutedEventArgs e)
        {
            (App.Current as App).day = 1;
        }

        private void todaybut_Checked(object sender, RoutedEventArgs e)
        {
            (App.Current as App).day = 0;
        }

        private void fivesecondsbut_Checked(object sender, RoutedEventArgs e)
        {
            cw.time = 5000;
        }

        private void thirtysecsbut_Checked(object sender, RoutedEventArgs e)
        {
            if (cw != null)
                cw.time = 30000;
        }

        private void oneminbut_Checked(object sender, RoutedEventArgs e)
        {
            cw.time = 60000;
        }

        private void twominbut_Checked(object sender, RoutedEventArgs e)
        {
            cw.time = 120000;
        }

        //add some function for my GeoRoutines when you or I have that figured out.
    }
}