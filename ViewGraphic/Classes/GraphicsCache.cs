using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ViewGraphic.Classes
{
    public static class GraphicsCache
    {
        // Fields
        private static Dictionary<string, Bitmap> _cache;

        // Methods
        public static void Initialize()
        { // Initializes the _cache dictionary
            _cache = new Dictionary<string, Bitmap>();
        }

        public static Bitmap GetBitmap(string graphicLocation)
        {
            // Checks if the key "imageLocation" exists in the _cache dictionary, and if it does not exist, adds the imageLocation and creates a Bitmap for it
            if (!_cache.ContainsKey(graphicLocation))
            {
                _cache.Add(graphicLocation, new Bitmap(graphicLocation));
            }
            return _cache[graphicLocation];
        }

        public static void ClearCache()
        { // Clears the _cache dictionary
            _cache.Clear();
        }

        public static Bitmap EmptyTrack(int width, int height)
        { // Creates an "empty" track with a pink background color if it doesn't already exist
            if (!_cache.ContainsKey("empty"))
            {
                _cache.Add("empty", new Bitmap(width, height)); // Add the empty track to the cache with the width and height
                Graphics graphic = Graphics.FromImage(_cache["empty"]); // Draw a background
                graphic.Clear(System.Drawing.Color.DarkBlue); // Background color
            }
            return (Bitmap)_cache["empty"].Clone(); // Call by reference, so make a copy
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        { // This method converts a Bitmap to a BitmapSource
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
