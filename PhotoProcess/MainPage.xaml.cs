using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhotoProcess.Resources;

namespace PhotoProcess
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Button Event
        /// <summary>
        /// Navigate to photo library to edit photos  
        /// </summary>
        private void buttonOpenPhotoLib_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PhotoEdit/PhotoEdit.xaml", UriKind.RelativeOrAbsolute));           
        }

        /// <summary>
        /// Navigate to Open Camera to take photos
        /// </summary>
        private void buttonOpenCamera_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Camera/CameraPhoto.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Navigate to Flickr Search to get photos on line
        /// </summary>
        private void buttonOpenFlickr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Flickr/FlickrPhotoSearch.xaml", UriKind.RelativeOrAbsolute));
        }
        #endregion
    }
}