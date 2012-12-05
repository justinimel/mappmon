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
        Map geoMap;
        MapLayer strokeLayer;
        public GeoRoutines(Microsoft.Phone.Controls.Maps.Map usedMap, MapLayer usedLayer)
        {
            geoMap = usedMap;
            strokeLayer = usedLayer;
        }

        public void plotPath(string start, string end)
        {
            string[] result = MappMon.mySocket.getLocations((App.Current as App).uid, start, end);
            if (!result.Equals("error"))
            {
                int resultLength = result.Length;
                LocationCollection points = new LocationCollection();
                int i = 0;
                double eastSide = -180;
                double westSide = 180;
                double northSide = -180;
                double southSide = 180;
                while (i < resultLength)
                {
                    string[] resString = result[i].Split(new Char[] { '|' });
                    double lat = Convert.ToDouble(resString[0]);
                    if (lat < westSide) westSide = lat;
                    if (lat > eastSide) eastSide = lat;
                    double longi = Convert.ToDouble(resString[1]);
                    if (longi > northSide) northSide = longi;
                    if (longi < southSide) southSide = longi;
                    points.Add(new System.Device.Location.GeoCoordinate(lat, longi));
                }
                string id = "Movement from " + start + " to " + end;
                MapPolyline actualLine = new MapPolyline();
                LocationRect view = new LocationRect();
                view.East = eastSide;
                view.West = westSide;
                view.North = northSide;
                view.South = southSide;
                actualLine.Locations = points;
                actualLine.Width = 3;
                Color StrokeColor = new Color();
                StrokeColor.B = 255;
                actualLine.Stroke = new SolidColorBrush(StrokeColor);
                strokeLayer.Children.Add(actualLine);
                geoMap.SetView(view);
            }
            else
            {
                //bitch and moan somehow.
            }
        }
    }

}