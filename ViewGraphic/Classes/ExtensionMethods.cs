using System;
using System.Diagnostics;
using System.Drawing;

namespace ViewGraphic.Classes
{
    public static class ExtensionMethods
    {
        // Extension methods
        public static Bitmap RotateImage(this Bitmap b, float angle) // Extension method
        {
            // Create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            // Make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                // Move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                // Draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }
        public static TimeSpan RoundSeconds(this TimeSpan span, int nDigits)
        {
            // TimeSpan.FromSeconds rounds to nearest millisecond, so nDigits should be 3 or less - won't get good answer beyond 3 digits
            Debug.Assert(nDigits <= 3);
            return TimeSpan.FromSeconds(Math.Round(span.TotalSeconds, nDigits));
        }
    }
}
