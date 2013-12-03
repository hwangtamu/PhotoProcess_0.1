using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Nokia.Graphics.Imaging;

namespace PhotoProcess.PhotoEdit
{
    /// <summary>
    /// This class is calling Nokia package to apply filters
    /// </summary>
    public class Filter
    {
        public async Task applyFilter(byte[] imageData, WriteableBitmap processedBitmap, List<IFilter> _components)
        {   
            MemoryStream ms = new MemoryStream(imageData);
            var source = new StreamImageSource(ms);
            var effect = new FilterEffect(source);
            var renderer = new WriteableBitmapRenderer(effect, processedBitmap);

            var filters = new List<IFilter>();
            filters = _components;

            effect.Filters = filters;
            // image processing
            await renderer.RenderAsync();
            processedBitmap.Invalidate();
        }
    }
}
