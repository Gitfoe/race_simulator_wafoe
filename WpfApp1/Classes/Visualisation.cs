using Model.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace ViewGraphic.Classes
{
    public static class Visualisation
    {
        public static BitmapSource DrawTrack(Track track)
        {
            return GraphicsCache.CreateBitmapSourceFromGdiBitmap(GraphicsCache.EmptyTrack(256, 256));
        }

        public static void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            DrawTrack(args.Track);
        }
    }
}
