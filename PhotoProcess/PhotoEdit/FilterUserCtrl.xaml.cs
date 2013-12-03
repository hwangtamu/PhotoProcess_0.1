using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Nokia.Graphics.Imaging;
using Windows.Foundation;


namespace PhotoProcess.PhotoEdit
{
    /// <summary>
    /// This class implements methods for calling filter functions
    /// </summary>
    public partial class FilterUserCtrl : UserControl
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
        public FilterUserCtrl()
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
        /// magic pen filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMagic_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new MagicPenFilter());
            apply();
        }

        /// <summary>
        /// antique filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAntique_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new AntiqueFilter());
            apply();
        }

        /// <summary>
        /// skecth filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSketch_Click(object sender, RoutedEventArgs e)
        {
            _components.Add(new SketchFilter());
            apply();
        }
        #endregion

        #region other function
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
