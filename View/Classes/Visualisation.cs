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
            GraphicSectionTypes.startGridNorth,
            GraphicSectionTypes.finishNorth,
            GraphicSectionTypes.straightNorth
        };
        private static Enum[] _movingDirectionEast = new Enum[] { // x = 4, y = 0
            GraphicSectionTypes.startGridEast,
            GraphicSectionTypes.finishEast,
            GraphicSectionTypes.straightEast
        };
        private static Enum[] _movingDirectionSouth = new Enum[] { // x = 0, y = -4
            GraphicSectionTypes.startGridSouth,
            GraphicSectionTypes.finishSouth,
            GraphicSectionTypes.straightSouth
        };
        private static Enum[] _movingDirectionWest = new Enum[] { // x = -4, y = 0
            GraphicSectionTypes.startGridWest,
            GraphicSectionTypes.finishWest,
            GraphicSectionTypes.straightWest,
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
                SectionTypes.StartGrid => GraphicSectionTypes.startGridEast,
                SectionTypes.Finish => GraphicSectionTypes.finishEast,
                SectionTypes.Straight => GraphicSectionTypes.straightEast,
                SectionTypes.RightCorner => GraphicSectionTypes.cornerSouthWest,
                SectionTypes.LeftCorner => GraphicSectionTypes.cornerNorthWest,
                _ => null
            };
        }

        private static string DetermineAngleOfGraphicSectionType(Enum graphicSectionType)
        {
            // Returns the direction of racing from the graphicSectionType enum
            if (_movingDirectionNorth.Contains(graphicSectionType))
            {
                return "North";
            }
            else if (_movingDirectionEast.Contains(graphicSectionType))
            {
                return "East";
            }
            else if (_movingDirectionSouth.Contains(graphicSectionType))
            {
                return "South";
            }
            else // the same as _movingDirectionWest.Contains(GraphicSectionType)
            {
                return "West";
            }
        }

        private static int[] DetermineNextPosition(Enum graphicSectionType)
        {
            string angle = DetermineAngleOfGraphicSectionType(graphicSectionType); // Call this method to request the directon of graphicSectionType
            // Determines the positional values the GraphicSectionType needs to get for the console (returns x and y movement values)
            if (angle == "North")
            {
                return new int[] { 0, -4 }; // x = 0, y = -4
            }
            else if (angle == "East")
            {
                return new int[] { 4, 0 }; // x = 4, y = 0
            }
            else if (angle == "South")
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
            string angle = DetermineAngleOfGraphicSectionType(graphicSectionType); // Call this method to request the directon of graphicSectionType
            // Based on the previous graphicSectionType and new section, declare a new graphicSectionType
            if (angle == "North")
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.startGridNorth,
                    SectionTypes.Finish => GraphicSectionTypes.finishNorth,
                    SectionTypes.Straight => GraphicSectionTypes.straightNorth,
                    SectionTypes.RightCorner => GraphicSectionTypes.cornerEastSouth,
                    SectionTypes.LeftCorner => GraphicSectionTypes.cornerSouthWest,
                    _ => null
                };
            }
            else if (angle == "East")
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.startGridEast,
                    SectionTypes.Finish => GraphicSectionTypes.finishEast,
                    SectionTypes.Straight => GraphicSectionTypes.straightEast,
                    SectionTypes.RightCorner => GraphicSectionTypes.cornerSouthWest,
                    SectionTypes.LeftCorner => GraphicSectionTypes.cornerNorthWest,
                    _ => null
                };
            }
            else if (angle == "South")
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.startGridSouth,
                    SectionTypes.Finish => GraphicSectionTypes.finishSouth,
                    SectionTypes.Straight => GraphicSectionTypes.straightSouth,
                    SectionTypes.RightCorner => GraphicSectionTypes.cornerNorthWest,
                    SectionTypes.LeftCorner => GraphicSectionTypes.cornerNorthEast,
                    _ => null
                };
            }
            else // the same as angle == "South"
            {
                return section.SectionType switch
                {
                    SectionTypes.StartGrid => GraphicSectionTypes.startGridWest,
                    SectionTypes.Finish => GraphicSectionTypes.finishWest,
                    SectionTypes.Straight => GraphicSectionTypes.straightWest,
                    SectionTypes.RightCorner => GraphicSectionTypes.cornerNorthEast,
                    SectionTypes.LeftCorner => GraphicSectionTypes.cornerEastSouth,
                    _ => null
                };
            }
        }
        private static List<string[]> ConvertGraphicSectionTypesToGraphicArrays(List<Enum> graphicSectionTypesList)
        {
            List<string[]> graphicSectionsList = new List<string[]>(); // Create list for all the graphic array values
            foreach (Enum graphicSectionType in graphicSectionTypesList) // Loop through the section Enums
            {
                switch (graphicSectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case GraphicSectionTypes.startGridNorth: graphicSectionsList.Add(_startGridNorth); break;
                    case GraphicSectionTypes.startGridEast: graphicSectionsList.Add(_startGridEast); break;
                    case GraphicSectionTypes.startGridSouth: graphicSectionsList.Add(_startGridSouth); break;
                    case GraphicSectionTypes.startGridWest: graphicSectionsList.Add(_startGridWest); break;
                    case GraphicSectionTypes.finishNorth: graphicSectionsList.Add(_finishVertical); break;
                    case GraphicSectionTypes.finishEast: graphicSectionsList.Add(_finishHorizontal); break;
                    case GraphicSectionTypes.finishSouth: graphicSectionsList.Add(_finishVertical); break;
                    case GraphicSectionTypes.finishWest: graphicSectionsList.Add(_finishHorizontal); break;
                    case GraphicSectionTypes.straightNorth: graphicSectionsList.Add(_straightVertical); break;
                    case GraphicSectionTypes.straightEast: graphicSectionsList.Add(_straightHorizontal); break;
                    case GraphicSectionTypes.straightSouth: graphicSectionsList.Add(_straightVertical); break;
                    case GraphicSectionTypes.straightWest: graphicSectionsList.Add(_straightHorizontal); break;
                    case GraphicSectionTypes.cornerSouthWest: graphicSectionsList.Add(_cornerSouthWest); break;
                    case GraphicSectionTypes.cornerEastSouth: graphicSectionsList.Add(_cornerEastSouth); break;
                    case GraphicSectionTypes.cornerNorthEast: graphicSectionsList.Add(_cornerNorthEast); break;
                    case GraphicSectionTypes.cornerNorthWest: graphicSectionsList.Add(_cornerNorthWest); break;
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
            //int[] tempCursorPosition = new int[] { 70, 70 };
            Console.SetCursorPosition(tempCursorPosition[0], tempCursorPosition[1]);
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
    public enum GraphicSectionTypes { // Declare new section types for angle determination
        startGridNorth,
        startGridEast,
        startGridSouth,
        startGridWest,
        finishNorth,
        finishEast,
        finishSouth,
        finishWest,
        straightNorth,
        straightEast,
        straightSouth,
        straightWest,
        cornerSouthWest,
        cornerEastSouth,
        cornerNorthEast,
        cornerNorthWest
    };
}