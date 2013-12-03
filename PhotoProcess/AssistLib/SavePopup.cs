using System;
using System.Windows.Media.Imaging;

using Coding4Fun.Toolkit.Controls;

namespace PhotoProcess.AssistLib
{
    /// <summary>
    /// Popup window to get image name from user input
    /// </summary>
    public class SavePopup
    {
        #region private variables
        private WriteableBitmap toSaveBitmap;
        #endregion

        #region constructor
        public SavePopup(WriteableBitmap toSaveBitmap)
        {
            this.toSaveBitmap = toSaveBitmap;
        }
        #endregion

        #region Main function
        /// <summary>
        /// show popup window
        /// </summary>
        public void Show()
        {
            InputPrompt fileName = new InputPrompt();
            fileName.Title = "Photo Name";
            fileName.Message = "What should we call the Photo?";
            fileName.Completed += FileNameCompleted;
            fileName.Show();
        }

        /// <summary>
        /// save to photo library when fileName is completed
        /// </summary>
        private void FileNameCompleted(object sender, PopUpEventArgs<string, PopUpResult> e)
        {

            if (e.PopUpResult == PopUpResult.Ok)
            {
                // Get fileName from user input
                string fileName = e.Result;
                fileName = fileName.EndsWith(".jpg") ? fileName : fileName + ".jpg";
                PhotoHelper.SaveToPhotoLibrary(fileName, this.toSaveBitmap);
            }
        }
        #endregion
    }
}
