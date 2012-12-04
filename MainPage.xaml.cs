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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

     

            int result = MappMon.mySocket.login(emailTB.Text, passwordTB.Text);

            if (result != -1)
            {
                NavigationService.Navigate(new Uri("/mappmon;component/Tabs.xaml",UriKind.Relative));
            }
        }
    }
}