using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CorrencyConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadPage : Page
    {
        string path;
        public LoadPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            path = e.Parameter.ToString();
            while(!await loadAttempt())
            {
                ContentDialog failure = new ContentDialog()
                {
                    Title = "Проблема с загрузкой валют.",
                    Content = "Попытка загрузки валют закончилась ошибкой. Повторить попытку?",
                    PrimaryButtonText = "Да",
                    SecondaryButtonText ="Нет"
                };
                ContentDialogResult res= await failure.ShowAsync();
                if (res == ContentDialogResult.Secondary)
                {
                    App.Current.Exit();
                }
            }
            Frame.Navigate(typeof(ConvertPage),"");
            
        }

        private async Task<bool> loadAttempt()
        {
            try
            {
                await Task.Run(loadCurrency);

            }
            catch (WebException e)
            {
                ContentDialog failure = new ContentDialog()
                {
                    Title = "Проблема с загрузкой валют.",
                    Content = e.Message,
                    CloseButtonText = "Ок."
                };
                await failure.ShowAsync();
                return false;
            }
            return true;
        }

        private async Task loadCurrency()
        {
            WebClient client = new WebClient();
            Task<string> buffer = client.DownloadStringTaskAsync(path);
            await buffer;
            if (buffer.IsCompleted)
            {
                JObject daily = JObject.Parse(buffer.Result);
                foreach (JProperty curr in daily["Valute"].Children())
                {
                    string n = curr.Name;
                    App.currency.Add(n, new Currency((string)daily["Valute"][n]["Name"], n, (decimal)daily["Valute"][n]["Value"]));
                }
                
            }
            return;
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialog confirm = new ContentDialog()
            {
                Content = "Загрузка валюты в процессе. Вы хотите прервать процесс загрузы и выключить приложение?",
                PrimaryButtonText = "Да",
                SecondaryButtonText = "Нет"
            };
            ContentDialogResult answer= await confirm.ShowAsync();
            if (answer == ContentDialogResult.Primary)
            {
                App.Current.Exit();
            }
        }
    }
}
