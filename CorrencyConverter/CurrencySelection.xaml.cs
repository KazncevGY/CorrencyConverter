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
    public sealed partial class BlankPage1 : Page
    {
        string selection;

        public BlankPage1()
        {
            this.InitializeComponent();
            CurrencySelection.ItemsSource = App.currency.Values;
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
            foreach (Currency c in CurrencySelection.Items)
            {
                if (c == selected)
                {
                    c.isSelected = true;
                    //CurrencySelection.SelectedItem = CurrencySelection.ContainerFromItem(c);
                }
                else
                {
                    c.isSelected = false;
                }
            }
            
        }

        private void CurrencySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
