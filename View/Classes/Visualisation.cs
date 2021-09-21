using Model.Classes;
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
            // to initialize vars
        }

        public static string DrawTrack(Track track)
        {
            Console.SetCursorPosition(0, 0);
        }

        // Region
        #region graphics
        // Draw the graphics per line horizontally - also, karts only move clockwise, not counter clockwise
        private static string[] _startGridRight =      { "----",
                                                         "  → ",
                                                         " →  ",
                                                         "----" };
        private static string[] _startGridLeft =       { "----",
                                                         "  ← ",
                                                         " ←  ",
                                                         "----" };
        private static string[] _startGridUp=          { "|  |",
                                                         "|↑ |",
                                                         "| ↑|",
                                                         "|  |" };
        private static string[] _startGridDown =       { "|  |",
                                                         "|↓ |",
                                                         "| ↓|",
                                                         "|  |" };
        private static string[] _finishHorizontal =    { "----",
                                                         " ## ",
                                                         " ## ",
                                                         "----" };
        private static string[] _finishVertical =      { "|  |",
                                                         "|##|",
                                                         "|##|",
                                                         "|  |" };
        private static string[] _straightHorizontal =  { "----",
                                                         "    ",
                                                         "    ",
                                                         "----" };
        private static string[] _straightVertical =    { "|  |",
                                                         "|  |",
                                                         "|  |",
                                                         "|  |" };
        private static string[] _corner1 =             {@"--\ ", // right corner down & left corner up
                                                        @"   \",
                                                         "   |",
                                                        @"\  |" };
        private static string[] _corner2 =             {@" /--", // left corner down & right corner up
                                                        @"/   ",
                                                         "|   ",
                                                        @"|  /" };
        private static string[] _corner3 =             {@"|  \", // right corner up & left corner down
                                                         "|   ",
                                                        @"\   ",
                                                        @" \--" };
        private static string[] _corner4 =             {@"/  |", // left corner up & right corner down
                                                         "   |",
                                                        @"   /",
                                                        @"--/ " };
        #endregion
    }
}
