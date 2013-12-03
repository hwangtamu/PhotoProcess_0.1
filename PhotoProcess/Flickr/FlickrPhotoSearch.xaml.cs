using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace PhotoProcess.Flickr
{
    /// <summary>
    /// Flickr searching page
    /// </summary>
    public partial class FlickrPhotoSearch : PhoneApplicationPage
    {
        #region constructor
        public FlickrPhotoSearch()
        {
            InitializeComponent();
            Loaded += FlickrPhotoPage_Loaded;

            BuildLocalizedApplicationBar();
        }
        #endregion

        #region page loading event
        /// <summary>
        /// page loading event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FlickrPhotoPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMap();
        }

        private async void UpdateMap()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            Geoposition position =
                await geolocator.GetGeopositionAsync(
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(30));

            var gpsCoorCenter =
                new GeoCoordinate(
                    position.Coordinate.Latitude,
                    position.Coordinate.Longitude);

            AroundMeMap.Center = gpsCoorCenter;
            AroundMeMap.ZoomLevel = 15;

        }

        /// <summary>
        /// build app bar when page is loaded
        /// </summary>
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarSearchButton = new ApplicationBarIconButton(
                    new Uri("/Assets/search.png", UriKind.Relative));

            ApplicationBarIconButton appBarBackButton = new ApplicationBarIconButton(
                    new Uri("/Assets/back.png", UriKind.Relative));

            appBarSearchButton.Text = "Search";
            appBarSearchButton.Click += SearchClick;
            ApplicationBar.Buttons.Add(appBarSearchButton);

            appBarBackButton.Text = "Back To Main";
            appBarBackButton.Click += BackToMainPage;
            ApplicationBar.Buttons.Add(appBarBackButton);
        }
        #endregion

        #region button event
        /// <summary>
        /// back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// click this button to navigate to search result page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchClick(object sender, EventArgs e)
        {
            string topic = HttpUtility.UrlEncode(SearchTopic.Text);
            string navTo = string.Format(
                "/Flickr/SearchResult.xaml?latitude={0}&longitude={1}&topic={2}&radius={3}",
                AroundMeMap.Center.Latitude,
                AroundMeMap.Center.Longitude, topic, 5 );

            NavigationService.Navigate(new Uri(navTo, UriKind.RelativeOrAbsolute));
        }
        #endregion
    }

}