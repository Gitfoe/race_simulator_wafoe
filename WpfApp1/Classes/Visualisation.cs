using Controller.Classes;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Model.Classes.Section;

namespace ViewGraphic.Classes
{
    public static class Visualisation
    {
        // Fields
        private const int _sectionPixels = 64; // Set the amount of width/x and height/y (they are square) for the section
        private const int _driverPixels = 28; // Set the amount of width/x and height/y (they are square) for the driver

        // Methods
        public static BitmapSource DrawTrack(Track track)
        {
            //Bitmap test = GraphicsCache.GetBitmap(_finishEast);
            //graphics.DrawImage(test, 0, 0, 64, 64); // draw the image

            // Convert sections to graphicsSectionTypes and positions and also keep track of the direction of the previous graphic section type
            List<GraphicSectionTypes> graphicSectionTypesList = new List<GraphicSectionTypes>();
            List<int[]> positionsList = new List<int[]>();
            List<CardinalDirections> directionPreviousGraphicSectionTypeList = new List<CardinalDirections>();

            // Used a fixed value for the first section instead (east)
            CardinalDirections directionPreviousGraphicSectionType = CardinalDirections.East;

            foreach (Section section in track.Sections)
            {
                graphicSectionTypesList.Add(DetermineNextGraphicSectionType(section, ref directionPreviousGraphicSectionType));
                positionsList.Add(DetermineNextPosition(directionPreviousGraphicSectionType));
                directionPreviousGraphicSectionTypeList.Add(directionPreviousGraphicSectionType);
            }

            int[] trackSize = DetermineTrackSize(positionsList);
            Bitmap canvas = GraphicsCache.EmptyTrack(trackSize[0], trackSize[1]);
            Graphics graphics = Graphics.FromImage(canvas);

            WriteGraphicsToWPF(graphicSectionTypesList, positionsList, track.Sections, directionPreviousGraphicSectionTypeList, graphics); // Finally write the track graphics!

            return GraphicsCache.CreateBitmapSourceFromGdiBitmap(canvas);
        }

        private static int[] DetermineNextPosition(CardinalDirections directionPreviousGraphicSectionType)
        {
            // Determines the positional values the GraphicSectionType needs to get for the console (returns x and y movement values)
            if (directionPreviousGraphicSectionType == CardinalDirections.North)
            {
                return new int[] { 0, -_sectionPixels }; // x = 0, y = -4
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.East)
            {
                return new int[] { _sectionPixels, 0 }; // x = 4, y = 0
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.South)
            {
                return new int[] { 0, _sectionPixels }; // x = 0, y = 4
            }
            else // the same as directionPreviousGraphicSectionType == CardinalDirections.West
            {
                return new int[] { -_sectionPixels, 0 }; // x = -4, y = 0
            }
        }

        private static GraphicSectionTypes DetermineNextGraphicSectionType(Section section, ref CardinalDirections directionPreviousGraphicSectionType)
        {
            // Based on the previous angle of the GraphicSectionType and new section, declare a new graphicSectionType
            GraphicSectionTypes newGraphicSectionType = default;
            if (directionPreviousGraphicSectionType == CardinalDirections.North)
            {
                AllocateNextGraphicSectionType(section, ref directionPreviousGraphicSectionType, ref newGraphicSectionType, new Enum[] {
                GraphicSectionTypes.StartGridNorth, GraphicSectionTypes.FinishNorth, GraphicSectionTypes.StraightNorth, CardinalDirections.East, CardinalDirections.West});
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.East)
            {
                AllocateNextGraphicSectionType(section, ref directionPreviousGraphicSectionType, ref newGraphicSectionType, new Enum[] {
                GraphicSectionTypes.StartGridEast, GraphicSectionTypes.FinishEast, GraphicSectionTypes.StraightEast, CardinalDirections.South, CardinalDirections.North});
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.South)
            {
                AllocateNextGraphicSectionType(section, ref directionPreviousGraphicSectionType, ref newGraphicSectionType, new Enum[] {
                GraphicSectionTypes.StartGridSouth,GraphicSectionTypes.FinishSouth, GraphicSectionTypes.StraightSouth, CardinalDirections.West, CardinalDirections.East });
            }
            else // the same as directionPreviousGraphicSectionType == CardinalDirections.West
            {
                AllocateNextGraphicSectionType(section, ref directionPreviousGraphicSectionType, ref newGraphicSectionType, new Enum[] {
                GraphicSectionTypes.StartGridWest, GraphicSectionTypes.FinishWest, GraphicSectionTypes.StraightWest, CardinalDirections.North, CardinalDirections.South });
            }
            return newGraphicSectionType;
        }

