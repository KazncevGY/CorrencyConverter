using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CorrencyConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Hashtable currency;

        public MainPage()
        {
            currency = new Hashtable();
            JObject rub = new JObject();
            loadCurrency("https://www.cbr-xml-daily.ru/daily_json.js");
            currency.Add("RUB", new Currency("Российский рубль", 1));
            this.InitializeComponent();

            

        }

        private void loadCurrency(string path)
        {
            WebClient client = new WebClient();
            string buffer = client.DownloadString(path);
            JObject daily = JObject.Parse(buffer);
            foreach (JProperty curr in daily["Valute"].Children())
            {
                string n = curr.Name;
                currency.Add(n, new Currency((string)daily["Valute"][n]["Name"], (float)daily["Valute"][n]["Value"]));
            }
        }

        private void ChangeFirst_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void ChangeSecond_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
