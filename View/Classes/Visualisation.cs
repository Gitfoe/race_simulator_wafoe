using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using static Model.Classes.Section;

namespace View.Classes
{
    public static class Visualisation
    {
        // Methods
        public static void DrawTrack(Track track)
        {
            // Convert sections to graphicsSectionTypes and positions and also keep track of the direction of the previous graphic section type
            List<GraphicSectionTypes> graphicSectionTypesList = new List<GraphicSectionTypes>();
            List<int[]> positionsList = new List<int[]>();
            CardinalDirections directionPreviousGraphicSectionType = default; // Assign temp value

            // GraphicSectionTypes enum and int[x, y] array
            int counter = 0;
            foreach (Section section in track.Sections)
            {
                if (counter > 0)
                {
                    graphicSectionTypesList.Add(DetermineNextGraphicSectionType(section, ref directionPreviousGraphicSectionType));
                    positionsList.Add(DetermineNextPosition(directionPreviousGraphicSectionType));
                }
                //graphicSections.Add(DetermineFollowingGraphicSections(graphicSections[counter]));
                else if (counter == 0)
                {
                    // Add the first section to the lists and make the outward direction east
                    graphicSectionTypesList.Add(DetermineFirstGraphicSectionType(section));
                    directionPreviousGraphicSectionType = CardinalDirections.East;
                    positionsList.Add(DetermineNextPosition(directionPreviousGraphicSectionType));
                }
                counter++; // Increment counter for correct looping
            }
            WriteGraphicsToConsole(graphicSectionTypesList, positionsList); // Finally write the track graphics!
        }

        private static GraphicSectionTypes DetermineFirstGraphicSectionType(Section section)
        { // Determine what direction and type the first section will be since this is a guess and can be any angle, we put it to east
            return section.SectionType switch
            {
                SectionTypes.StartGrid => GraphicSectionTypes.StartGridEast,
                SectionTypes.Finish => GraphicSectionTypes.FinishEast,
                SectionTypes.Straight => GraphicSectionTypes.StraightEast,
                SectionTypes.RightCorner => GraphicSectionTypes.CornerEastSouth,
                SectionTypes.LeftCorner => GraphicSectionTypes.CornerNorthEast,
                _ => default
            };
        }

        private static int[] DetermineNextPosition(CardinalDirections directionPreviousGraphicSectionType)
        {
            // Determines the positional values the GraphicSectionType needs to get for the console (returns x and y movement values)
            if (directionPreviousGraphicSectionType == CardinalDirections.North)
            {
                return new int[] { 0, -4 }; // x = 0, y = -4
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.East)
            {
                return new int[] { 4, 0 }; // x = 4, y = 0
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.South)
            {
                return new int[] { 0, 4 }; // x = 0, y = 4
            }
            else // the same as directionPreviousGraphicSectionType == CardinalDirections.West
            {
                return new int[] { -4, 0 }; // x = -4, y = 0
            }
        }

