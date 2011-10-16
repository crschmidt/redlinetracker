using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections;
using System.Globalization;

namespace RedLineTracker
{

    public partial class Page1 : PhoneApplicationPage
    {
        public Dictionary<string,Route> northRoutes;
        public Dictionary<string,Route> southRoutes;
        public string direction;
        public Page1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(RouteSelect_Loaded);

         }
        void RouteSelect_Loaded(object sender, RoutedEventArgs e)
        {
            var routes = new Routes();
            

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Routes routes = new Routes();
            if (NavigationContext.QueryString.TryGetValue("direction", out direction))
            {
                if (direction == "N")
                {
                    listBoxTarget.ItemsSource = routes.northRoutes.Values;
                    PageTitle.Text = "Northbound";
                } else if (direction == "S") {
                    listBoxTarget.ItemsSource = routes.southRoutes.Values;
                    PageTitle.Text = "Southbound";
                } 
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void SwapIcon_Click(object sender, EventArgs e)
        {
            string newdirection = null;
            if (direction == "N")
            {
                newdirection = "S";    
            }
            else if (direction == "S")
            {
                newdirection = "N";    
            }

            if (newdirection != null)
            {
                NavigationService.Navigate(new Uri("/RouteSelect.xaml?direction=" + newdirection, UriKind.Relative));
            }
        }

    }
    
    public class Route
    {
        public String key { get; set; }
        public String title { get; set; }
        public String direction { get; set; }
        public String url { get; set; }
    }
    public class Routes
    {
        public Dictionary<string, Route> northRoutes;
        public Dictionary<string, Route> southRoutes;
        public Routes()
        {
            northRoutes = new Dictionary<string, Route>();
            southRoutes = new Dictionary<string, Route>();
            northRoutes.Add("RALEN", new Route { key = "RALEN", url = "/Stop.xaml?key=RALEN", direction = "N", title = "Alewife Station" });
            northRoutes.Add("RDAVN", new Route { key = "RDAVN", url = "/Stop.xaml?key=RDAVN", direction = "N", title = "Davis Station" });
            southRoutes.Add("RDAVS", new Route { key = "RDAVS", url = "/Stop.xaml?key=RDAVS", direction = "S", title = "Davis Station" });
            northRoutes.Add("RPORN", new Route { key = "RPORN", url = "/Stop.xaml?key=RPORN", direction = "N", title = "Porter Square Station" });
            southRoutes.Add("RPORS", new Route { key = "RPORS", url = "/Stop.xaml?key=RPORS", direction = "S", title = "Porter Square Station" });
            northRoutes.Add("RHARN", new Route { key = "RHARN", url = "/Stop.xaml?key=RHARN", direction = "N", title = "Harvard Square Station" });
            southRoutes.Add("RHARS", new Route { key = "RHARS", url = "/Stop.xaml?key=RHARS", direction = "S", title = "Harvard Square Station" });
            northRoutes.Add("RCENN", new Route { key = "RCENN", url = "/Stop.xaml?key=RCENN", direction = "N", title = "Central Square Station" });
            southRoutes.Add("RCENS", new Route { key = "RCENS", url = "/Stop.xaml?key=RCENS", direction = "S", title = "Central Square Station" });
            northRoutes.Add("RKENN", new Route { key = "RKENN", url = "/Stop.xaml?key=RKENN", direction = "N", title = "Kendall/MIT Station" });
            southRoutes.Add("RKENS", new Route { key = "RKENS", url = "/Stop.xaml?key=RKENS", direction = "S", title = "Kendall/MIT Station" });
            northRoutes.Add("RMGHN", new Route { key = "RMGHN", url = "/Stop.xaml?key=RMGHN", direction = "N", title = "Charles/MGH Station" });
            southRoutes.Add("RMGHS", new Route { key = "RMGHS", url = "/Stop.xaml?key=RMGHS", direction = "S", title = "Charles/MGH Station" });
            northRoutes.Add("RPRKN", new Route { key = "RPRKN", url = "/Stop.xaml?key=RPRKN", direction = "N", title = "Park St. Station" });
            southRoutes.Add("RPRKS", new Route { key = "RPRKS", url = "/Stop.xaml?key=RPRKS", direction = "S", title = "Park St. Station" });
            northRoutes.Add("RDTCN", new Route { key = "RDTCN", url = "/Stop.xaml?key=RDTCN", direction = "N", title = "Downtown Crossing Station" });
            southRoutes.Add("RDTCS", new Route { key = "RDTCS", url = "/Stop.xaml?key=RDTCS", direction = "S", title = "Downtown Crossing Station" });
            northRoutes.Add("RSOUN", new Route { key = "RSOUN", url = "/Stop.xaml?key=RSOUN", direction = "N", title = "South Station" });
            southRoutes.Add("RSOUS", new Route { key = "RSOUS", url = "/Stop.xaml?key=RSOUS", direction = "S", title = "South Station" });
            northRoutes.Add("RBRON", new Route { key = "RBRON", url = "/Stop.xaml?key=RBRON", direction = "N", title = "Broadway Station" });
            southRoutes.Add("RBROS", new Route { key = "RBROS", url = "/Stop.xaml?key=RBROS", direction = "S", title = "Broadway Station" });
            northRoutes.Add("RANDN", new Route { key = "RANDN", url = "/Stop.xaml?key=RANDN", direction = "N", title = "Andrew Station" });
            southRoutes.Add("RANDS", new Route { key = "RANDS", url = "/Stop.xaml?key=RANDS", direction = "S", title = "Andrew Station" });
            northRoutes.Add("RJFKN", new Route { key = "RJFKN", url = "/Stop.xaml?key=RJFKN", direction = "N", title = "JFK/UMass Station" });
            southRoutes.Add("RJFKS", new Route { key = "RJFKS", url = "/Stop.xaml?key=RJFKS", direction = "S", title = "JFK/UMass Station" });
            northRoutes.Add("RSAVN", new Route { key = "RSAVN", url = "/Stop.xaml?key=RSAVN", direction = "N", title = "Savin Hill Station" });
            southRoutes.Add("RSAVS", new Route { key = "RSAVS", url = "/Stop.xaml?key=RSAVS", direction = "S", title = "Savin Hill Station" });
            northRoutes.Add("RFIEN", new Route { key = "RFIEN", url = "/Stop.xaml?key=RFIEN", direction = "N", title = "Fields Corner Station" });
            southRoutes.Add("RFIES", new Route { key = "RFIES", url = "/Stop.xaml?key=RFIES", direction = "S", title = "Fields Corner Station" });
            northRoutes.Add("RSHAN", new Route { key = "RSHAN", url = "/Stop.xaml?key=RSHAN", direction = "N", title = "Shawmut Station" });
            southRoutes.Add("RSHAS", new Route { key = "RSHAS", url = "/Stop.xaml?key=RSHAS", direction = "S", title = "Shawmut Station" });
            southRoutes.Add("RASHS", new Route { key = "RASHS", url = "/Stop.xaml?key=RASHS", direction = "S", title = "Ashmont Station" });
            northRoutes.Add("RNQUN", new Route { key = "RNQUN", url = "/Stop.xaml?key=RNQUN", direction = "N", title = "North Quincy Station" });
            southRoutes.Add("RNQUS", new Route { key = "RNQUS", url = "/Stop.xaml?key=RNQUS", direction = "S", title = "North Quincy Station" });
            northRoutes.Add("RWOLN", new Route { key = "RWOLN", url = "/Stop.xaml?key=RWOLN", direction = "N", title = "Wollaston Station" });
            southRoutes.Add("RWOLS", new Route { key = "RWOLS", url = "/Stop.xaml?key=RWOLS", direction = "S", title = "Wollaston Station" });
            northRoutes.Add("RQUCN", new Route { key = "RQUCN", url = "/Stop.xaml?key=RQUCN", direction = "N", title = "Quincy Center Station" });
            southRoutes.Add("RQUCS", new Route { key = "RQUCS", url = "/Stop.xaml?key=RQUCS", direction = "S", title = "Quincy Center Station" });
            northRoutes.Add("RQUAN", new Route { key = "RQUAN", url = "/Stop.xaml?key=RQUAN", direction = "N", title = "Quincy Adams Station" });
            southRoutes.Add("RQUAS", new Route { key = "RQUAS", url = "/Stop.xaml?key=RQUAS", direction = "S", title = "Quincy Adams Station" });
            southRoutes.Add("RBRAS", new Route { key = "RBRAS", url = "/Stop.xaml?key=RBRAS", direction = "S", title = "Braintree Station" });

        }
        
    }
}