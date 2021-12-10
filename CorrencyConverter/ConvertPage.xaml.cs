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
    public sealed partial class ConvertPage : Page
    {
        bool internalTextChange;
        public ConvertPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            internalTextChange = false;
            LeftCurrency.Text = App.leftCurrency.code;
            RightCurrency.Text = App.rightCurrency.code;
            if (e.Parameter.ToString().Equals("right"))
            {
                LeftAmount.Text = App.leftAmount.ToString();
                LeftToRightConvertion();
            }
            if (e.Parameter.ToString().Equals("left"))
            {
                RightAmount.Text = App.rightAmount.ToString();
                RightToLeftConvertion();
            }
        }

        

        private void ChangeLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1), "left");
        }

        private void ChangeRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1), "right");
        }

        private void LeftAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (internalTextChange)
            {
                internalTextChange = false;
            }
            else
            {
                internalTextChange = true;
                LeftToRightConvertion();
            }
        }
        private void RightAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (internalTextChange)
            {
                internalTextChange = false;
            }
            else
            {
                internalTextChange = true;
                RightToLeftConvertion();
            }
        }


        private void LeftToRightConvertion()
        {
            try
            {
                App.leftAmount = Decimal.Round(System.Convert.ToDecimal(LeftAmount.Text),2);
                App.rightAmount = Decimal.Round(App.leftCurrency.convert(App.leftAmount, App.rightCurrency),2);
                RightAmount.Text = App.rightAmount.ToString();
            }
            catch (FormatException fe)
            {
                RightAmount.Text = "0";
            }
        }


        private void RightToLeftConvertion()
        {
            try
            {
                App.rightAmount = Decimal.Round(System.Convert.ToDecimal(RightAmount.Text),2);
                App.leftAmount = Decimal.Round(App.rightCurrency.convert(App.rightAmount, App.leftCurrency),2);
                LeftAmount.Text = App.leftAmount.ToString();
            }
            catch (FormatException fe)
            {
                LeftAmount.Text = "0";
            }
        }
    }
}