        private static void AllocateNextGraphicSectionType(Section section, ref CardinalDirections directionPreviousGraphicSectionType, ref GraphicSectionTypes newGraphicSectionType, Enum[] values)
        {
            switch (section.SectionType) // Allocate correct graphic array to Enum and add to list
            {
                case SectionTypes.StartGrid:
                    newGraphicSectionType = (GraphicSectionTypes)values[0]; break;
                case SectionTypes.Finish:
                    newGraphicSectionType = (GraphicSectionTypes)values[1]; break;
                case SectionTypes.Straight:
                    newGraphicSectionType = (GraphicSectionTypes)values[2]; break;
                case SectionTypes.RightCorner:
                    newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.RightCorner, directionPreviousGraphicSectionType);
                    directionPreviousGraphicSectionType = (CardinalDirections)values[3]; break;
                case SectionTypes.LeftCorner:
                    newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                    directionPreviousGraphicSectionType = (CardinalDirections)values[4]; break;
                default: break;
            }
        }

        private static GraphicSectionTypes DetermineNextCornerGraphicSectionType(SectionTypes currentSectionType, CardinalDirections directionPreviousGraphicSectionType)
        {
            // Because corners enter at a certain CardinalDirection and exit at another CardinalDirection,
            // we nee to find which CardinalDirection the previous track has to declare a correct next GraphicSectionType
            if (currentSectionType == SectionTypes.RightCorner)
            {
                return directionPreviousGraphicSectionType switch
                {
                    CardinalDirections.North => GraphicSectionTypes.CornerEastSouth,
                    CardinalDirections.East => GraphicSectionTypes.CornerSouthWest,
                    CardinalDirections.South => GraphicSectionTypes.CornerNorthWest,
                    CardinalDirections.West => GraphicSectionTypes.CornerNorthEast,
                    _ => default
                };
            }
            else // Same as currentSectionType == SectionTypes.LeftCorner
            {
                return directionPreviousGraphicSectionType switch
                {
                    CardinalDirections.North => GraphicSectionTypes.CornerSouthWest,
                    CardinalDirections.East => GraphicSectionTypes.CornerNorthWest,
                    CardinalDirections.South => GraphicSectionTypes.CornerNorthEast,
                    CardinalDirections.West => GraphicSectionTypes.CornerEastSouth,
                    _ => default
                };
            }
        }

        private static List<Bitmap> ConvertGraphicSectionTypesToGraphics(List<GraphicSectionTypes> graphicSectionTypesList)
        {
            List<Bitmap> graphicSectionsList = new List<Bitmap>(); // Create list for all the graphic bitmap values
            foreach (Enum graphicSectionType in graphicSectionTypesList) // Loop through the section Enums
            {
                switch (graphicSectionType) // Allocate correct graphic  to Enum and add to list
                {
                    case GraphicSectionTypes.StartGridNorth: graphicSectionsList.Add(GraphicsCache.GetBitmap(_startGridNorth)); break;
                    case GraphicSectionTypes.StartGridEast: graphicSectionsList.Add(GraphicsCache.GetBitmap(_startGridEast)); break;
                    case GraphicSectionTypes.StartGridSouth: graphicSectionsList.Add(GraphicsCache.GetBitmap(_startGridSouth)); break;
                    case GraphicSectionTypes.StartGridWest: graphicSectionsList.Add(GraphicsCache.GetBitmap(_startGridWest)); break;
                    case GraphicSectionTypes.FinishNorth: graphicSectionsList.Add(GraphicsCache.GetBitmap(_finishEast)); break;
                    case GraphicSectionTypes.FinishEast: graphicSectionsList.Add(GraphicsCache.GetBitmap(_finishEast)); break;
                    case GraphicSectionTypes.FinishSouth: graphicSectionsList.Add(GraphicsCache.GetBitmap(_finishSouth)); break;
                    case GraphicSectionTypes.FinishWest: graphicSectionsList.Add(GraphicsCache.GetBitmap(_finishWest)); break;
                    case GraphicSectionTypes.StraightNorth: case GraphicSectionTypes.StraightEast: case GraphicSectionTypes.StraightSouth: case GraphicSectionTypes.StraightWest:
                        graphicSectionsList.Add(GraphicsCache.GetBitmap(_straight)); break;
                    case GraphicSectionTypes.CornerSouthWest: graphicSectionsList.Add(GraphicsCache.GetBitmap(_cornerSouthWest)); break;
                    case GraphicSectionTypes.CornerEastSouth: graphicSectionsList.Add(GraphicsCache.GetBitmap(_cornerEastSouth)); break;
                    case GraphicSectionTypes.CornerNorthEast: graphicSectionsList.Add(GraphicsCache.GetBitmap(_cornerNorthEast)); break;
                    case GraphicSectionTypes.CornerNorthWest: graphicSectionsList.Add(GraphicsCache.GetBitmap(_cornerNorthWest)); break;
                    default: break;
                }
            }
            return graphicSectionsList;
        }

