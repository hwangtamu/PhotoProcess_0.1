using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Nokia.Graphics.Imaging;


namespace PhotoProcess.PhotoEdit
{
    /// <summary>
    /// This class implements methods for calling filter functions
    /// </summary>
    public partial class Filter2UserCtrl : UserControl
    {
        #region private variables
        private Image imageRender;
        private StackPanel mainFuncuserCtrl;
        // stack to store all the processed images 
        Stack<WriteableBitmap> processedBmpImageStack;

        // output variable: store processed bitmap
        private WriteableBitmap orginalBitmap;

        // orginal image data read from opened photo 
        private byte[] imageData;

        // list to store all the chosen filters
        public List<IFilter> _components;
        #endregion

        #region constructor
        public Filter2UserCtrl()
        {
            InitializeComponent();
        }

        public void setImageGrid(StackPanel mainFuncuserCtrl, Image imageRender, byte[] imageData,
            Stack<WriteableBitmap> processedBmpImageStack, List<IFilter> components)
        {
            this.processedBmpImageStack = processedBmpImageStack;
            this.imageRender = imageRender;
            this.imageData = imageData;
            this.mainFuncuserCtrl = mainFuncuserCtrl;
            this._components = components;
        }
        #endregion

        #region button event
        /// <summary>
        /// back to main photo editing page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBackToMainFunc_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            mainFuncuserCtrl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// boost filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBoost_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new ColorBoostFilter(3.0f));
            apply();
        }

        /// <summary>
        /// trint filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTint_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new TemperatureAndTintFilter(0.8, -0.4));
            apply();
        }

        /// <summary>
        /// level filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLevel_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new LevelsFilter(0.7f, 0.2f, 0.4f));
            apply();
        }
        #endregion

        #region other functions
        /// <summary>
        /// apply all the filter effects
        /// </summary>
        private void apply()
        {
            this.orginalBitmap = processedBmpImageStack.Peek();
            WriteableBitmap processedBmpImage = new WriteableBitmap(orginalBitmap.PixelWidth, orginalBitmap.PixelHeight);
            Filter filter = new Filter();
            filter.applyFilter(imageData, processedBmpImage, _components);
            imageRender.Source = processedBmpImage;
            processedBmpImageStack.Push(processedBmpImage);
        }
        #endregion

    }
}
