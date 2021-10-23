using Model.Classes;
using Controller.Classes;
using System;
using System.Collections.Generic;
using static Model.Classes.Section;

namespace View.Classes
{
    public static class Visualisation_Extra
    {
        public static void MakeTrack()
        { // This is broken now after level 5, and since it's extra I'm not gonna fix it :-)
            List<Section.SectionTypes> sectionList = new List<SectionTypes>();

            Console.Write("What is the name of your track? ");
            string name = Console.ReadLine();
            Console.Clear();
            Track userTrack = new Track(name, 3, new Section.SectionTypes[0]);

            string input = null;
            while (input != "done")
            {
                Console.SetCursorPosition(80, 0);
                Console.Write("Enter section: ");
                input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "grid": case "g": sectionList.Add(SectionTypes.StartGrid); break;
                    case "straight": case "s": sectionList.Add(SectionTypes.Straight); break;
                    case "left": case "l": sectionList.Add(SectionTypes.LeftCorner); break;
                    case "right": case "r": sectionList.Add(SectionTypes.RightCorner); break;
                    case "finish": case "f": sectionList.Add(SectionTypes.Finish); break;
                    default: break;
                }
                userTrack.Sections = userTrack.SectionTypesToLinkedListOfSections(sectionList.ToArray());
                    
                Visualisation.DrawTrack(userTrack);
            }
            Data.GrandPrix.Tracks.Enqueue(userTrack);
        }
    }
}
