using System;
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
        }

        public void getCurrentLocation_button(object sender, RoutedEventArgs e)
        {
            cw.getCoordinates();
        }

        //add some function for my GeoRoutines when you or I have that figured out.
    }
}