        private static void WriteGraphicsToWPF(List<GraphicSectionTypes> graphicSectionTypesList, List<int[]> positionsList, LinkedList<Section> sectionList, List<CardinalDirections> directionPreviousGraphicSectionType, Graphics graphics)
        {
            int[] tempCursorPosition = FixCursorPosition(positionsList); // Fix the cursor position if the x or y count is negative
            WriteSectionsToWPF(graphicSectionTypesList, positionsList, graphics, tempCursorPosition);
            WriteDriversToWPF(positionsList, graphics, tempCursorPosition, sectionList, directionPreviousGraphicSectionType);
        }

        private static void WriteSectionsToWPF(List<GraphicSectionTypes> graphicSectionTypesList, List<int[]> positionsList, Graphics graphics, int[] tempCursorPosition)
        { // Only write the sections to the WPF
            int[] tempCursorPosition2 = (int[])tempCursorPosition.Clone(); // Make clone of the temporary cursor position
            List<Bitmap> graphicSectionsList = ConvertGraphicSectionTypesToGraphics(graphicSectionTypesList); // Convert the Enums to actual Graphics
            for (int i = 0; i < graphicSectionsList.Count; i++) // Start loop at the length of graphicSectionsList (is the same as positionsList, graphicSectionTypesList and sectionList)
            {
                graphics.DrawImage(graphicSectionsList[i], tempCursorPosition2[0], tempCursorPosition2[1], _sectionPixels, _sectionPixels); // Draw the image
                // Set the cursor afterwards to the next position for the next section
                tempCursorPosition2[0] += positionsList[i][0];
                tempCursorPosition2[1] += positionsList[i][1];
            }
        }

        private static void WriteDriversToWPF(List<int[]> positionsList, Graphics graphics, int[] tempCursorPosition, LinkedList<Section> sectionList, List<CardinalDirections> directionPreviousGraphicSectionType)
        { // Only write the sections to the WPF
            int[] tempCursorPosition2 = (int[])tempCursorPosition.Clone(); // Make clone of the temporary cursor position
            SectionData sectionData;
            Bitmap tempBitmap;
            for (int i = 0; i < sectionList.Count; i++)
            {
                sectionData = Data.CurrentRace.GetSectionData(sectionList.ElementAt(i));
                if (sectionData.Left != null)
                { // Generates the bitmap, rotates it and draws it
                    tempBitmap = RotateBitmapOfDriver(ReturnBitmapOfParticipant(sectionData.Left), directionPreviousGraphicSectionType.ElementAt(i));
                    int[] xyOffset = ReturnXYOffsetOfSection(directionPreviousGraphicSectionType.ElementAt(i), true);
                    graphics.DrawImage(tempBitmap, tempCursorPosition2[0] + xyOffset[0], tempCursorPosition2[1] + xyOffset[1], _driverPixels, _driverPixels); // Draw the image
                    if (sectionData.Left.Equipment.IsBroken)
                    { // Puts a thwomp on top of the driver if it is broken
                        tempBitmap = GraphicsCache.GetBitmap(_thwomp);
                        graphics.DrawImage(tempBitmap, tempCursorPosition2[0] + xyOffset[0], tempCursorPosition2[1] + xyOffset[1], _driverPixels, _driverPixels); // Draw the image
                    }
                }
                if (sectionData.Right != null)
                { // Generates the bitmap, rotates it and draws it
                    tempBitmap = RotateBitmapOfDriver(ReturnBitmapOfParticipant(sectionData.Right), directionPreviousGraphicSectionType.ElementAt(i));
                    int[] xyOffset = ReturnXYOffsetOfSection(directionPreviousGraphicSectionType.ElementAt(i), false);
                    graphics.DrawImage(tempBitmap, tempCursorPosition2[0] + xyOffset[0], tempCursorPosition2[1] + xyOffset[1], _driverPixels, _driverPixels); // Draw the image
                    if (sectionData.Right.Equipment.IsBroken)
                    { // Puts a thwomp on top of the driver if it is broken
                        tempBitmap = GraphicsCache.GetBitmap(_thwomp);
                        graphics.DrawImage(tempBitmap, tempCursorPosition2[0] + xyOffset[0], tempCursorPosition2[1] + xyOffset[1], _driverPixels, _driverPixels); // Draw the image
                    }
                }
                // Set the cursor afterwards to the next position for the next section
                tempCursorPosition2[0] += positionsList[i][0];
                tempCursorPosition2[1] += positionsList[i][1];
            }
        }

