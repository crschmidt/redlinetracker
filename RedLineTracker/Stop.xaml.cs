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
using Microsoft.Phone.Shell;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;


namespace RedLineTracker
{
    public partial class Page2 : PhoneApplicationPage
    {
        Routes routes;
        Boolean loadComplete;
        string key;
        Route currentRoute;
        public Page2()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page2_Loaded);
        }

        void loadRoutes()
        {
            if (!loadComplete)
            {
                routes = new Routes();
                loadComplete = true;
                getResults("http://developer.mbta.com/Data/Red.json");
            }
        }
        void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            loadRoutes();
        }

        public void getResults(string websiteURL)
        {
            PredictionsStatus.Text = "Loading...";
            WebClient c = new WebClient();
            Random random = new Random();
            websiteURL += "?random=" + random.Next().ToString();
            c.DownloadStringAsync(new Uri(websiteURL));
            c.DownloadStringCompleted += new DownloadStringCompletedEventHandler(c_DownloadStringCompleted);
        }

        void c_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            lock (this)
            {
                try
                {
                    string s = e.Result;

                    DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(typeof(Predictions));
                    System.IO.MemoryStream ms = new
                      System.IO.MemoryStream(Encoding.UTF8.GetBytes(s));
                    Predictions predictions = (Predictions)serializer.ReadObject(ms);
                    predictions.retrieved = DateTime.Now;
                    Predictions keyPredictions = predictions.getPredictions(key);
                    if (keyPredictions.Count > 0)
                    {
                        predictionsTarget.ItemsSource = keyPredictions;
                        if (keyPredictions.getSoonest() != null)
                        {
                            Soonest.Text = keyPredictions.getSoonest().delta;
                        };
                        var offset = new TimeSpan(predictions[0].offset(predictions.retrieved)).TotalSeconds;
                        string text = predictions.getPredictions(key).Count + " upcoming train";
                        if (keyPredictions.Count > 1)
                        {
                            text += "s";
                        }
                        PredictionsStatus.Text = text;
                    }
                    else
                    {
                        PredictionsStatus.Text = "No current predictions.";
                    }
                }
                catch (Exception)
                {
                    PredictionsStatus.Text = "Failed to load predictions";
                }
                }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            loadRoutes();

            if (NavigationContext.QueryString.TryGetValue("key", out key))
            {
                Boolean found = false;
                Route r = null;
                if (routes.northRoutes.ContainsKey(key))
                {
                    r = routes.northRoutes[key];
                    found = true;
                }
                else if (routes.southRoutes.ContainsKey(key))
                {
                    r = routes.southRoutes[key];
                    found = true;
                }
                if (found)
                {
                    PageTitle.Text = r.title;
                    Direction.Text = r.direction == "N" ? "Northbound" : "Southbound";
                    currentRoute = r;
                }
            }
            else
            {
                PageTitle.Text = "no key";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getResults("http://developer.mbta.com/Data/Red.json");
        }

        private void SwapIcon_Click(object sender, EventArgs e)
        {
            string station = key.Substring(0, key.Length - 1);
            string newstation = null;
            char direction = key[4];
            if (direction == 'N')
            {
                newstation = station + "S";
            }
            else if (direction == 'S')
            {
                newstation = station + "N";
            }
            if (newstation != null && (routes.northRoutes.ContainsKey(newstation) ||routes.southRoutes.ContainsKey(newstation)))
            {
                NavigationService.Navigate(new Uri("/Stop.xaml?key=" + newstation, UriKind.Relative));
            }
        }

        private void PinIcon_Click(object sender, EventArgs e)
        {
            StandardTileData NewTileData = new StandardTileData
            {
                Title = currentRoute.direction + "." + currentRoute.title.Replace(" Station", ""),
                BackgroundImage = new Uri("MBTA.png", UriKind.Relative),
            };
            ShellTile.Create(
                new Uri(
                    "/Stop.xaml?key="+currentRoute.key,
                    UriKind.Relative),
                NewTileData);
        }
    }
    public class Prediction
    {
        public string Line { get; set; }
        public int Trip { get; set; }
        public string PlatformKey { get; set; }
        public string InformationType { get; set; }
        public string Time { get; set; }
        public string TimeRemaining { get; set; }
        public string Revenue { get; set; }
        public string Route { get; set; }
        public DateTime Timestamp { get {
            DateTime dx = DateTime.Parse(Time);
            return dx;
        } 
        }
        public long offset (DateTime orig) 
        {
                DateTime dx = Timestamp;
                var pieces = TimeRemaining.Split(':');
                Boolean negative = false;
                if (pieces[0][0] == '-')
                {
                    negative = true;
                }
                int hours = Math.Abs(Convert.ToInt16(pieces[0]));
                int minutes = Math.Abs(Convert.ToInt16(pieces[1]));
                int seconds = Math.Abs(Convert.ToInt16(pieces[2]));

                TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                if (negative)
                {
                    dx = dx.Add(ts);
                }
                else
                {
                    dx = dx.Subtract(ts);
                }
                
                return dx.Ticks - orig.Ticks;
            
        }
        public string status { get {
            var timestring = Timestamp.ToString();
            var ts = new TimeSpan(Timestamp.Ticks - DateTime.Now.Ticks);
            string delta = (int)Math.Abs(ts.TotalMinutes) + ":" + Math.Abs(ts.Seconds).ToString("00");
            if (ts.TotalMinutes < 0)
            {
                delta = "-" + delta;
            }
            return InformationType + " in " + delta + "; " + timestring ;
        } }
        public string delta
        {
            get
            {
                var ts = new TimeSpan(Timestamp.Ticks - DateTime.Now.Ticks);
                var text = "Next train: ";
                if (InformationType == "Arrived")
                {
                    text += "Arriving";
                }
                else
                {
                    text += (int)ts.TotalMinutes + " minutes";
                }
                return text;
            }
        }
    }
    public class Predictions : List<Prediction>
    {
        public DateTime retrieved { get; set; }
        public Predictions getPredictions(string key) {
            Predictions retp = new Predictions();
            for (int i = 0; i < this.Count; i++) {
                Prediction p = this[i];
                if (p.PlatformKey == key)
                {
                    retp.Add(p);
                }
            }
            retp.Sort(delegate(Prediction p1, Prediction p2) { return p1.Timestamp.CompareTo(p2.Timestamp); });
            return retp;
        }
        public Prediction getSoonest()
        {
            Prediction min = null;
            for (var i = 0; i < this.Count; i++)
            {
                if (min == null)
                {
                    min = this[i];
                }
                if (this[i].Timestamp < min.Timestamp)
                {
                    min = this[i];
                }
            }
            return min;
        }
        
    }
}