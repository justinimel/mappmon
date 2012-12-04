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
using Microsoft.Phone.Controls.Maps;

namespace mappmon
{

    public class GeoRoutines
    {
        public GeoRoutines()
        {
        }

        public void GetMap()
        {

        }

        public void plotPath(string start, string end)
        {
            string[] result = MappMon.mySocket.getLocations((App.Current as App).uid, start, end);
            if (!result.Equals("error"))
            {
                int resultLength = result.Length;
                LocationCollection points = new LocationCollection();
                int i = 0;
                while (i < resultLength)
                {
                    string[] resString = result[i].Split(new Char[] { '|' });
                    double lat = Convert.ToDouble(resString[0]);
                    double longi = Convert.ToDouble(resString[1]);
                    points.Add(new System.Device.Location.GeoCoordinate(lat, longi));
                }
                string id = "Movement from " + start + " to " + end;
                MapPolyline actualLine = new MapPolyline();
                actualLine.Locations = points;
            }
            else
            {
                //bitch and moan somehow.
            }
        }
    }

}