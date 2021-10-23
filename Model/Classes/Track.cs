using System.Collections.Generic;

namespace Model.Classes
{
    public class Track // Racebaan
    {
        // Properties
        public string Name { get; set; }
        public int AmountOfLaps { get; set; }
        public LinkedList<Section> Sections { get; set; }

        // Constructors
        public Track(string name, int amountOfLaps, Section.SectionTypes[] sections)
        {
            Name = name;
            AmountOfLaps = amountOfLaps;
            Sections = SectionTypesToLinkedListOfSections(sections);
        }

        // Methods
        public LinkedList<Section> SectionTypesToLinkedListOfSections(Section.SectionTypes[] sectionTypesArray)
        {
            // Inputs an array of SectionTypes and returns a LinkedList of Section classes with the SectionTypes in them
            LinkedList<Section> sectionList = new LinkedList<Section>();
            foreach (Section.SectionTypes sectionType in sectionTypesArray) // Add all the sections to the LinkedList
            {
                sectionList.AddLast(new Section(sectionType));
            }
            return sectionList;
        }
    }
}
