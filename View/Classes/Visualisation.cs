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
        // Methods
        public static void DrawTrack(Track track)
        {
            // Convert sections to graphicsSectionTypes and positions and also keep track of the direction of the previous graphic section type
            List<GraphicSectionTypes> graphicSectionTypesList = new List<GraphicSectionTypes>();
            List<int[]> positionsList = new List<int[]>();

            // Assign a random value for the first section
            Random random = new Random(DateTime.Now.Millisecond);
            CardinalDirections directionPreviousGraphicSectionType = (CardinalDirections)random.Next(Enum.GetNames(typeof(CardinalDirections)).Length);

            foreach (Section section in track.Sections)
            {
                    graphicSectionTypesList.Add(DetermineNextGraphicSectionType(section, ref directionPreviousGraphicSectionType));
                    positionsList.Add(DetermineNextPosition(directionPreviousGraphicSectionType));
            }

            WriteGraphicsToConsole(graphicSectionTypesList, positionsList, track.Sections); // Finally write the track graphics!
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
                        directionPreviousGraphicSectionType = CardinalDirections.East; break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.West; break;
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
                        directionPreviousGraphicSectionType = CardinalDirections.South; break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.North; break;
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
                        directionPreviousGraphicSectionType = CardinalDirections.West; break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.East; break;
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
                        directionPreviousGraphicSectionType = CardinalDirections.North; break;
                    case SectionTypes.LeftCorner:
                        newGraphicSectionType = DetermineNextCornerGraphicSectionType(SectionTypes.LeftCorner, directionPreviousGraphicSectionType);
                        directionPreviousGraphicSectionType = CardinalDirections.South; break;
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

        private static List<string[]> ConvertGraphicSectionTypesToGraphicArrays(List<GraphicSectionTypes> graphicSectionTypesList, LinkedList<Section> sections)
        {
            List<string[]> graphicSectionsList = new List<string[]>(); // Create list for all the graphic array values
            int counter = 0; // Alright, sorry for this, it starts to get quite spaghetti here, but it's my first time coding C# and SOLID
                             // and I don't want to rewrite all this, so the counter is needed to call the element from section
            foreach (Enum graphicSectionType in graphicSectionTypesList) // Loop through the section Enums
            {
                SectionData participant = Data.CurrentRace.GetSectionData(sections.ElementAt(counter));
                switch (graphicSectionType) // Allocate correct graphic array to Enum and add to list
                {
                    case GraphicSectionTypes.StartGridNorth: graphicSectionsList.Add(PlaceParticipants(_startGridNorth, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.StartGridEast: graphicSectionsList.Add(PlaceParticipants(_startGridEast, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.StartGridSouth: graphicSectionsList.Add(PlaceParticipants(_startGridSouth, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.StartGridWest: graphicSectionsList.Add(PlaceParticipants(_startGridWest, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.FinishNorth: graphicSectionsList.Add(PlaceParticipants(_finishVertical, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.FinishEast: graphicSectionsList.Add(PlaceParticipants(_finishHorizontal, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.FinishSouth: graphicSectionsList.Add(RotateStringArray180Degrees(PlaceParticipants(_finishVertical, participant.Left, participant.Right))); break;
                    case GraphicSectionTypes.FinishWest: graphicSectionsList.Add(RotateStringArray180Degrees(PlaceParticipants(_finishHorizontal, participant.Left, participant.Right))); break;
                    case GraphicSectionTypes.StraightNorth: graphicSectionsList.Add(PlaceParticipants(_straightVertical, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.StraightEast: graphicSectionsList.Add(PlaceParticipants(_straightHorizontal, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.StraightSouth: graphicSectionsList.Add(RotateStringArray180Degrees(PlaceParticipants(_straightVertical, participant.Left, participant.Right))); break;
                    case GraphicSectionTypes.StraightWest: graphicSectionsList.Add(RotateStringArray180Degrees(PlaceParticipants(_straightHorizontal, participant.Left, participant.Right))); break;
                    case GraphicSectionTypes.CornerSouthWest: graphicSectionsList.Add(PlaceParticipants(_cornerSouthWest, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.CornerEastSouth: graphicSectionsList.Add(PlaceParticipants(_cornerEastSouth, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.CornerNorthEast: graphicSectionsList.Add(PlaceParticipants(_cornerNorthEast, participant.Left, participant.Right)); break;
                    case GraphicSectionTypes.CornerNorthWest: graphicSectionsList.Add(PlaceParticipants(_cornerNorthWest, participant.Left, participant.Right)); break;
                    default: break;
                }
                counter++;
            }
            return graphicSectionsList;
        }

        private static void WriteGraphicsToConsole(List<GraphicSectionTypes> graphicSectionTypesList, List<int[]> positionsList, LinkedList<Section> sections)
        {
            List<string[]> graphicSectionsList = ConvertGraphicSectionTypesToGraphicArrays(graphicSectionTypesList, sections); // Convert the Enums to actual Graphics
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

        private static int[] FixCursorPosition(List<int[]> positionsList)
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

        private static string[] RotateStringArray180Degrees(string[] inputArray)
        {
            string[] outputArray = new string[inputArray.Length]; // Makes new string array with the same length as the input array
            for (int s = 0; s < inputArray.Length; s++) // Loops through the length of the array
            {
                outputArray[s] = new string(inputArray[s].ToCharArray().Reverse().ToArray()); // Uses LINQ to reverse the string and save it
            }
            Array.Reverse(outputArray, 0, inputArray.Length); // Reverses the array (because mirror + reverse = 180 degree turn)
            return outputArray;
        }

        private static string[] PlaceParticipants(string[] graphicSection, IParticipant leftParticipant, IParticipant rightParticipant)
        {
            string[] outputGraphicSection = (string[])graphicSection.Clone();
            char leftParticipantFirstLetterName = ' ';
            char rightParticipantFirstLetterName = ' ';
            if (leftParticipant != null)
            { // Assigns the first letter of a name to a temporary variable, or if the participant is not given, it keeps the default blank space
                leftParticipantFirstLetterName = leftParticipant.Name[0];
            }
            if (rightParticipant != null)
            {
                rightParticipantFirstLetterName = rightParticipant.Name[0];
            }
            // Replaces the 1 for the leftParticipantFirstLetterName and the 2 for the rightParticipantFirstLetterName
            outputGraphicSection[1] = outputGraphicSection[1].Replace(char.Parse($"1"), leftParticipantFirstLetterName);
            outputGraphicSection[2] = outputGraphicSection[2].Replace(char.Parse($"1"), leftParticipantFirstLetterName);
            outputGraphicSection[1] = outputGraphicSection[1].Replace(char.Parse($"2"), rightParticipantFirstLetterName);
            outputGraphicSection[2] = outputGraphicSection[2].Replace(char.Parse($"2"), rightParticipantFirstLetterName);
            return outputGraphicSection;
        }

        #region graphics
        // Draw the graphics per line horizontally
        private static string[] _startGridNorth =      { "|^ |",
                                                         "|1^|",
                                                         "| 2|",
                                                         "|  |" };
        private static string[] _startGridEast =       { "----",
                                                         "  1>",
                                                         " 2> ",
                                                         "----" };
        private static string[] _startGridSouth =      { "|  |",
                                                         "|2 |",
                                                         "|v1|",
                                                         "| v|" };
        private static string[] _startGridWest =       { "----",
                                                         " <2 ",
                                                         "<1  ",
                                                         "----" };
        private static string[] _finishVertical =      { "|  |", // Points to the north
                                                         "|1 |",
                                                         "| 2|",
                                                         "|##|" };
        private static string[] _finishHorizontal =    { "----", // Points to the east
                                                         "# 1 ",
                                                         "#2  ",
                                                         "----" };
        private static string[] _straightVertical =    { "|  |", // Points to the north
                                                         "|1 |",
                                                         "| 2|",
                                                         "|  |" }; 
        private static string[] _straightHorizontal =  { "----", // Points to the east
                                                         "  1 ",
                                                         " 2  ",
                                                         "----" };
        private static string[] _cornerSouthWest =     {@"--\ ", // Right corner down & left corner up
                                                        @"  2\",
                                                         " 1 |",
                                                        @"\  |" };
        private static string[] _cornerEastSouth =     {@" /--", // Left corner down & right corner up
                                                        @"/2  ",
                                                         "| 1 ",
                                                        @"|  /" };
        private static string[] _cornerNorthEast =     {@"|  \", // Right corner up & left corner down
                                                         "| 1 ",
                                                        @"\2  ",
                                                        @" \--" };
        private static string[] _cornerNorthWest =     {@"/  |", // Left corner up & right corner down
                                                         " 1 |",
                                                        @"  2/",
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