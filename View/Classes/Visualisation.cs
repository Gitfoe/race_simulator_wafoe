using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using static Model.Classes.Section;
using System.Linq;

namespace View.Classes
{
    public static class Visualisation
    {
        // Variables
        private static Enum[] _movingDirectionNorth = new Enum[] { // x = 0, y = 4
            GraphicSectionTypes.StartGridNorth,
            GraphicSectionTypes.FinishNorth,
            GraphicSectionTypes.StraightNorth
        };
        private static Enum[] _movingDirectionEast = new Enum[] { // x = 4, y = 0
            GraphicSectionTypes.StartGridEast,
            GraphicSectionTypes.FinishEast,
            GraphicSectionTypes.StraightEast
        };
        private static Enum[] _movingDirectionSouth = new Enum[] { // x = 0, y = -4
            GraphicSectionTypes.StartGridSouth,
            GraphicSectionTypes.FinishSouth,
            GraphicSectionTypes.StraightSouth
        };
        private static Enum[] _movingDirectionWest = new Enum[] { // x = -4, y = 0
            GraphicSectionTypes.StartGridWest,
            GraphicSectionTypes.FinishWest,
            GraphicSectionTypes.StraightWest,
        };

        // Methods
        public static void DrawTrack(Track track)
        {
            // ConvertSectionsToGraphicsSectionTypesAndPositions
            List<Enum> graphicSectionTypesList = new List<Enum>();
            List<int[]> positionsList = new List<int[]>();

            // GraphicSectionTypes enum and int[x, y] array
            int counter = 0;
            foreach (Section section in track.Sections)
            {
                if (counter > 0)
                {
                    graphicSectionTypesList.Add(DetermineNextGraphicSectionType(section, graphicSectionTypesList[counter - 1]));
                    positionsList.Add(DetermineNextPosition(graphicSectionTypesList[counter]));
                }
                //graphicSections.Add(DetermineFollowingGraphicSections(graphicSections[counter]));
                else if (counter == 0)
                {
                    // Add the first section to the list and make it always go 4 to the right (the default first values only go to the right)
                    graphicSectionTypesList.Add(DetermineFirstGraphicSectionType(section));
                    positionsList.Add(new int[] { 0, 4 });
                }
                counter++; // Increment counter for correct looping
            }
            WriteGraphicsToConsole(graphicSectionTypesList, positionsList); // Finally write the track graphics!
        }

        private static Enum DetermineFirstGraphicSectionType(Section section)
        { // Determine what angle and type the first section will be since this is a guess and can be any angle
            return section.SectionType switch
            {
                SectionTypes.StartGrid => GraphicSectionTypes.StartGridEast,
                SectionTypes.Finish => GraphicSectionTypes.FinishEast,
                SectionTypes.Straight => GraphicSectionTypes.StraightEast,
                SectionTypes.RightCorner => GraphicSectionTypes.CornerSouthWest,
                SectionTypes.LeftCorner => GraphicSectionTypes.CornerNorthWest,
                _ => null
            };
        }

        private static CardinalDirections DetermineAngleOfGraphicSectionType(Enum graphicSectionType)
        {
            // Returns the direction of racing from the graphicSectionType enum
            if (_movingDirectionNorth.Contains(graphicSectionType))
            {
                return CardinalDirections.North;
            }
            else if (_movingDirectionEast.Contains(graphicSectionType))
            {
                return CardinalDirections.East;
            }
            else if (_movingDirectionSouth.Contains(graphicSectionType))
            {
                return CardinalDirections.South;
            }
            else // the same as _movingDirectionWest.Contains(GraphicSectionType)
            {
                return CardinalDirections.West;
            }
        }

        private static int[] DetermineNextPosition(Enum graphicSectionType)
        {
            CardinalDirections angle = DetermineAngleOfGraphicSectionType(graphicSectionType); // Call this method to request the directon of graphicSectionType
            // Determines the positional values the GraphicSectionType needs to get for the console (returns x and y movement values)
            if (angle == CardinalDirections.North)
            {
                return new int[] { 0, -4 }; // x = 0, y = -4
            }
            else if (angle == CardinalDirections.East)
            {
                return new int[] { 4, 0 }; // x = 4, y = 0
            }
            else if (angle == CardinalDirections.South)
            {
                return new int[] { 0, 4 }; // x = 0, y = 4
            }
            else // the same as direction == "West"
            {
                return new int[] { -4, 0 }; // x = -4, y = 0
            }
        }