        public static int[] FixCursorPosition(List<int[]> positionsList)
        {
            // Compensation algorithm for the console so graphics don't go out of bounds and the track always starts at x = 0 y = 0
            int xCount = 0;
            int yCount = 0;
            int lowestXCount = 0;
            int lowestYCount = 0;
            int newXCount = 0;
            int newYCount = 0;
            foreach (int[] xy in positionsList)
            {
                xCount += xy[0]; // x
                if (xCount < lowestXCount) // If current xCount is lower than the lowest xCount, save the value
                {
                    lowestXCount = xCount;
                }
                yCount += xy[1]; // y
                if (yCount < lowestYCount) // If current yCount is lower than the lowest yCount, save the value
                {
                    lowestYCount = yCount;
                }
            }
            if (lowestXCount < 0)
            {
                newXCount = Math.Abs(lowestXCount); // If lowestXCount is negative, for example -8, set 8 to newXCount, otherwise do nothing
            }
            if (lowestYCount < 0)
            {
                newYCount = Math.Abs(lowestYCount); // If lowestYCount is negative, for example -8, set 8 to newYCount, otherwise do nothing
            }
            return new int[] { newXCount, newYCount };
        }

        public static int[] DetermineTrackSize(List<int[]> positionsList)
        {
            // Determines the total size of the track and fixes the background frame for it
            int xCount = 0;
            int yCount = 0;
            int lowestXCount = 0;
            int lowestYCount = 0;
            int highestXCount = 0;
            int highestYCount = 0;
            foreach (int[] xy in positionsList)
            {
                xCount += xy[0];
                yCount += xy[1];
                if (xy[0] < 0 && xCount < 0)
                {
                    lowestXCount += xy[0];
                }
                else if (xCount > highestXCount)
                {
                    highestXCount += xy[0];
                }

                if (xy[1] < 0 && yCount < 0)
                {
                    lowestYCount += xy[1];
                }
                else if (yCount > highestYCount)
                {
                    highestYCount += xy[1];
                }
            }
            return new int[] { highestXCount + Math.Abs(lowestXCount) + _sectionPixels, highestYCount + Math.Abs(lowestYCount) + _sectionPixels};
        }

        private static Bitmap ReturnBitmapOfParticipant(IParticipant participant)
        { // Returns the cached bitmap of a driver, or makes an new bitmap in cache for it
            return participant.TeamColor switch
            {
                TeamColors.Bowser => GraphicsCache.GetBitmap(_bowser),
                TeamColors.DKJunior => GraphicsCache.GetBitmap(_dkJunior),
                TeamColors.Koopa => GraphicsCache.GetBitmap(_koopa),
                TeamColors.Luigi => GraphicsCache.GetBitmap(_luigi),
                TeamColors.Mario => GraphicsCache.GetBitmap(_mario),
                TeamColors.Peach => GraphicsCache.GetBitmap(_peach),
                TeamColors.Toad => GraphicsCache.GetBitmap(_toad),
                TeamColors.Wafoe => GraphicsCache.GetBitmap(_wafoe),
                _ => default
            };
        }

        private static Bitmap RotateBitmapOfDriver(Bitmap bitmap, CardinalDirections directionPreviousGraphicSectionType)
        { // Rotates the bitmap of the driver using the previous section type information
            return directionPreviousGraphicSectionType switch
            {
                CardinalDirections.East => bitmap.RotateImage(90),
                CardinalDirections.South => bitmap.RotateImage(180),
                CardinalDirections.West => bitmap.RotateImage(270),
                _ => bitmap, // Don't rotate for north, because the driver bitmap defautly faces north
            };
        }

