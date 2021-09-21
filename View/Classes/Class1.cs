using System;
using System.Collections.Generic;
using System.Text;

namespace View.Classes
{
    public static class Visualisation
    {
        // Properties
        public static string[] MyProperty { get; set; }

        // Methods
        public static void Initiliaze()
        {

        }

        public static void DrawTrack()
        {

        }

        // Region
        #region graphics
        private static string[] _startGridHorizontal = { "----",
                                                         "  > ", 
                                                         " >  ",
                                                         "----" };
        private static string[] _startGridVertical =   { "|  |",
                                                         "|^ |",
                                                         "| ^|",
                                                         "|  |" };
        private static string[] _finishHorizontal =    { "----",
                                                         "  # ",
                                                         "  # ",
                                                         "----" };
        private static string[] _finishVertical =      { "|  |",
                                                         "|##|",
                                                         "|  |",
                                                         "|  |" };
        private static string[] _straightHorizontal =  { "----",
                                                         "    ",
                                                         "    ",
                                                         "----" };
        private static string[] _straightVertical =    { "|  |",
                                                         "|  |",
                                                         "|  |",
                                                         "|  |" };
        private static string[] _rightCornerDown =     {@"--\ ",
                                                        @"   \",
                                                         "   |",
                                                        @"\  |" };
        private static string[] _leftCornerDown =      {@" /--",
                                                        @"/   ",
                                                         "|   ",
                                                        @"|  /" };
        private static string[] _rightCornerUp =       {@"|  \",
                                                         "|   ",
                                                        @"\   ",
                                                        @" \--" };
        private static string[] _leftCornerUp =        {@"/  |",
                                                         "   |",
                                                        @"   /",
                                                        @"--/ " };
        #endregion
    }
}