        private static Enum DetermineNextGraphicSectionType(Section section, Enum graphicSectionType)
        {
            CardinalDirections angle = DetermineAngleOfGraphicSectionType(graphicSectionType); // Call this method to request the directon of graphicSectionType
            // Based on the previous graphicSectionType and new section, declare a new graphicSectionType
            if (angle == CardinalDirections.North)
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.StartGridNorth,
                    SectionTypes.Finish => GraphicSectionTypes.FinishNorth,
                    SectionTypes.Straight => GraphicSectionTypes.StraightNorth,
                    SectionTypes.RightCorner => GraphicSectionTypes.CornerEastSouth,
                    SectionTypes.LeftCorner => GraphicSectionTypes.CornerSouthWest,
                    _ => null
                };
            }
            else if (angle == CardinalDirections.East)
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.StartGridEast,
                    SectionTypes.Finish => GraphicSectionTypes.FinishEast,
                    SectionTypes.Straight => GraphicSectionTypes.StraightEast,
                    SectionTypes.RightCorner => GraphicSectionTypes.CornerSouthWest,
                    SectionTypes.LeftCorner => GraphicSectionTypes.CornerNorthWest,
                    _ => null
                };
            }
            else if (angle == CardinalDirections.South)
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.StartGridSouth,
                    SectionTypes.Finish => GraphicSectionTypes.FinishSouth,
                    SectionTypes.Straight => GraphicSectionTypes.StraightSouth,
                    SectionTypes.RightCorner => GraphicSectionTypes.CornerNorthWest,
                    SectionTypes.LeftCorner => GraphicSectionTypes.CornerNorthEast,
                    _ => null
                };
            }
            else // the same as angle == "South"
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.StartGridWest,
                    SectionTypes.Finish => GraphicSectionTypes.FinishWest,
                    SectionTypes.Straight => GraphicSectionTypes.StraightWest,
                    SectionTypes.RightCorner => GraphicSectionTypes.CornerNorthEast,
                    SectionTypes.LeftCorner => GraphicSectionTypes.CornerEastSouth,
                    _ => null
                };
            }
        }

        private static Enum DetermineNextCornerGraphicSectionType(Enum graphicSectionType, Enum graphicSectionType)
        {
            // Because corners enter at an angle and exit at an angle
        }

        private static List<string[]> ConvertGraphicSectionTypesToGraphicArrays(List<Enum> graphicSectionTypesList)
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

        private static void WriteGraphicsToConsole(List<Enum> graphicSectionTypesList, List<int[]> positionsList)
        {
            // First convert using ConvertGraphicSectionTypesToGraphicArrays
            List<string[]> graphicSectionsList = ConvertGraphicSectionTypesToGraphicArrays(graphicSectionTypesList);
            int[] tempCursorPosition = FixCursorPosition(positionsList); // Fix the cursor position if the x or y count is negative
            Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1]); // Set the cursor once to the corrected position
            for (int i = 0; i < graphicSectionsList.Count; i++) // Start loop at the length of graphicSectionsList (is the same as positionsList)
            {
                foreach (string line in graphicSectionsList[i]) // Print each array line following the for loop index
                {
                    Console.Write($"{line}\n");
                }
                tempCursorPosition[0] += positionsList[i][0];
                tempCursorPosition[1] += positionsList[i][1];
                Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1]); // Set the cursor afterwards to the next position
            }

        }

        public static int[] FixCursorPosition(List<int[]> positionsList)
        {
            // Compensation method for the console so graphics don't go out of bounds
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
            if (xCount < 0)
            {
                newXCount = Math.Abs(lowestXCount); // If lowestXCount is negative, for example -8, add 8 to newXCount
            }
            if (yCount < 0)
            {
                newYCount = Math.Abs(lowestYCount);
            }
            return new int[] { newXCount, newYCount };
        }

        #region graphics
        // Draw the graphics per line horizontally - also, karts only move clockwise, not counter clockwise
        private static string[] _startGridNorth =      { "|  |",
                                                         "|↑ |",
                                                         "| ↑|",
                                                         "|  |" };
        private static string[] _startGridEast =       { "----",
                                                         "  → ",
                                                         " →  ",
                                                         "----" };
        private static string[] _startGridSouth =      { "|  |",
                                                         "|↓ |",
                                                         "| ↓|",
                                                         "|  |" };
        private static string[] _startGridWest =       { "----",
                                                         "  ← ",
                                                         " ←  ",
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
    {
        North,
        East,
        South,
        West
    };
}