        private static int[] ReturnXYOffsetOfSection(CardinalDirections directionPreviousGraphicSectionType, bool left)
        { // Returns the offset values for the drivers on the track
            return left
                ? directionPreviousGraphicSectionType switch
                {
                    CardinalDirections.North => new int[] { 2, 36 },
                    CardinalDirections.East => new int[] { 0, 2 },
                    CardinalDirections.South => new int[] { 34, 0 },
                    CardinalDirections.West => new int[] { 36, 34 },
                    _ => default,
                }
                : directionPreviousGraphicSectionType switch
                {
                    CardinalDirections.North => new int[] { 34, 4 },
                    CardinalDirections.East => new int[] { 32, 34 },
                    CardinalDirections.South => new int[] { 2, 32 },
                    CardinalDirections.West => new int[] { 4, 2 },
                    _ => default,
                };
        }

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

        private static void CheckParticipants(string[] graphicSection, IParticipant leftParticipant, IParticipant rightParticipant, out string[] outputGraphicSection, out char leftParticipantFirstLetterName, out char rightParticipantFirstLetterName)
        {
            outputGraphicSection = (string[])graphicSection.Clone();
            leftParticipantFirstLetterName = ' ';
            rightParticipantFirstLetterName = ' ';
            if (leftParticipant != null)
            { // Assigns the first letter of a name to a temporary variable, or if the participant is not given, it keeps the default blank space
                if (leftParticipant.Equipment.IsBroken == true)
                {
                    leftParticipantFirstLetterName = '*'; // If the equipment is broken, set it to the broken char, which is *
                }
                else
                {
                    leftParticipantFirstLetterName = leftParticipant.Name[0];
                }
            }
            if (rightParticipant != null)
            {
                if (rightParticipant.Equipment.IsBroken == true)
                {
                    rightParticipantFirstLetterName = '*';
                }
                else
                {
                    rightParticipantFirstLetterName = rightParticipant.Name[0];
                }
            }
        }

        #region graphics
        // Sections
        private const string _startGridNorth = @"..\..\..\Graphics\Sections\StartGridNorth.png";
        private const string _startGridEast = @"..\..\..\Graphics\Sections\StartGridEast.png";
        private const string _startGridSouth = @"..\..\..\Graphics\Sections\StartGridSouth.png";
        private const string _startGridWest = @"..\..\..\Graphics\Sections\StartGridWest.png";
        private const string _finishNorth = @"..\..\..\Graphics\Sections\FinishNorth.png";
        private const string _finishEast = @"..\..\..\Graphics\Sections\FinishEast.png";
        private const string _finishSouth = @"..\..\..\Graphics\Sections\FinishSouth.png";
        private const string _finishWest = @"..\..\..\Graphics\Sections\FinishWest.png";
        private const string _straight = @"..\..\..\Graphics\Sections\Straight.png";
        private const string _cornerSouthWest = @"..\..\..\Graphics\Sections\CornerSouthWest.png";
        private const string _cornerEastSouth = @"..\..\..\Graphics\Sections\CornerEastSouth.png";
        private const string _cornerNorthEast = @"..\..\..\Graphics\Sections\CornerNorthEast.png";
        private const string _cornerNorthWest = @"..\..\..\Graphics\Sections\CornerNorthWest.png";

        // Drivers
        private const string _bowser = @"..\..\..\Graphics\Racers\Bowser.png";
        private const string _dkJunior = @"..\..\..\Graphics\Racers\DKJunior.png";
        private const string _koopa = @"..\..\..\Graphics\Racers\Koopa.png";
        private const string _luigi = @"..\..\..\Graphics\Racers\Luigi.png";
        private const string _mario = @"..\..\..\Graphics\Racers\Mario.png";
        private const string _peach = @"..\..\..\Graphics\Racers\Peach.png";
        private const string _toad = @"..\..\..\Graphics\Racers\Toad.png";
        private const string _wafoe = @"..\..\..\Graphics\Racers\Wafoe.png";

        // IsBroken
        private const string _thwomp = @"..\..\..\Graphics\Racers\Thwomp.png";

        #endregion
    }
    public enum GraphicSectionTypes
    { // Declare new section types for angle determination
        StartGridNorth,
        StartGridEast,
        StartGridSouth,
        StartGridWest,
        FinishNorth,
        FinishEast,
        FinishSouth,
        FinishWest,
        StraightNorth,
        StraightEast,
        StraightSouth,
        StraightWest,
        CornerSouthWest,
        CornerEastSouth,
        CornerNorthEast,
        CornerNorthWest
    };

    public enum CardinalDirections
    { // Cardinal directions of a compass
        North,
        East,
        South,
        West
    };
}
