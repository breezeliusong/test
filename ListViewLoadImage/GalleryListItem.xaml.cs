using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ListViewLoadImage
{
    public sealed partial class GalleryListItem : UserControl
    {
        public GalleryListItem()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Create bindings for image path
        /// </summary>
        public string ImagePath
        {
            get
            {
                return (string)GetValue(imagePathProperty);
            }
            set
            {
                SetValue(imagePathProperty, value);
            }
        }

        public static DependencyProperty imagePathProperty = DependencyProperty.Register("ImagePath", typeof(string), typeof(GalleryListItem), new PropertyMetadata("", ImagePathCallback));

        public static async void ImagePathCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var imagePath = e.NewValue as string;
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath)) return;

            GalleryListItem gli = sender as GalleryListItem;

            Uri uri = new Uri(imagePath);
            // load the image
            BitmapImage bitmap = new BitmapImage();
           var imageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var stream = await imageFile.OpenReadAsync())
            {
                await bitmap.SetSourceAsync(stream);
            }

            // set the image in the ListItem
            await gli.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                gli.Image.Source = bitmap;
            });
        }
    }
}
