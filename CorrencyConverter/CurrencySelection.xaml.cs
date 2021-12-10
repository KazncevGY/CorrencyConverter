using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CurrencySelection : Page
    {
        string selection;

        public CurrencySelection()
        {
            this.InitializeComponent();
            CurrencyList.ItemsSource = App.currency.Values;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Currency selected;
            selection = e.Parameter.ToString();
            if (e.Parameter.ToString().Equals("left"))
            {
                selected = App.leftCurrency;
            } else
            {
                selected = App.rightCurrency;
            }
            foreach (Currency c in CurrencyList.Items)
            {
                if (c == selected)
                {
                    c.isSelected = true;
                }
                else
                {
                    c.isSelected = false;
                }
            }
            
        }

        private void CurrencyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selection.Equals("left"))
            {
                App.leftCurrency = (Currency)e.AddedItems.First();
            }
            else
            {
                App.rightCurrency = (Currency)e.AddedItems.First();
            }
            Frame.Navigate(typeof(ConvertPage), selection);
        }
    }
}