        private static GraphicSectionTypes DetermineNextGraphicSectionType(Section section, ref CardinalDirections directionPreviousGraphicSectionType)
        {
            // Based on the previous angle of the GraphicSectionType and new section, declare a new graphicSectionType
            GraphicSectionTypes newGraphicSectionType = default;
            if (directionPreviousGraphicSectionType == CardinalDirections.North)
            {
                switch (section.SectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case SectionTypes.StartGrid: newGraphicSectionType = GraphicSectionTypes.StartGridNorth; break;
                    case SectionTypes.Finish: newGraphicSectionType = GraphicSectionTypes.FinishNorth; break;
                    case SectionTypes.Straight: newGraphicSectionType = GraphicSectionTypes.StraightNorth; break;
                    case SectionTypes.RightCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.RightCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.East;
                        break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.West;
                        break;
                    default: break;
                }
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.East)
            {
                switch (section.SectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case SectionTypes.StartGrid: newGraphicSectionType = GraphicSectionTypes.StartGridEast; break;
                    case SectionTypes.Finish: newGraphicSectionType = GraphicSectionTypes.FinishEast; break;
                    case SectionTypes.Straight: newGraphicSectionType = GraphicSectionTypes.StraightEast; break;
                    case SectionTypes.RightCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.RightCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.South;
                        break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.North;
                        break;
                    default: break;
                }
            }
            else if (directionPreviousGraphicSectionType == CardinalDirections.South)
            {
                switch (section.SectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case SectionTypes.StartGrid: newGraphicSectionType = GraphicSectionTypes.StartGridSouth; break;
                    case SectionTypes.Finish: newGraphicSectionType = GraphicSectionTypes.FinishSouth; break;
                    case SectionTypes.Straight: newGraphicSectionType = GraphicSectionTypes.StraightSouth; break;
                    case SectionTypes.RightCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.RightCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.West;
                        break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.East;
                        break;
                    default: break;
                }
            }
            else // the same as directionPreviousGraphicSectionType == CardinalDirections.West
            {
                switch (section.SectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case SectionTypes.StartGrid: newGraphicSectionType = GraphicSectionTypes.StartGridWest; break;
                    case SectionTypes.Finish: newGraphicSectionType = GraphicSectionTypes.FinishWest; break;
                    case SectionTypes.Straight: newGraphicSectionType = GraphicSectionTypes.StraightWest; break;
                    case SectionTypes.RightCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.RightCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.North;
                        break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.South;
                        break;
                    default: break;
                }
            }
            return newGraphicSectionType;
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

        private static List<string[]> ConvertGraphicSectionTypesToGraphicArrays(List<GraphicSectionTypes> graphicSectionTypesList)
        {
            List<string[]> graphicSectionsList = new List<string[]>(); // Create list for all the graphic array values
            foreach (Enum graphicSectionType in graphicSectionTypesList) // Loop through the section Enums
            {
                switch (graphicSectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case GraphicSectionTypes.StartGridNorth: graphicSectionsList.Add(_startGridNorth); break;
                    case GraphicSectionTypes.StartGridEast: graphicSectionsList.Add(_startGridEast); break;
                    case GraphicSectionTypes.StartGridSouth: graphicSectionsList.Add(_startGridSouth); break;
                    case GraphicSectionTypes.StartGridWest: graphicSectionsList.Add(_startGridWest); break;
                    case GraphicSectionTypes.FinishNorth: graphicSectionsList.Add(_finishVertical); break;
                    case GraphicSectionTypes.FinishEast: graphicSectionsList.Add(_finishHorizontal); break;
                    case GraphicSectionTypes.FinishSouth: graphicSectionsList.Add(_finishVertical); break;
                    case GraphicSectionTypes.FinishWest: graphicSectionsList.Add(_finishHorizontal); break;
                    case GraphicSectionTypes.StraightNorth: graphicSectionsList.Add(_straightVertical); break;
                    case GraphicSectionTypes.StraightEast: graphicSectionsList.Add(_straightHorizontal); break;
                    case GraphicSectionTypes.StraightSouth: graphicSectionsList.Add(_straightVertical); break;
                    case GraphicSectionTypes.StraightWest: graphicSectionsList.Add(_straightHorizontal); break;
                    case GraphicSectionTypes.CornerSouthWest: graphicSectionsList.Add(_cornerSouthWest); break;
                    case GraphicSectionTypes.CornerEastSouth: graphicSectionsList.Add(_cornerEastSouth); break;
                    case GraphicSectionTypes.CornerNorthEast: graphicSectionsList.Add(_cornerNorthEast); break;
                    case GraphicSectionTypes.CornerNorthWest: graphicSectionsList.Add(_cornerNorthWest); break;
                    default: break;
                }
            }
            return graphicSectionsList;
        }

        private static void WriteGraphicsToConsole(List<GraphicSectionTypes> graphicSectionTypesList, List<int[]> positionsList)
        {
            List<string[]> graphicSectionsList = ConvertGraphicSectionTypesToGraphicArrays(graphicSectionTypesList); // Convert the Enums to actual Graphics
            int[] tempCursorPosition = FixCursorPosition(positionsList); // Fix the cursor position if the x or y count is negative
            Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1]); // Set the cursor once to the corrected position
            for (int i = 0; i < graphicSectionsList.Count; i++) // Start loop at the length of graphicSectionsList (is the same as positionsList)
            {
                int counter = 0;
                foreach (string line in graphicSectionsList[i]) // Print each array line following the for loop index
                {
                    Console.Write($"{line}\n");
                    counter++;
                    // Compensate the cursor so it doesn't go to x = 0 after a new line by using a counter
                    Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1] + counter);                    
                }
                // Set the cursor afterwards to the next position for the next section
                tempCursorPosition[0] += positionsList[i][0];
                tempCursorPosition[1] += positionsList[i][1];
                Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1]);
            }

        }

        public static int[] FixCursorPosition(List<int[]> positionsList)
        {
            // Compensation algorithm for the console so graphics don't go out of bounds
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

        #region graphics
        // Draw the graphics per line horizontally
        private static string[] _startGridNorth =      { "|  |",
                                                         "|v |",
                                                         "| v|",
                                                         "|  |" };
        private static string[] _startGridEast =       { "----",
                                                         "  > ",
                                                         " >  ",
                                                         "----" };
        private static string[] _startGridSouth =      { "|  |",
                                                         "|↓ |",
                                                         "| ↓|",
                                                         "|  |" };
        private static string[] _startGridWest =       { "----",
                                                         "  < ",
                                                         " <  ",
                                                         "----" };
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
        private static string[] _cornerSouthWest =     {@"--\ ", // right corner down & left corner up
                                                        @"   \",
                                                         "   |",
                                                        @"\  |" };
        private static string[] _cornerEastSouth =     {@" /--", // left corner down & right corner up
                                                        @"/   ",
                                                         "|   ",
                                                        @"|  /" };
        private static string[] _cornerNorthEast =     {@"|  \", // right corner up & left corner down
                                                         "|   ",
                                                        @"\   ",
                                                        @" \--" };
        private static string[] _cornerNorthWest =     {@"/  |", // left corner up & right corner down
                                                         "   |",
                                                        @"   /",
                                                        @"--/ " };
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