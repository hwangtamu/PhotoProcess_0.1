using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using PhotoProcess.AssistLib;

namespace PhotoProcess.Flickr
{
    /// <summary>
    /// search result page
    /// </summary>
    public partial class SearchResult : PhoneApplicationPage
    {
        #region private variables
        // API key
        private const string flickrApiKey = "6958b431c0133c63069347ef6723747a";
        
        private double latitude;
        private double longitude;
        private double radius;
        private string topic;
        #endregion

        #region constructor
        public SearchResult()
        {
            InitializeComponent();

            Loaded += SearchResult_Loaded;

            BuildLocalizedApplicationBar();
        }
        #endregion

        #region page loading event
        /// <summary>
        /// load search results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchResult_Loaded(object sender, RoutedEventArgs e)
        {
            var images = await FlickrImage.GetFlickrImages(flickrApiKey, topic, latitude, longitude, radius);
            DataContext = images;

            if (images.Count == 0)
                NoPhotosFound.Visibility = Visibility.Visible;
            else
                NoPhotosFound.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// parse parameters from previous page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            latitude = Convert.ToDouble(NavigationContext.QueryString["latitude"]);
            longitude = Convert.ToDouble(NavigationContext.QueryString["longitude"]);
            radius = Convert.ToDouble(NavigationContext.QueryString["radius"]);
            topic = NavigationContext.QueryString["topic"];
        }

        /// <summary>
        /// buuild app bar when page is loaded
        /// </summary>
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarSaveButton = new ApplicationBarIconButton(
                    new Uri("/Assets/save.png", UriKind.Relative));
            appBarSaveButton.Text = "Save";
            appBarSaveButton.Click += SaveToPhotoLibrary;
            ApplicationBar.Buttons.Add(appBarSaveButton);

            ApplicationBarIconButton appBarBackButton = new ApplicationBarIconButton(
                    new Uri("/Assets/back.png", UriKind.Relative));
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
        /// save to photo library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveToPhotoLibrary(object sender, EventArgs e)
        {
            List<FlickrImage> imgList = new List<FlickrImage>();
            foreach (object item in PhotoToSave.SelectedItems)
            {
                FlickrImage img = item as FlickrImage;
                if (img != null)
                {
                    imgList.Add(img);
                }
            }
            PhotoHelper.CleanStorage();
            await PhotoHelper.SaveToPhotoLibrary(imgList);
            
            // navigate to photo library for editing
            MessageBoxResult result = MessageBox.Show("Successfully saved to local Photo Library!", "Edit Now?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                NavigationService.Navigate(new Uri("/PhotoEdit/PhotoEdit.xaml", UriKind.RelativeOrAbsolute));
            }
        }
        #endregion

        #region other events
        /// <summary>
        /// selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhotosToSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhotoToSave.SelectedItems.Count == 0)
                ApplicationBar.IsVisible = false;
            else
                ApplicationBar.IsVisible = true;
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            if (img == null) return;

            Storyboard s = new Storyboard();
            DoubleAnimation doubleAni = new DoubleAnimation();
            doubleAni.To = 1;
            doubleAni.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            Storyboard.SetTarget(doubleAni, img);
            Storyboard.SetTargetProperty(doubleAni, new PropertyPath(OpacityProperty));
            s.Children.Add(doubleAni);
            s.Begin();
        }
        #endregion
    }
}