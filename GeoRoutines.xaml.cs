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
using Microsoft.Maps.MapControl;

namespace mappmon
{

    public class GeoRoutines
    {
        public GeoRoutines()
        {
            InitializeComponent();
        }

        public void GetMap()
        {

        }

        public void plotPath(string start, string end)
        {
            string [] result = mySocket.getLocations((App.Current as App).uid, start, end);
            if (!result.Equals("error"))
            {
                int resultLength = result.Length();
                VELatLong points = new VELatLong[resultLength];
                int i = 0;
                while (i < resultLength)
                {
                    string[] resString = result[i].Split(new Char [] {'|'});
                    int lat = ToInt32(resString[0]);
                    int longi = ToInt32(resString[1]);
                    points[i] = new VELatLong[lat, longi];
                }
                VEColor color = new VEColor(0, 0, 255, 1);
                int width = 5;
                string id = "Movement from " + start + " to " + end;
                VEPolyline actualLine = new VEPolyline(id, points, color, width);
                map.AddPolyline(actualLine);
                map.SetMapView(points);
            }
            else
            {
                //bitch and moan somehow.
            }
        }
    }

}