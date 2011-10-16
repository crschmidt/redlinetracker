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

namespace RedLineTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Southbound_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RouteSelect.xaml?direction=S", UriKind.Relative));
        }

        private void Northbound_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RouteSelect.xaml?direction=N", UriKind.Relative));
        }
    }
}