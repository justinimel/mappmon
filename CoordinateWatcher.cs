using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using System.ComponentModel;
using System.Threading;

namespace mappmon
{
    public class CoordinateWatcher
    {
        public int time = 30000;
        private GeoCoordinateWatcher watcher;

        public CoordinateWatcher()
        {
            this.watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.Start();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            //Thread.Sleep(4000);
            //worker.RunWorkerAsync();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (watcher.Status == GeoPositionStatus.Ready)
                    getCoordinates();
                Thread.Sleep(time);
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show("mappmon has encountered an error. Please restart the app to continue tracking your location.");
        }

        public void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Disabled)
                MessageBox.Show("location service is disabled.");
            else if (e.Status == GeoPositionStatus.NoData)
                MessageBox.Show("No location data can be found right now.");
            //else if (e.Status == GeoPositionStatus.Initializing)
              //  MessageBox.Show("Attempting to retrieve location data.");
        }

        public bool getCoordinates()
        {
            bool success = MappMon.mySocket.addLocation((App.Current as App).uid, watcher.Position.Location.Longitude, watcher.Position.Location.Latitude, watcher.Position.Location.HorizontalAccuracy);

            return success;
        }

    }
}
