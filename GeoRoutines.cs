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
using System.Device.Location;

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
            string [] result = MappMon.mySocket.getLocations((App.Current as App).uid,start,end);
            if (!result[0].Equals("error"))
            if (!result[0].Equals("error"))
            {
                int resultLength = result.Length - 1;
                LocationCollection points = new LocationCollection();
                int i = 0;
                double eastSide = -180;
                double westSide = 180;
                double northSide = -90;
                double southSide = 90;
                while (i < resultLength)
                {
                    string [] resString = result[i].Split(new Char [] {'|'});
                    //System.Diagnostics.Debug.WriteLine(resString);
                    Double longi;
                    longi = Double.Parse(resString[0]);
                    if (longi < westSide) westSide = longi +1;
                    if (longi > eastSide) eastSide = longi -1;
                    Double lat;
                    lat = Double.Parse(resString[1]);
                    if (lat > northSide) northSide = lat -1;
                    if (lat < southSide) southSide = lat +1;
                    points.Add(new System.Device.Location.GeoCoordinate(lat, longi));
                    Pushpin pin = new Pushpin();
                    pin.Location = new GeoCoordinate(lat, longi);
                    strokeLayer.Children.Add(pin);
                    i++;
                }
                string id = "Movement from " + start + " to " + end;
                MapPolyline actualLine = new MapPolyline();
                LocationRect view = new LocationRect();
                view.East = eastSide;
                view.West = westSide;
                view.North = northSide;
                view.South = southSide;
                actualLine.Locations = points;
                Color StrokeColor = new Color();
                StrokeColor.B = 10;
                StrokeColor.A = 200;
                StrokeColor.G = 10;
                StrokeColor.R = 250;
                actualLine.Stroke = new SolidColorBrush(StrokeColor);
                actualLine.Opacity = 0.9;
                actualLine.StrokeThickness = 5;
                geoMap.SetView(view);
                geoMap.Children.Add(actualLine);
                geoMap.UpdateLayout();
            }
            else
            {
                //bitch and moan somehow
            }
        }
    }

}