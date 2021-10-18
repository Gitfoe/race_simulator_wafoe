using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ViewGraphic.Classes
{
    public static class GraphicsCache
    {
        // Fields
        private static Dictionary<string, Bitmap> _cache;

        // Methods
        public static void Initialize()
        {
            _cache = new Dictionary<string, Bitmap>();
        }

        public static Bitmap GetBitmapData(string imageLocation)
        {
            // Checks if the key "imageLocation" exists in the _cache dictionary, and if it does not exist, adds the imageLocation and creates a Bitmap for it
            if (!_cache.ContainsKey(imageLocation))
            {
                _cache.Add(imageLocation, new Bitmap($@"{imageLocation}"));
            }
            return _cache[imageLocation];
        }

        public static void ClearCache()
        { // Clears the _cache dictionary
            _cache.Clear();
        }
    }
}
