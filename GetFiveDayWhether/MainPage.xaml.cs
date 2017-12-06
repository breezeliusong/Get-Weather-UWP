using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetFiveDayWhether
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            collection = new ObservableCollection<List>();
            //this.DataContext = this;
            ApplicationData.Current.LocalSettings.Values["name"] = "Hello world";

        }

        public ObservableCollection<List> collection { get; set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var position1 = await LocationManager.GetPosition();
            var latitude1 = position1.Coordinate.Latitude;
            var longitude1 = position1.Coordinate.Longitude;
            RootObject forecast = await WeatherForecast.GetWeatherForecast(latitude1, longitude1);

            for (int i = 0; i < 5; i++)
            {

                collection.Add(forecast.list[i]);
            }

            ForecastGridView.ItemsSource = collection;

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("file.txt", CreationCollisionOption.GenerateUniqueName);
            await ApplicationData.Current.LocalCacheFolder.CreateFileAsync("file.txt", CreationCollisionOption.GenerateUniqueName);
            await ApplicationData.Current.TemporaryFolder.CreateFileAsync("file.txt", CreationCollisionOption.GenerateUniqueName);
            await ApplicationData.Current.RoamingFolder.CreateFileAsync("file.txt", CreationCollisionOption.GenerateUniqueName);
            string content= (string)ApplicationData.Current.LocalSettings.Values["name"];
            if (content == null)
            {
                content = "there is no the data";
            }
            txtblock.Text = content;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await ApplicationData.Current.ClearAsync();
        }
    }
    public class displayData
    {
        public string date { get; set; }
        public double temperature { get; set; }
        public string description { get; set; }
    }

}
