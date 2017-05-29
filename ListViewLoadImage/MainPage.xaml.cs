using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace ListViewLoadImage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Data> Images  { get;set;}
        public MainPage()
        {
            this.InitializeComponent();
            Images = new ObservableCollection<Data>();
            
            Images.Add(new Data() { Path= @"ms-appx:///Assets/pic.png" });
            Images.Add(new Data ());

            this.DataContext = this;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/pic.png"));
            Debug.WriteLine(imageFile.Name);
        }
    }

    public class Data
    {
        public string Path { get; set; }
    }
}
